using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoveSave
{
    class ChatItem
    {
        private string _qqNumber;   //QQ号
        private string _unix;       //时间戳
        private string _content;    //内容
        private string _imgUrl;     //所含图片下载地址
        private string _imgFileName;//保存图片时所指定的文件名

        public string Name
        {
            get
            {
                return GetNameByQQ(_qqNumber);
            }
        }
        public DateTime Time
        {
            get
            {
                return GetTimeByUnix(_unix);
            }
        }
        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
            }
        }
        public string ImgUrl
        {
            get
            {
                return _imgUrl;
            }

            set
            {
                _imgUrl = value;
            }
        }

        public ChatItem(JToken jt)
        {
            _qqNumber = jt["uin"].ToString();
            _unix = jt["time"].ToString();
            _content = jt["content"].ToString();
            _imgUrl = "";
            if (jt["richval"].ToString().Contains("\"type\":\"image\""))
            {
                string str = jt["richval"].ToString();
                _imgUrl = Regex.Match(str, Constant.findUrlInRichval).Value + "670";
            }
            _imgFileName = Guid.NewGuid().ToString();
        }

        private string GetNameByQQ(string _qqNumber)
        {
            if (_qqNumber == "914724771")
            {
                return "傻蛋";
            }
            else if (_qqNumber == "727043293")
            {
                return "傻丫头";
            }
            else
            {
                return "";
            }
        }
        private DateTime GetTimeByUnix(string _unix)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(_unix + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
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

        public bool hasImage()
        {
            return ImgUrl == "" ? false : true;
        }
        public void SaveToDatabase()
        {
            DBhelper.Save2tbChat(Name, Time, Content, ImgUrl, hasImage(), _imgFileName);
        }
        public void DownloadImageTo(string savePath)
        {
            if (!hasImage())
            {
                return;
            }
            #region savePath合法性验证
            if (Path.HasExtension(savePath))
            {
                //包含扩展名说明是文件路径,去掉文件名和扩展名
                savePath = savePath.Replace(Path.GetFileName(savePath), "");
            }
            if (savePath.Substring(savePath.Length - 1, 1) != "\\")
            {
                //如果最后一个字符不是'\',就把'\'加上
                savePath += "\\";
            }
            if (!Directory.Exists(savePath))
            {
                //如果目标文件夹不存在,则创建
                Directory.CreateDirectory(savePath);
            }
            #endregion
            DownloadImage(ImgUrl, savePath, _imgFileName);
        }

    }
}
