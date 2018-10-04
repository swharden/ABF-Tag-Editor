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
            string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\16d05007_vc_tags.abf";
            //string demoABF = @"C:\Users\scott\Documents\GitHub\pyABF\data\abfs\abf1_with_tags.abf";
            abftag = new AbfTagEdit(demoABF);
            formConsole.TextSet(abftag.GetLog(true));
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
    }
}
