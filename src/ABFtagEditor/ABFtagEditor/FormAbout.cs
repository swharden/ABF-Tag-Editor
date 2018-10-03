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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            AbfTagEdit tag = new AbfTagEdit();
            lblVersion.Text = tag.versionString;
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            // when the textbox gets focus (which would normally let you see a caret and select text), lose it
            pictureBox1.Focus();
        }

        private void rtbGitHub_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/swharden/ABF-Tag-Editor");
        }

        private void rtbAuthor_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://tech.swharden.com/");            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tech.swharden.com/");
        }
    }
}
