using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    class ImageInfo
    {
        private string _diaryGid;
        private string _url;
        private string _fileName;

        public string DiaryGid
        {
            get
            {
                return _diaryGid;
            }

            set
            {
                _diaryGid = value;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }


        public ImageInfo(string analysisStr, string diaryGid)
        {
            _diaryGid = diaryGid;
            _url = GetUrl(analysisStr, out _fileName);
        }

        private string GetUrl(string analysisStr, out string fileName)
        {
            string url = AnalysisUrl(analysisStr);
            string lastWord = url.Substring(url.Length - 1, 1);
            if (lastWord == "?")
            {
                //系统自带图片
                url = url.Substring(0, url.Length - 1); //去掉问号
                string[] temp = url.Split('/');
                fileName = temp[temp.Length - 1];
                Debug.Print($"filename = {FileName }");
                return url;
            }
            else if (lastWord == "/")
            {
                //用户上传图片
                fileName = AnalysisLloclist(analysisStr) + ".jpg";
                fileName = fileName.Replace('*', '_');
                Debug.Print($"filename = {FileName }");
                return url + "670";
            }
            else
            {
                throw new Exception("Found an error when analysis url of richval.");
            }

        }
        private string AnalysisLloclist(string analysisStr)
        {
            return RegexHelper.GetMatch(analysisStr, Constant.findLloclistInRichval);
        }
        private string AnalysisUrl(string analysisStr)
        {
            string url = RegexHelper.GetMatch(analysisStr, Constant.findUrlInRichval);
            Debug.Print(url);
            return url;
        }

        public void Save()
        {
            DBhelper.Save2tbDiaryImage(DiaryGid, Url, FileName);
        }
        public void Download()
        {
            string path = Constant.DiaryImageDownloadPath + $"{FileName}";
            if (File.Exists(path))
            {
                return;
            }

            HttpWebRequest request;
            request = WebRequest.Create(Url) as HttpWebRequest;
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
    }
}
