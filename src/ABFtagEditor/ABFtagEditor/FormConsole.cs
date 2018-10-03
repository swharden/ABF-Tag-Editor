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
    public partial class FormConsole : Form
    {
        public FormConsole()
        {
            InitializeComponent();
        }

        private void FormConsole_Load(object sender, EventArgs e)
        {

        }

        public void TextAdd(string msg, bool breakBefore = true)
        {
            if (breakBefore)
                msg = "\n" + msg;
            richTextBox1.Text += msg;
        }

        public void TextSet(string msg)
        {
            richTextBox1.Text = msg;
        }

        public void TextClear()
        {
            richTextBox1.Text = "";
        }
    }
}
