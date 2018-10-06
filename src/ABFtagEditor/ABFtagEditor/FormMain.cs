using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABFtagEditor
{
    public partial class FormMain : Form
    {
        public static FormConsole formConsole;
        AbfTagEdit abftag;

        public FormMain()
        {
            InitializeComponent();
            formConsole = new FormConsole();
            formConsole.TextSet("");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadABF("");
        }

        private void LoadABF(string abfFilePath)
        {
            if (System.IO.File.Exists(abfFilePath))
            {
                btnLaunch.Enabled = true;
                lblAbfFileName.Text = System.IO.Path.GetFileName(abfFilePath);
            }
            else
            {
                lblAbfFileName.Text = "";
                btnLaunch.Enabled = false;
                cbTags.Enabled = false;
                cbTags.Items.Clear();
                nudMin.Enabled = false;
                nudMin.Value = 0;
                nudSec.Enabled = false;
                nudSec.Value = 0;
                nudSweep.Enabled = false;
                nudSweep.Value = 0;
                nudTagTime.Value = 0;
                tbComment.Enabled = false;
                tbComment.Text = "";
                return;
            }

            if (System.IO.File.Exists(abfFilePath + ".backup"))
            {
                BackupButtonUpdate(false);
                btnBackup.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                BackupButtonUpdate(true);
                btnBackup.Enabled = true;
                btnSave.Enabled = false;
            }

            abftag = new AbfTagEdit(abfFilePath);
            UpdateGui();
            string status = $"ABF file contains {abftag.tags.Count} comment tag";
            if (abftag.tags.Count != 1)
                status += "s";
            lblStatus.Text = status;
        }

        private void UpdateGui()
        {
            // update debug console
            formConsole.TextSet(abftag.GetLog());

            // set NUD limits to reflect ABF data
            nudMin.Maximum = (decimal)(abftag.abfTotalLengthSec / 60.0);
            nudSec.Maximum = (decimal)(abftag.abfTotalLengthSec);
            nudSweep.Maximum = (decimal)(abftag.abfSweepCount);

            // if no file is loaded or no tags exist, gray everything
            if (abftag == null || abftag.tags.Count == 0)
            {
                cbTags.Enabled = false;
                cbTags.Items.Clear();
                nudMin.Enabled = false;
                nudSec.Enabled = false;
                nudSweep.Enabled = false;
                tbComment.Enabled = false;
                tbComment.Text = "";
                return;
            }
            else
            {
                cbTags.Enabled = true;
                nudMin.Enabled = true;
                nudSec.Enabled = true;
                nudSweep.Enabled = true;
                tbComment.Enabled = true;
            }

            // update combobox list of tags
            if (cbTags.Items.Count == abftag.tags.Count)
            {
                // same number of items in the list, so just update the text
                for (int i = 0; i < abftag.tags.Count; i++)
                {
                    string tagLine = $"Tag {i + 1}: {abftag.tags[i].description}";
                    cbTags.Items[i] = tagLine;
                }
            }
            else
            {
                // different number of tags, so rebuild the list from scratch
                cbTags.Items.Clear();
                for (int i = 0; i < abftag.tags.Count; i++)
                {
                    string tagLine = $"Tag {i + 1}: {abftag.tags[i].description}";
                    cbTags.Items.Add(tagLine);
                }
                cbTags.SelectedIndex = 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        // GUI EVENT ACTIONS

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(abftag.abfPath);
            lblStatus.Text = "Launching ABF in ClampFit...";
        }

        private void cbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbTags.SelectedIndex;
            try
            {
                nudMin.Value = (decimal)abftag.tags[i].tagTimeMin;
                nudSec.Value = (decimal)abftag.tags[i].tagTimeSec;
                nudSweep.Value = (decimal)abftag.tags[i].tagTimeSweep;
                nudTagTime.Value = (decimal)abftag.tags[i].tagTime;
                tbComment.Text = abftag.tags[i].comment;
            }
            catch
            {
                Console.WriteLine("EXCEPTION");
            }
        }

        private void tbComment_TextChanged(object sender, EventArgs e)
        {
            if (abftag == null || abftag.tags.Count == 0 || abftag.tags.Count != cbTags.Items.Count) return;
            abftag.tags[cbTags.SelectedIndex].SetComment(tbComment.Text);
            UpdateGui();
        }

        private void nudSec_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null || abftag.tags.Count == 0 || abftag.tags.Count != cbTags.Items.Count) return;
            double timeSec = (double)(nudSec.Value);
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }

        private void nudMin_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null || abftag.tags.Count == 0 || abftag.tags.Count != cbTags.Items.Count) return;
            double timeSec = (double)(nudMin.Value * 60);
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }

        private void nudSweep_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null || abftag.tags.Count == 0 || abftag.tags.Count != cbTags.Items.Count) return;
            double sweepLengthSec = abftag.tags[0].sweepLengthSec;
            double timeSec = (double)nudSweep.Value * sweepLengthSec;
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.Filter = "ABF Files (*.abf)|*.abf|All files (*.*)|*.*";
            if (abftag != null)
                diag.InitialDirectory = System.IO.Path.GetDirectoryName(abftag.abfPath);
            if (diag.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = diag.FileName;
                LoadABF(selectedPath);
            }
        }

        private void BackupButtonUpdate(bool backupNeeded = true)
        {
            if (backupNeeded)
            {
                btnBackup.Enabled = true;
                btnBackup.BackColor = Color.LightGoldenrodYellow;
                btnBackup.UseVisualStyleBackColor = false;
            }
            else
            {
                btnBackup.Enabled = false;
                btnBackup.BackColor = SystemColors.Control;
                btnBackup.UseVisualStyleBackColor = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            System.IO.File.Copy(abftag.abfPath, abftag.abfPath + ".backup");
            lblStatus.Text = $"Created {System.IO.Path.GetFileName(abftag.abfPath)}.backup";
            BackupButtonUpdate(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            abftag.GetLog(true);
            abftag.WriteTags();
            lblStatus.Text = "ABF file saved with new tags!";
            formConsole.TextAdd(abftag.GetLog(true));
        }

        private void loadABFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLoad_Click(null, null);
        }

        private void closeABFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadABF("");
        }

        private void developerConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (developerConsoleToolStripMenuItem.Checked)
            {
                developerConsoleToolStripMenuItem.Checked = false;
                formConsole.Visible = false;
            } else
            {
                developerConsoleToolStripMenuItem.Checked = true;
                formConsole.Visible = true;
            }
        }
    }
}
