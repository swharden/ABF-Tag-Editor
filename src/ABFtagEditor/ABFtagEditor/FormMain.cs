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
        public FormMain()
        {
            InitializeComponent();
            formConsole = new FormConsole();
            formConsole.Show();
            formConsole.TextSet("wow, that was hard");
        }

        AbfTagEdit abftag;
        private void Form1_Load(object sender, EventArgs e)
        {
            //string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d05007_vc_tags.abf";
            string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\abf1_with_tags.abf";
            //string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\File_axon_7.abf";
            abftag = new AbfTagEdit(demoABF);
            UpdateGui();
        }

        private void UpdateGui()
        {
            // update debug console
            formConsole.TextSet(abftag.GetLog());

            // set NUD limits to reflect ABF data
            nudMin.Maximum = (decimal)(abftag.abfTotalLengthSec / 60.0);
            nudSec.Maximum = (decimal)(abftag.abfTotalLengthSec);
            nudSweep.Maximum = (decimal)(abftag.abfSweepCount);

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
        }

        private void cbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbTags.SelectedIndex;
            try
            {
                nudMin.Value = (decimal)abftag.tags[i].tagTimeMin;
                nudSec.Value = (decimal)abftag.tags[i].tagTimeSec;
                nudSweep.Value = (decimal)abftag.tags[i].tagTimeSweep;
                tbComment.Text = abftag.tags[i].comment;
            } catch
            {
                Console.WriteLine("EXCEPTION");
            }
        }

        private void tbComment_TextChanged(object sender, EventArgs e)
        {
            if (abftag == null) return;
            abftag.tags[cbTags.SelectedIndex].SetComment(tbComment.Text);
            UpdateGui();
        }

        private void nudSec_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null) return;
            double timeSec = (double)(nudSec.Value);
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }

        private void nudMin_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null) return;
            double timeSec = (double)(nudMin.Value * 60);
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }

        private void nudSweep_ValueChanged(object sender, EventArgs e)
        {
            if (abftag == null) return;
            double sweepLengthSec = abftag.tags[0].sweepLengthSec;
            double timeSec = (double)nudSweep.Value * sweepLengthSec;
            abftag.tags[cbTags.SelectedIndex].SetTimeSec(timeSec);
            UpdateGui();
        }
    }
}
