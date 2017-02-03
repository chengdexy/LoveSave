using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace LoveSaveDo
{
    public partial class Form1 : Form
    {
        #region 常量
        private const int TheFristOne = 0;
        private const int validItemIndexBegins = 3; //默认为3
        private const string breakToItems = "<div class=\"item\">.*?(?=<div class=\"item\">)";
        private const string findNickName = "(?<=<span class=\"username ellipsis_1\">).*?(?=</span>)";
        private const string findItemTime = "(?<=<p class=\"t_time\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</p>)";
        private const string findItemContent = "(?<=<div class=\"con_txt.*?\">\\s*).*?(?=\\s*<)";
        private const string findAllComments = "(?<=<div class=\"comments_content\">).*?(?=</div?)";
        private const string findCommentName = "(?<=>\\s*)\\S.*?\\S(?=\\s*</a>)";
        private const string findCommentTime = "(?<=<span class=\"t_date\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</span>)";
        private const string findCommentContent = "(?<=</a>\\s*)\\S.*?\\S(?=\\s*<div)";
        private const string findItemImages = "<img.*?>";
        private const string findUrlOfImage = "(?<=url=\").*?(?=\\?\")";
        private const string findlloc = "(?<=lloc=\").*?(?=\")";
        private const string findAlbumid = "(?<=albumid=\").*?(?=\")";
        #endregion

        private string strResource;
        private MatchCollection mc;
        private int indexOfItem = 0;
        private int CountOfItems = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //读入html文件
            ReadHtml();
            //拆分html文件
            Regex rx = new Regex(breakToItems);
            mc = rx.Matches(strResource);
            CountOfItems = mc.Count;
            lblCount.Text = "/" + (CountOfItems - validItemIndexBegins - 1).ToString();
            txtIndex.Text = indexOfItem.ToString();
            //解析第1个拆分项
            AnalysisItem(mc[validItemIndexBegins]);
        }

        /// <summary>
        /// 解析一个item
        /// </summary>
        /// <param name="item"></param>
        private void AnalysisItem(Match item)
        {
            #region 输入
            string itemStr = item.Value;
            //获取昵称
            string nickName = GetNickName(itemStr);
            //获取时间
            DateTime itemTime = GetItemTime(itemStr);
            //获取内容
            string itemContent = GetItemContent(itemStr);
            //如果itemContent为空,则可能是上传图片到相册,尝试获取图片列表

            //获得此item所包含的图片的**文件名**列表
            //文件名命名规则为: `indexOfItem_indexOfImgList.jpg`
            string[] imgList = GetItemImgList(itemStr);
            //将图片展示到前台去

            //获取评论
            Comments[] comment = GetComments(itemStr);
            #endregion

            #region 输出
            //输出到gui
            txtNickName.Text = nickName;
            txtItemTime.Text = itemTime.ToString("yyyy-MM-dd mm:ss");
            txtItemContent.Text = itemContent;
            //输出评论
            dgvComments.Rows.Clear();
            for (int i = 0; i < comment.Length; i++)
            {
                dgvComments.Rows.Add(comment[i].CommentName, comment[i].CommentTime.ToString("yyyy-MM-dd mm:ss"), comment[i].CommentContent);
            }
            #endregion
        }

        /// <summary>
        /// 获得一个item中所包含的图片
        /// 将图片下载后重命名保存在本地
        /// 并返回由它们本地的文件名组成的文件名列表
        /// 
        /// 命名规则: `indexOfItem_indexOfImgList.jpg`
        /// 
        /// </summary>
        /// <param name="itemStr"></param>
        /// <returns>字符串数组,用于存储所有出现的文件名</returns>
        private string[] GetItemImgList(string itemStr)
        {
            int counter = 0;
            List<string> list = new List<string>();
            list.Clear();
            //获得所有符合<img.*?>的子串集合
            //对每个子串进行分析
            foreach (Match match in Regex.Matches(itemStr, findItemImages))
            {
                string albumid = "";
                string lloc = "";
                string filePath = "";
                albumid = GetAlbumid(match.Value);
                lloc = Getlloc(match.Value);
                //如果不包含有效的albumid和lloc,则
                if (string.IsNullOrEmpty(albumid) || string.IsNullOrEmpty(lloc))
                {
                    //下载url中所指向的图片地址
                    filePath = GetUrlOfImage(match.Value);
                }
                else
                {
                    //如果包含有效的albumid和lloc,则下载它
                    filePath = $"http://group.store.qq.com/sweet/{albumid}/{lloc}/670";
                }
                //将下载得到的图片保存为约定的文件名
                if (!string.IsNullOrEmpty(filePath))
                {
                    //有时候会包含albumid,lloc,url都为空的情况
                    string fileName = DownloadImage(filePath, indexOfItem.ToString() + "_" + counter.ToString());
                    list.Add(fileName);
                }
                counter++;
            }
            //将文件名存入数组返回
            return list.ToArray();
        }

        /// <summary>
        /// 下载指定url的图片,并保存为指定的文件名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string DownloadImage(string filePath, string filename)
        {
            HttpWebRequest request;

            request = WebRequest.Create(filePath) as HttpWebRequest;//picpath,图片地址
            request.Method = "GET";
            request.Timeout = 30000;
            request.AllowAutoRedirect = true;
            request.ContentType = "image/bmp";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                sr.Close();
                string path = Environment.CurrentDirectory + $"\\imgDownload\\{filename}.jpg";
                sourcebm.Save(path);//filename 保存地址
            }
            return filename;
        }

        private string GetUrlOfImage(string value)
        {
            return Regex.Match(value, findUrlOfImage).Value;
        }

        private string Getlloc(string value)
        {
            return Regex.Match(value, findlloc).Value;
        }

        private string GetAlbumid(string value)
        {
            return Regex.Match(value, findAlbumid).Value;
        }

        /// <summary>
        /// 获取所有评论内容
        /// </summary>
        /// <param name="itemStr"></param>
        /// <returns></returns>
        private static Comments[] GetComments(string itemStr)
        {
            //获得所有评论的Matches集合
            List<Comments> list = new List<Comments>();
            list.Clear();
            foreach (Match match in Regex.Matches(itemStr, findAllComments))
            {
                string name = Regex.Matches(match.Value, findCommentName)[TheFristOne].Value;
                DateTime time = Convert.ToDateTime(Regex.Matches(match.Value, findCommentTime)[TheFristOne].Value);
                string content = Regex.Matches(match.Value, findCommentContent)[TheFristOne].Value;
                //为每个Match实例化一个Comments对象,并加入List中
                list.Add(new Comments()
                {
                    CommentName = name,
                    CommentTime = time,
                    CommentContent = content
                });
            }

            //将List转化为数组返回
            return list.ToArray();
        }

        /// <summary>
        /// 获取一个item的正文文本
        /// 其中可能包含部分未能成功过滤的html代码
        /// </summary>
        /// <param name="itemStr"></param>
        /// <returns>返回值可能为空</returns>
        private static string GetItemContent(string itemStr)
        {
            return Regex.Matches(itemStr, findItemContent)[TheFristOne].Value.Trim();
        }

        /// <summary>
        /// 获取一个item的发表时间
        /// </summary>
        /// <param name="itemStr"></param>
        /// <returns></returns>
        private static DateTime GetItemTime(string itemStr)
        {
            return Convert.ToDateTime(Regex.Matches(itemStr, findItemTime)[TheFristOne].Value);
        }

        /// <summary>
        /// 获取一个item中所包含的昵称信息
        /// 即,该篇日志的作者
        /// </summary>
        /// <param name="itemStr"></param>
        /// <returns></returns>
        private static string GetNickName(string itemStr)
        {
            return Regex.Matches(itemStr, findNickName)[TheFristOne].Value;
        }

        /// <summary>
        /// 打开Open对话框,读取一个html文件,将内容保存在字段strResource中
        /// </summary>
        private void ReadHtml()
        {
            ofdOpen.ShowDialog();
            if (!File.Exists(ofdOpen.FileName))
            {
                return;
            }
            StreamReader sr = new StreamReader(ofdOpen.FileName, Encoding.UTF8);
            strResource = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            indexOfItem++;
            if (indexOfItem > CountOfItems - validItemIndexBegins - 1)
            {
                //下标越界
                indexOfItem = CountOfItems - validItemIndexBegins - 1;
            }
            AnalysisItem(mc[validItemIndexBegins + indexOfItem]);
            txtIndex.Text = indexOfItem.ToString();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            indexOfItem--;
            if (indexOfItem < 0)
            {
                //下标越界
                indexOfItem = 0;
            }
            AnalysisItem(mc[validItemIndexBegins + indexOfItem]);
            txtIndex.Text = indexOfItem.ToString();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIndex.Text.Trim()))
            {
                int index = Convert.ToInt32(txtIndex.Text);
                if (index < 0)
                {
                    index = 0;
                }
                else if (index > CountOfItems - validItemIndexBegins - 1)
                {
                    index = CountOfItems - validItemIndexBegins - 1;
                }

                AnalysisItem(mc[index + validItemIndexBegins]);
                indexOfItem = index;
            }
            else
            {
                txtIndex.Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new LoveSaveDo.Form2();
            f.Show();
        }
    }
}
