namespace ABFtagEditor
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadABFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeABFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developerConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSec = new System.Windows.Forms.NumericUpDown();
            this.nudSweep = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTagAdd = new System.Windows.Forms.Button();
            this.btnTagDelete = new System.Windows.Forms.Button();
            this.cbTags = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweep)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(485, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadABFToolStripMenuItem,
            this.closeABFToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadABFToolStripMenuItem
            // 
            this.loadABFToolStripMenuItem.Name = "loadABFToolStripMenuItem";
            this.loadABFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadABFToolStripMenuItem.Text = "Load ABF ...";
            // 
            // closeABFToolStripMenuItem
            // 
            this.closeABFToolStripMenuItem.Name = "closeABFToolStripMenuItem";
            this.closeABFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.closeABFToolStripMenuItem.Text = "Close ABF";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.developerConsoleToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // developerConsoleToolStripMenuItem
            // 
            this.developerConsoleToolStripMenuItem.Name = "developerConsoleToolStripMenuItem";
            this.developerConsoleToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.developerConsoleToolStripMenuItem.Text = "Developer Console";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(485, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(129, 17);
            this.lblStatus.Text = "Load an ABF to begin...";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(218, 39);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 39);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(83, 23);
            this.btnLoad.TabIndex = 20;
            this.btnLoad.Text = "Load ABF";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(332, 39);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(126, 23);
            this.btnLaunch.TabIndex = 22;
            this.btnLaunch.Text = "Launch in ClampFit";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(95, 39);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(117, 23);
            this.btnBackup.TabIndex = 24;
            this.btnBackup.Text = "Create Backup ABF";
            this.btnBackup.UseVisualStyleBackColor = true;
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(6, 75);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(452, 20);
            this.tbComment.TabIndex = 0;
            // 
            // nudMin
            // 
            this.nudMin.DecimalPlaces = 2;
            this.nudMin.Location = new System.Drawing.Point(6, 34);
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(70, 20);
            this.nudMin.TabIndex = 0;
            this.nudMin.Value = new decimal(new int[] {
            612,
            0,
            0,
            131072});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(82, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "second:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudSec
            // 
            this.nudSec.Location = new System.Drawing.Point(82, 34);
            this.nudSec.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSec.Name = "nudSec";
            this.nudSec.Size = new System.Drawing.Size(70, 20);
            this.nudSec.TabIndex = 2;
            this.nudSec.Value = new decimal(new int[] {
            367,
            0,
            0,
            0});
            // 
            // nudSweep
            // 
            this.nudSweep.DecimalPlaces = 1;
            this.nudSweep.Location = new System.Drawing.Point(158, 34);
            this.nudSweep.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudSweep.Name = "nudSweep";
            this.nudSweep.Size = new System.Drawing.Size(70, 20);
            this.nudSweep.TabIndex = 3;
            this.nudSweep.Value = new decimal(new int[] {
            237,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(158, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "sweep:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "minute:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "comment:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnTagAdd
            // 
            this.btnTagAdd.Enabled = false;
            this.btnTagAdd.Location = new System.Drawing.Point(416, 18);
            this.btnTagAdd.Name = "btnTagAdd";
            this.btnTagAdd.Size = new System.Drawing.Size(42, 23);
            this.btnTagAdd.TabIndex = 1;
            this.btnTagAdd.Text = "Add";
            this.btnTagAdd.UseVisualStyleBackColor = true;
            // 
            // btnTagDelete
            // 
            this.btnTagDelete.Enabled = false;
            this.btnTagDelete.Location = new System.Drawing.Point(359, 18);
            this.btnTagDelete.Name = "btnTagDelete";
            this.btnTagDelete.Size = new System.Drawing.Size(51, 23);
            this.btnTagDelete.TabIndex = 2;
            this.btnTagDelete.Text = "Delete";
            this.btnTagDelete.UseVisualStyleBackColor = true;
            // 
            // cbTags
            // 
            this.cbTags.FormattingEnabled = true;
            this.cbTags.Location = new System.Drawing.Point(6, 19);
            this.cbTags.Name = "cbTags";
            this.cbTags.Size = new System.Drawing.Size(347, 21);
            this.cbTags.TabIndex = 7;
            this.cbTags.Text = "6.34 min - some drug added 200 nM";
            this.cbTags.SelectedIndexChanged += new System.EventHandler(this.cbTags_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "2018_04_13_0016b_modified.abf";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnLaunch);
            this.groupBox1.Controls.Add(this.btnBackup);
            this.groupBox1.Location = new System.Drawing.Point(9, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 68);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABF file to modify";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTags);
            this.groupBox2.Controls.Add(this.btnTagDelete);
            this.groupBox2.Controls.Add(this.btnTagAdd);
            this.groupBox2.Location = new System.Drawing.Point(9, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 48);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select tag";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nudMin);
            this.groupBox3.Controls.Add(this.nudSweep);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.nudSec);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbComment);
            this.groupBox3.Location = new System.Drawing.Point(9, 157);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 102);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tag time";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 293);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABF Tag Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweep)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadABFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeABFToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudSweep;
        private System.Windows.Forms.NumericUpDown nudSec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTagAdd;
        private System.Windows.Forms.Button btnTagDelete;
        private System.Windows.Forms.ComboBox cbTags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripMenuItem developerConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

