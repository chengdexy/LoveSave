using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoveSave
{
    public partial class LoveSaveForm : Form
    {
        public LoveSaveForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ofdHtml.ShowDialog();
            if (string.IsNullOrEmpty(ofdHtml.FileName))
            {
                return;
            }
            else
            {
                if (!File.Exists(ofdHtml.FileName))
                {
                    return;
                }
                else
                {
                    btnOpen.Enabled = false;
                    btnOpen.Visible = false;
                    //读html内容
                    lblResult.Text = "正在读取文件内容...";
                    lblResult.Refresh();
                    string strResource = ReadHtml(ofdHtml.FileName);
                    //解析内容
                    lblResult.Text = "正在解析文件内容...";
                    lblResult.Refresh();
                    Analysis ana = new Analysis(strResource);
                    //保存内容
                    lblResult.Text = "正在将解析结果保存至数据库...";
                    lblResult.Refresh();
                    ana.SaveToDatabase();
                    //下载图片
                    lblResult.Text = "正在下载涉及的图片...";
                    lblResult.Refresh();
                    ana.DownloadImagesTo(Environment.CurrentDirectory + "\\Result\\Images");
                    lblResult.Text = "全部工作已完成!";
                    //打开文件夹供查看
                }
            }
        }

        private string ReadHtml(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return result;
        }

        private void LoveSaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBhelper.CloseCnxn();
        }
    }
}
