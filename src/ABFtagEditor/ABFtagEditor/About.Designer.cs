namespace ABFtagEditor
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtbAuthor = new System.Windows.Forms.RichTextBox();
            this.lblSep = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.rtbGitHub = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(132, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 32);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "ABF Tag Editor";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 170);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // rtbAuthor
            // 
            this.rtbAuthor.BackColor = System.Drawing.SystemColors.Control;
            this.rtbAuthor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbAuthor.Location = new System.Drawing.Point(138, 124);
            this.rtbAuthor.Name = "rtbAuthor";
            this.rtbAuthor.ReadOnly = true;
            this.rtbAuthor.Size = new System.Drawing.Size(168, 65);
            this.rtbAuthor.TabIndex = 16;
            this.rtbAuthor.Text = "Scott W Harden, D.M.D., Ph.D\nPresident & CEO\nHarden Technologies, LLC\nhttp://tech" +
    ".SWHarden.com";
            this.rtbAuthor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbAuthor_MouseClick);
            this.rtbAuthor.Enter += new System.EventHandler(this.richTextBox1_Enter);
            // 
            // lblSep
            // 
            this.lblSep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSep.Location = new System.Drawing.Point(127, 7);
            this.lblSep.Name = "lblSep";
            this.lblSep.Size = new System.Drawing.Size(2, 170);
            this.lblSep.TabIndex = 17;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(135, 29);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(69, 13);
            this.lblVersion.TabIndex = 18;
            this.lblVersion.Text = "Version 0.0.1";
            // 
            // rtbGitHub
            // 
            this.rtbGitHub.BackColor = System.Drawing.SystemColors.Control;
            this.rtbGitHub.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbGitHub.Location = new System.Drawing.Point(138, 66);
            this.rtbGitHub.Name = "rtbGitHub";
            this.rtbGitHub.ReadOnly = true;
            this.rtbGitHub.Size = new System.Drawing.Size(229, 37);
            this.rtbGitHub.TabIndex = 19;
            this.rtbGitHub.Text = "Project Page:\nhttp://github.com/swharden/ABF-Tag-Editor";
            this.rtbGitHub.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbGitHub_MouseClick);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 188);
            this.Controls.Add(this.rtbGitHub);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblSep);
            this.Controls.Add(this.rtbAuthor);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "About";
            this.Text = "About ABF Tag Editor";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox rtbAuthor;
        private System.Windows.Forms.Label lblSep;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.RichTextBox rtbGitHub;
    }
}