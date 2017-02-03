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

namespace LoveSaveDo
{
    public partial class Form1 : Form
    {
        #region 常量
        private const int TheFristOne = 0;
        private const int validItemIndexBegins = 3;
        private const string breakToItems = "<div class=\"item\">.*?(?=<div class=\"item\">)";
        private const string findNickName = "(?<=<span class=\"username ellipsis_1\">).*?(?=</span>)";
        private const string findItemTime = "(?<=<p class=\"t_time\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</p>)";
        private const string findItemContent = "(?<=<div class=\"con_txt\">).*?(?=</div>)";
        private const string findAllComments = "(?<=<div class=\"comments_content\">).*?(?=</div?)";
        private const string findCommentName = "(?<=>\\s*)\\S.*?\\S(?=\\s*</a>)";
        private const string findCommentTime = "(?<=<span class=\"t_date\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</span>)";
        private const string findCommentContent = "(?<=</a>\\s*)\\S.*?\\S(?=\\s*<div)";
        #endregion

        private string strResource;
        private MatchCollection mc;
        private int indexOfItem = 0;

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
            return Regex.Matches(itemStr, findItemContent)[TheFristOne].Value;
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            indexOfItem++;
            AnalysisItem(mc[validItemIndexBegins + indexOfItem]);
        }
    }
}
