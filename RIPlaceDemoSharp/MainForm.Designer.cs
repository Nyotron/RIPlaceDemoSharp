namespace RIPlaceDemoSharp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lnkLearnMore = new System.Windows.Forms.LinkLabel();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(354, 61);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = resources.GetString("lblInfo.Text");
            // 
            // txtPath
            // 
            this.txtPath.AllowDrop = true;
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtPath.Location = new System.Drawing.Point(12, 71);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(469, 20);
            this.txtPath.TabIndex = 0;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPath_DragDrop);
            this.txtPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtPath_DragEnter);
            this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyDown);
            this.txtPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPath_KeyPress);
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(487, 69);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "RIPlace";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(372, 9);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(190, 55);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 4;
            this.picLogo.TabStop = false;
            // 
            // picStatus
            // 
            this.picStatus.Image = global::RIPlaceDemoSharp.Properties.Resources.ready;
            this.picStatus.Location = new System.Drawing.Point(11, 19);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(70, 70);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 5;
            this.picStatus.TabStop = false;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.lnkLearnMore);
            this.grpStatus.Controls.Add(this.picStatus);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Location = new System.Drawing.Point(12, 99);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(550, 100);
            this.grpStatus.TabIndex = 7;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // lnkLearnMore
            // 
            this.lnkLearnMore.AutoSize = true;
            this.lnkLearnMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLearnMore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLearnMore.Location = new System.Drawing.Point(464, 76);
            this.lnkLearnMore.Name = "lnkLearnMore";
            this.lnkLearnMore.Size = new System.Drawing.Size(80, 13);
            this.lnkLearnMore.TabIndex = 7;
            this.lnkLearnMore.TabStop = true;
            this.lnkLearnMore.Text = "Find out more...";
            this.lnkLearnMore.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkLearnMore.Visible = false;
            this.lnkLearnMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLearnMore_LinkClicked);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(87, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(457, 57);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(574, 211);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(590, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 250);
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RIPlace Demo Tool";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPath_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtPath_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.LinkLabel lnkLearnMore;
    }
}

