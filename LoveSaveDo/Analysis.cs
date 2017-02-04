using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoveSave
{
    class Analysis
    {
        #region 正则
        private const string breakToItems = "<div class=\"item\">.*?(?=<div class=\"item\">)";//将html文件粗略拆分为若干个item
        #endregion
        #region 字段
        private string _htmlString;
        private AnalysisItem[] _items;
        #endregion
        #region 属性
        public AnalysisItem[] Items
        {
            get
            {
                return _items;
            }
        }
        #endregion
        #region 构造
        public Analysis(string html)
        {
            _htmlString = html;

            List<AnalysisItem> list = new List<LoveSave.AnalysisItem>();
            list.Clear();

            MatchCollection mc = Regex.Matches(html, breakToItems);
            for (int i = 3; i < mc.Count; i++)
            {
                list.Add(new AnalysisItem(i, mc[i].Value));
            }

            _items = list.ToArray();
        }
        #endregion
        #region 方法
        public void SaveToDatabase()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].SaveToDatabase();
            }
        }
        public void DownloadImagesTo(string savePath)
        {
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
            //开始下载
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].DownloadImagesTo(savePath);
            }
        }
        #endregion
    }
}
