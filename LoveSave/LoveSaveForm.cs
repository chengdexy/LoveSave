using Microsoft.Win32;
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
        private List<string> diaryList = new List<string>();
        private List<string> chatList = new List<string>();
        private List<string> memosList = new List<string>();
        private List<Diary> diaries = new List<Diary>();
        private List<ChatItem> chats = new List<ChatItem>();
        private List<Memos> memos = new List<Memos>();
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
            browserMain.Navigate("about:blank");
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
                if (!Directory.Exists(Constant.ResultPath))
                {
                    Directory.CreateDirectory(Constant.ResultPath);
                    Directory.CreateDirectory(Constant.DiaryImageDownloadPath);
                    Directory.CreateDirectory(Constant.ChatImageDownloadPath);
                    Directory.CreateDirectory(Constant.PagesPath);
                }
                File.Copy($"{Constant.DataModelPath}", $"{Constant.ResultPath}Data.mdb", true);
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
        private void GetNecessary()
        {
            string url;
            //获取情侣的QQ号 - peeruin
            long t = DateHelper.DateToStamp(DateTime.Now, false);
            url = $"http://gm.show.qq.com/cgi-bin/qs_gm_qlyz_get?cmd=getbaseinfo&t={t}&opuin={uin}&uin={uin}&plat=0&g_tk={g_tk}";
            WaitNavigated();
            peeruin = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, Constant.findPeerUin);
            //获取diary记录总数
            url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_getbyhouse?g_tk={g_tk}&uin={uin}&start=0&num=1&opuin={uin}&plat=0&outputformat=2";
            WaitNavigated();
            diaryTotal = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, Constant.findTotal);
            //获取chat记录总数
            url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_chat_getmsg?opuin={uin}&luin={peeruin}&cmd=0&beginidx=0&endidx=0&order=0&src=1&plat=0&uin={uin}&g_tk={g_tk}";
            WaitNavigated();
            chatTotal = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, Constant.findTotal);

        }
        private void SetRegistryKey()
        {
            RegistryKey key = Registry.ClassesRoot;
            RegistryKey type = key.CreateSubKey(@"MIME\Database\Content Type\application/x-javascript");
            type.SetValue("CLSID", "{25336920-03F9-11cf-8FD0-00AA00686F13}", RegistryValueKind.String);
            //type.SetValue("Encoding", Encoding.UTF8.GetBytes("80000"), RegistryValueKind.Binary);
            type.Flush();
            type.Close();
            type.Dispose();
            key.Close();
            key.Dispose();
        }
        private void GetAllMemos()
        {
            string html;
            string url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_memorialday_get_v3?uin={uin}&opuin={uin}&plat=0&g_tk={g_tk}";
            WaitNavigated();
            html = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, "{.*}");
            memosList.Add(html);
        }
        private void GetAllChatJson()
        {
            string html;
            int step = 100;
            int beginidx = 0;
            int endidx;
            int sum = Convert.ToInt32(chatTotal);
            int turn = sum / step + 1;
            for (int i = 0; i < turn; i++)
            {
                beginidx = i * step;
                if (beginidx + step >= sum)
                {
                    endidx = beginidx + sum % step - 1;
                }
                else
                {
                    endidx = beginidx + step - 1;
                }
                string url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_chat_getmsg?opuin={uin}&luin={peeruin}&cmd=0&beginidx={beginidx}&endidx={endidx}&order=0&src=1&plat=0&uin={uin}&g_tk={g_tk}";
                WaitNavigated();
                html = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, "{.*}");
                chatList.Add(html);
            }
        }
        private void GetAllDiaryJson()
        {
            string html;
            int start, num;
            int step = 50;
            int sum = Convert.ToInt32(diaryTotal);
            int turn = sum / step + 1;
            for (int i = 0; i < turn; i++)
            {
                start = i * step;
                if (start + step > sum)
                {
                    num = sum % step;
                }
                else
                {
                    num = step;
                }
                string url = $"http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_getbyhouse?g_tk={g_tk}&uin={uin}&start={start}&num={num}&opuin={uin}&plat=0&outputformat=4";
                WaitNavigated();
                html = RegexHelper.GetMatchWaitBrowser(url, ref browserMain, "{.*}");
                diaryList.Add(html);
            }
        }
        private void AddResult(string result)
        {
            listResult.Items.Insert(0, result);
            listResult.SelectedIndex = 0;
        }
        #endregion
        #region 事件 <===入口
        //程序开始 <=步骤1
        private void LoveSaveForm_Load(object sender, EventArgs e)
        {
            this._NetCut.RequestComplete += _NetCut_RequestComplete;
            this._NetCut.Install();
        }
        //得到gtk,uin <=步骤2
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
                    this.Width = 350;
                    btnStart.Enabled = true;
                    btnStart.Text = "登陆成功,点击这里开始捕获数据!!";
                    AddResult("登陆成功!");
                    AddResult("点击上方按钮开始捕获数据...");
                }
            }
            ));
        }
        //点击事件 <=步骤3
        private void btnStart_Click(object sender, EventArgs e)
        {
            #region 自用字段初始化
            diaryList.Clear();
            chatList.Clear();
            memosList.Clear();
            diaries.Clear();
            chats.Clear();
            memos.Clear();
            #endregion
            btnStart.Text = "若卡住,点我重试!";
            AddResult("捕捉核心参数中...");
            #region 修改注册表
            //修改注册表,阻止"application/x-javascript"型文件被下载
            SetRegistryKey();
            #endregion
            #region 获取必要参数
            //得到peeruin,diaryTotal,chatTotal
            GetNecessary();
            #endregion
            AddResult("成功捕获核心参数!");
            AddResult($"验证号:{g_tk}");
            AddResult($"登陆QQ号:{uin}");
            AddResult($"情侣QQ号{peeruin}");
            #region 获取需要解析的原始文本
            AddResult("开始检索内容...(若卡住,请再次点击上方按钮)");
            //获取所有diary记录的json文本
            AddResult($"检测到共有情侣日志{diaryTotal}篇,开始解析详细内容...");
            GetAllDiaryJson();
            AddResult("完成!");
            //获取所有chat记录的json文件
            AddResult($"检测到共有密语聊天记录{chatTotal}条,开始解析详细内容...");
            GetAllChatJson();
            AddResult("完成!");
            //获取所有memos记录的json文件
            AddResult("检测到纪念日信息若干,开始解析详细内容...");
            GetAllMemos();
            AddResult($"完成!共{memosList.Count}个.");
            #endregion
            AddResult("全部解析已完成!开始将数据保存至数据库,并将相关图片保存至本地...");
            #region 解析原始文本生成Result
            btnStart.Text = "保存数据中,请不要结束进程!";
            //创建数据库
            CopyDatabaseToResult();
            //解析diary
            diaryList.ForEach(str =>
            {
                JObject jo = JObject.Parse(str);
                jo.SelectToken("data").Select(p => p).ToList().ForEach(jodata =>
                {
                    Diary d = new Diary(jodata);
                    diaries.Add(d);
                    d.Save();
                });
            });
            //解析chat
            chatList.ForEach(str =>
            {
                JObject jo = JObject.Parse(str);
                jo.SelectToken("data").Select(p => p).ToList().ForEach(jodata =>
                {
                    ChatItem ci = new ChatItem(jodata);
                    chats.Add(ci);
                    ci.SaveToDatabase();
                    if (ci.hasImage())
                    {
                        ci.DownloadImageTo(Constant.ChatImageDownloadPath);
                    }
                });
            });
            //解析memos
            memosList.ForEach(str =>
            {
                JObject jo = JObject.Parse(str);
                jo.SelectToken("memos").Select(p => p).ToList().ForEach(jodata =>
                {
                    string time = jodata["time"].ToString();
                    int year = Convert.ToInt32(time.Substring(0, 4));
                    int month = Convert.ToInt32(time.Substring(5, 2));
                    int day = Convert.ToInt32(time.Substring(8, 2));
                    Memos mm = new Memos(
                        jodata["name"].ToString(),
                        year, month, day,
                        jodata["lunar"].ToString().Trim() == "1" ? true : false
                        );
                    memos.Add(mm);
                    mm.SaveToDatabase();
                });
            });
            #endregion
            DBhelper.CloseCnxn();
            PageHelper ph = new PageHelper();
            ph.CreateAll(diaries, chats, memos);
            #region 打开文件夹供查看
            AddResult($"全部工作已完成!!");
            AddResult($"{uin}与{peeruin}的情侣空间中包含的主要内容已成功导出并保存到本地!");
            MessageBox.Show("导出成功！即将弹出导出结果.", "导出成功!", MessageBoxButtons.OK, MessageBoxIcon.Question);
            Process.Start("explorer.exe", Constant.ResultPath + "index.html");
            #endregion
            btnStart.Visible = false;
            btnResult.Visible = true;
            btnResult.Enabled = true;
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Constant.ResultPath);
        }
        //被动事件
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() == "about:blank" && browserMain.ReadyState == WebBrowserReadyState.Complete)
            {
                canStartNewNavigate = true;
            }
        }
        private void LoveSaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBhelper.CloseCnxn();
        }
        #endregion
    }
}
