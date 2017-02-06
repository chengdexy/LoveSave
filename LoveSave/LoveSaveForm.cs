using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private static bool DatabaseCopied = false;

        public LoveSaveForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ofdHtml.Filter = "Html文件(*.htm)|*.htm";
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
                    CopyDatabaseToResult();
                    ana.SaveToDatabase();
                    //下载图片
                    lblResult.Text = "正在下载涉及的图片...";
                    lblResult.Refresh();
                    ana.DownloadImagesTo( "Result\\Images");
                    lblResult.Text = "全部工作已完成!";
                    //打开文件夹供查看
                    System.Diagnostics.Process.Start("explorer.exe",  "Result\\");
                }
            }
        }
        private void btnChat_Click(object sender, EventArgs e)
        {
            ofdHtml.Filter = "JSON文件(*.json)|*.json";
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
                    //解析内容
                    lblResult.Text = "正在解析内容...";
                    lblResult.Refresh();
                    StreamReader sr = new StreamReader(ofdHtml.FileName, Encoding.Default);
                    string strJson = sr.ReadToEnd();

                    JObject joChat = JObject.Parse(strJson);

                    //保存内容
                    lblResult.Text = "正在保存内容到数据库，并将发现的图片下载到本地...";
                    lblResult.Refresh();
                    CopyDatabaseToResult();
                    var joDatas = joChat.SelectToken("data").Select(p => p).ToList();
                    foreach (var joData in joDatas)
                    {
                        ChatItem ci = new ChatItem(joData);
                        ci.SaveToDatabase();
                        //下载图片（如果有）
                        if (ci.hasImage())
                        {
                            ci.DownloadImageTo("Result\\ChatImage\\");
                        }
                    }
                    DBhelper.CloseCnxn();
                    //打开文件夹供查看
                    lblResult.Text = "全部工作已完成!";
                    System.Diagnostics.Process.Start("explorer.exe", "Result\\");
                }
            }
        }
        private void btnMemos_Click(object sender, EventArgs e)
        {
            ofdHtml.Filter = "JSON文件(*.json)|*.json";
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
                    //解析内容
                    lblResult.Text = "正在解析内容...";
                    lblResult.Refresh();
                    StreamReader sr = new StreamReader(ofdHtml.FileName, Encoding.Default);
                    string strJson = sr.ReadToEnd();

                    JObject joChat = JObject.Parse(strJson);

                    //保存内容
                    lblResult.Text = "正在保存内容到数据库...";
                    lblResult.Refresh();
                    CopyDatabaseToResult();
                    var joDatas = joChat.SelectToken("memos").Select(p => p).ToList();
                    foreach (var joData in joDatas)
                    {
                        string time = joData["time"].ToString();
                        int year = Convert.ToInt32(time.Substring(0, 4));
                        int month = Convert.ToInt32(time.Substring(5, 2));
                        int day = Convert.ToInt32(time.Substring(8, 2));
                        Memos mm = new Memos(
                            joData["name"].ToString(),
                            year, month, day,
                            joData["lunar"].ToString().Trim() == "1" ? true : false
                            );
                        mm.SaveToDatabase();
                    }
                    DBhelper.CloseCnxn();
                    //打开文件夹供查看
                    lblResult.Text = "全部工作已完成!";
                    System.Diagnostics.Process.Start("explorer.exe", "Result\\");
                }
            }
        }

        private void CopyDatabaseToResult()
        {
            if (DatabaseCopied)
            {
                return;
            }
            else
            {
                #region 复制数据库文件
                if (!Directory.Exists( "Result"))
                {
                    Directory.CreateDirectory( "Result");
                }
                File.Copy( "Data.mdb", "Result\\Data.mdb", true);
                DatabaseCopied = true;
                #endregion
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
