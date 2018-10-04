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
        List<AbfTag> tags = new List<AbfTag>();
        private void Form1_Load(object sender, EventArgs e)
        {
            //string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d05007_vc_tags.abf";
            //string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\abf1_with_tags.abf";
            //abftag = new AbfTagEdit(demoABF);
            //formConsole.TextSet(abftag.GetLog(true));
            CreateDemoTags();
            UpdateGuiFromTags();
        }

        private void CreateDemoTags()
        {
            // make some fake tags to practice with
            tags.Clear();
            tags.Add(new AbfTag(7000000, "TTX started", 1 / 80000.0, 2));
            tags.Add(new AbfTag(13918208, "TGOT added", 1 / 80000.0, 2));
            tags.Add(new AbfTag(23588864, "TGOT removed", 1 / 80000.0, 2));

            foreach (AbfTag tag in tags)
            {
                Console.WriteLine($"{tag.comment} @ {tag.tagTimeMin} min (sweep {tag.tagTimeSweep})");
            }

        }

        private void UpdateGuiFromTags()
        {
            cbTags.Items.Clear();
            for (int i=0; i<tags.Count; i++)
            {
                cbTags.Items.Add($"Tag {i+1}: {tags[i].description}");
            }
            cbTags.SelectedIndex = 0;
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
            nudMin.Value = (decimal) tags[i].tagTimeMin;
            nudSec.Value = (decimal) tags[i].tagTimeSec;
            nudSweep.Value = (decimal)tags[i].tagTimeSweep;
            tbComment.Text = tags[i].comment;
        }
    }
}
