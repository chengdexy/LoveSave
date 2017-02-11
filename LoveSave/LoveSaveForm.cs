using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        #region 自用字段
        private static bool DatabaseCopied = false;
        private readonly NetCut.NetCut _NetCut = new NetCut.NetCut();
        private bool canStartNewNavigate = false;
        #endregion
        #region 必要字段
        private string g_tk;
        private string uin;
        private string peeruin;
        private string diaryTotal;
        private string chatTotal;
        #endregion
        #region 构造
        public LoveSaveForm()
        {
            InitializeComponent();
        }
        #endregion
        #region 方法
        private void WaitNavigated()
        {
            canStartNewNavigate = false;
            webBrowser1.Navigate("about:blank");
            this.Enabled = false;
            while (!canStartNewNavigate)
            {
                Application.DoEvents();
            }
            this.Enabled = true;
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
                if (!Directory.Exists("Result"))
                {
                    Directory.CreateDirectory("Result");
                }
                File.Copy("Data.mdb", "Result\\Data.mdb", true);
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
        #endregion
        #region 事件 <===入口
        //程序开始
        private void LoveSaveForm_Load(object sender, EventArgs e)
        {
            this._NetCut.RequestComplete += _NetCut_RequestComplete;
            this._NetCut.Install();
        }
        //得到gtk,uin
        private void _NetCut_RequestComplete(object sender, NetCut.NetTab e)
        {
            base.Invoke(new EventHandler((x, y) =>
            {
                string requestStr = e.Service.ToString() + e.Url.ToString();
                if (requestStr.Contains("sweet_share_getbyhouse"))
                {
                    g_tk = RegexHelper.GetMatch(requestStr, "(?<=g_tk=)\\d*?(?=\\D)");
                    uin = RegexHelper.GetMatch(requestStr, "(?<=uin=)\\d*?(?=\\D)");
                    _NetCut.Uninstall();
                    WaitNavigated();
                    btnStart.Enabled = true;
                }
            }
            ));
        }
        //得到peeruin,diaryTotal,chatTotal
        private void btnStart_Click(object sender, EventArgs e)
        {
            #region 获取必要参数
            string url;
            //获取情侣的QQ号 - peeruin
            long t = DateHelper.DateToStamp(DateTime.Now, false);
            url = $"http://gm.show.qq.com/cgi-bin/qs_gm_qlyz_get?cmd=getbaseinfo&t={t}&opuin={uin}&uin={uin}&plat=0&g_tk={g_tk}";
            WaitNavigated();
            peeruin = RegexHelper.GetMatchWaitBrowser(url, ref webBrowser1, Constant.findPeerUin);
            //获取diary记录总数
            url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_getbyhouse?g_tk={g_tk}&uin={uin}&start=0&num=1&opuin={uin}&plat=0&outputformat=2";
            WaitNavigated();
            diaryTotal = RegexHelper.GetMatchWaitBrowser(url, ref webBrowser1, Constant.findTotal);
            //获取chat记录总数
            url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_chat_getmsg?opuin={uin}&luin={peeruin}&cmd=0&beginidx=0&endidx=1&order=0&src=1&plat=0&uin={uin}&g_tk={g_tk}";
            WaitNavigated();
            chatTotal = RegexHelper.GetMatchWaitBrowser(url, ref webBrowser1, Constant.findTotal);
            #endregion
            //获取所有diary记录的json文件
            //获取所有chat记录的json文件
            //获取所有memos记录的json文件

            Debug.Print($"Analysis successful!\ng_tk:{g_tk},uin:{uin},peeruin:{peeruin},\ndiaries:{diaryTotal},chats:{chatTotal}");
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() == "about:blank" && webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                canStartNewNavigate = true;
            }
        }
        private void LoveSaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBhelper.CloseCnxn();
        }
        //Todo: 以下是旧版功能触发按钮,待自动解析逻辑完成将删除
        private void btnDiary_Click(object sender, EventArgs e)
        {
            ofdHtml.Filter = "JSON文件(*.json)|*.json";
            ofdHtml.ShowDialog();
            if (string.IsNullOrEmpty(ofdHtml.FileName))
            {
                return;
            }
            if (!File.Exists(ofdHtml.FileName))
            {
                return;
            }
            lblResult.Text = "正在保存内容到数据库，并将发现的图片下载到本地...";
            lblResult.Refresh();
            CopyDatabaseToResult();
            //解析并保存内容
            JsonHelper.JsonToObjectCollection(ofdHtml.FileName, "data", Encoding.Default).ForEach(joData =>
            {
                new Diary(joData).Save();
            });
            DBhelper.CloseCnxn();
            //打开文件夹供查看
            lblResult.Text = "全部工作已完成!";
            System.Diagnostics.Process.Start("explorer.exe", "Result\\");
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
        #endregion 
    }
}
