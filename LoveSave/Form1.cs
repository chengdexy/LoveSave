using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LoveSave
{
    public partial class Form1 : Form
    {
        private string strResource = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ofdResource.ShowDialog();
            StreamReader sr = new StreamReader(ofdResource.FileName, Encoding.UTF8);
            strResource = sr.ReadToEnd();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strResource.Trim()))
            {
                return;
            }
            int i = 0;
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                i = Convert.ToInt32(textBox1.Text);
            }
            string pattern = txtRegular.Text.Trim();
            Regex rx = new Regex(pattern);
            MatchCollection mc = rx.Matches(strResource);
            label1.Text = (mc.Count - 1).ToString();
            txtContent.Text = mc[i].Value;
        }
    }
}
