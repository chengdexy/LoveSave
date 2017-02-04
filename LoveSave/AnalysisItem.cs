using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LoveSave
{
    /// <summary>
    /// "解析项目"类
    /// </summary>
    class AnalysisItem
    {
        #region 正则
        private const string findNickName = "(?<=<span class=\"username ellipsis_1\">).*?(?=</span>)";//在item中获取此item的作者昵称
        private const string findItemTime = "(?<=<p class=\"t_time\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</p>)";//item的发出时间
        private const string findItemContent = "(?<=<div class=\"con_txt.*?\">\\s*).*?(?=\\s*<)";//获取item所包含正文
        private const string findAllComments = "(?<=<div class=\"comments_content\">).*?(?=</div?)";//获取item中的评论部分,拆分为没条评论
        private const string findCommentName = "(?<=>\\s*)\\S.*?\\S(?=\\s*</a>)";//于评论中提取评论人名
        private const string findCommentTime = "(?<=<span class=\"t_date\">)\\d{4}年\\d\\d月\\d\\d日\\s\\d\\d:\\d\\d(?=</span>)";//评论中提取评论时间
        private const string findCommentContent = "(?<=</a>\\s*)\\S.*?\\S(?=\\s*<div)";//评论中提取评论内容
        private const string findItemImages = "<img.*?>";//提取item中所有<img>标签内容
        private const string findUrlOfImage = "(?<=url=\").*?(?=\\?\")";//于<img>中提取url属性的值
        private const string findlloc = "(?<=lloc=\").*?(?=\")";//于<img>中提取lloc属性的值(lloc="xxxx")
        private const string findAlbumid = "(?<=albumid=\").*?(?=\")";//于<img>中提取albumid属性的值
        #endregion

        #region 字段
        private string _resourceString = "";
        #endregion

        #region 属性
        public int Index { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }
        public KeyValuePair<string, string>[] ImagePaths { get; set; }
        public Comments[] Comments { get; set; }
        #endregion

        #region 构造
        public AnalysisItem(int index, string resString)
        {
            _resourceString = resString;
            Index = index;
            Name = GetItemName();
            Time = GetItemTime();
            Content = GetItemContent();
            ImagePaths = GetImagePathList();
            Comments = GetItemComments();
        }
        #endregion

        #region 私有方法
        #region 昵称
        private string GetItemName()
        {
            return Regex.Matches(_resourceString, findNickName)[0].Value;
        }
        #endregion
        #region 时间
        private DateTime GetItemTime()
        {
            return Convert.ToDateTime(Regex.Matches(_resourceString, findItemTime)[0].Value);
        }
        #endregion
        #region 内容
        private string GetItemContent()
        {
            return Regex.Matches(_resourceString, findItemContent)[0].Value.Trim();
        }
        #endregion
        #region 图片
        private KeyValuePair<string, string>[] GetImagePathList()
        {
            int counter = 0;
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Clear();

            foreach (Match match in Regex.Matches(_resourceString, findItemImages))
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
                    //有时候会包含albumid,lloc,url都为空的情况(已证实是抓到了头像图片)
                    string fileName = Index.ToString() + "_" + counter.ToString();
                    list.Add(new KeyValuePair<string, string>(fileName, filePath));
                }
                counter++;
            }
            //将文件名存入数组返回
            return list.ToArray();
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
        #endregion
        #region 评论
        private Comments[] GetItemComments()
        {
            //获得所有评论的Matches集合
            List<Comments> list = new List<Comments>();
            list.Clear();
            foreach (Match match in Regex.Matches(_resourceString, findAllComments))
            {
                string name = Regex.Matches(match.Value, findCommentName)[0].Value;
                DateTime time = Convert.ToDateTime(Regex.Matches(match.Value, findCommentTime)[0].Value);
                MatchCollection mc = Regex.Matches(match.Value, findCommentContent);
                string content = mc.Count == 0 ? "" : mc[0].Value;
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
        #endregion
        #region 下载
        /// <summary>
        /// 将指定url的图片下载到指定文件夹
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="savePath">保存的文件夹</param>
        /// <param name="filename">保存为的文件名</param>
        private void DownloadImage(string url, string savePath, string filename)
        {
            string path = savePath + $"\\{filename}.jpg";
            if (File.Exists(path))
            {
                return;
            }

            HttpWebRequest request;
            request = WebRequest.Create(url) as HttpWebRequest;
            #region 配置请求
            request.Method = "GET";
            request.Timeout = 30000;
            request.AllowAutoRedirect = true;
            request.ContentType = "image/bmp";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            #endregion
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                #region 读取响应
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                sr.Close();

                #endregion
                sourcebm.Save(path);
            }
        }
        #endregion

        #endregion

        #region 公共方法
        /// <summary>
        /// 将实例化后的对象所包含信息分别入库,包括:
        /// tbDiaryItem - 
        ///                 编号
        ///                 作者名
        ///                 发布时间
        ///                 内容文本
        /// tbDiaryImage -  
        ///                 对应Item编号
        ///                 下载URL
        ///                 保存时应使用的文件名
        /// tbDiaryComment - 
        ///                 对应Item编号
        ///                 评论人名
        ///                 评论时间
        ///                 评论内容文本
        /// </summary>
        public void SaveToDatabase()
        {
            //日志信息入库
            DBhelper.Save2tbDiaryItem(
                Index,
                Name,
                Time,
                Content,
                ImagePaths.Length > 0 ? true : false,
                Comments.Length > 0 ? true : false
                );
            //图片下载地址及存储时应使用的文件名入库
            for (int i = 0; i < ImagePaths.Length; i++)
            {
                DBhelper.Save2tbDiaryImage(
                    Index,
                    ImagePaths[i].Value,
                    ImagePaths[i].Key);
            }
            //评论信息入库
            for (int i = 0; i < Comments.Length; i++)
            {
                DBhelper.Save2tbDiaryComment(
                    Index,
                    Comments[i].CommentName,
                    Comments[i].CommentTime,
                    Comments[i].CommentContent
                    );
            }
        }
        /// <summary>
        /// 根据实例化后的对象所包含的图片下载地址,
        /// 将图片下载到指定的文件夹,
        /// 并使用指定的文件名来保存
        /// 指定的文件名 - ImagePath[i].Key
        /// 下载地址URL - ImagePath[i].Value
        /// </summary>
        /// <param name="savePath">指定的保存文件夹</param>
        public void DownloadImagesTo(string savePath)
        {
            for (int i = 0; i < ImagePaths.Length; i++)
            {
                DownloadImage(ImagePaths[i].Value, savePath, ImagePaths[i].Key);
            }
        }

        #endregion
    }
}
