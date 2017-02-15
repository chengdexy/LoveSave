using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    /// <summary>
    /// 评论呢,回复模版
    /// </summary>
    class PageReply
    {
        private string _html;

        public PageReply(string nick, string time, string content)
        {
            _html = "<div class=\"reply\">{nick}({time}):{content}</div>";
        }

        public string GetHtml()
        {
            return _html;
        }
    }
    /// <summary>
    /// 项目内,图片模版
    /// </summary>
    class PageImage
    {
        private string _html;

        public string Src { get; set; }

        public PageImage(string src)
        {
            Src = src;
            _html = $"<div class=\"col-xs-6 col-md-3\"><a href=\"\\{Src}\" class=\"thumbnail\"><img src=\"{Src}\"></a></div>";
        }

        public string GetHtml()
        {
            return _html;
        }
    }
    /// <summary>
    /// 项目内,评论模版
    /// </summary>
    class PageComment
    {
        private string _html;

        public string Nick { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public string[] Replys { get; set; }

        public PageComment()
        {
            InitHtml();
        }
        public PageComment(PageReply[] replys) : this()
        {
            if (replys == null)
            {
                Replys = null;
            }
            else
            {
                List<string> list = new List<string>();
                for (int i = 0; i < replys.Length; i++)
                {
                    list.Add(replys[i].GetHtml());
                }
                Replys = list.ToArray();
            }
        }

        private void InitHtml()
        {
            StreamReader sr = new StreamReader(Constant.CommentModelPath);
            _html = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
        private void setNick()
        {
            if (string.IsNullOrEmpty(Nick))
            {
                return;
            }
            _html = _html.Replace("~comment-nick~", Nick);
        }
        private void setTime()
        {
            if (string.IsNullOrEmpty(Time))
            {
                return;
            }
            _html = _html.Replace("~comment-time~", Time);
        }
        private void setContent()
        {
            if (string.IsNullOrEmpty(Content))
            {
                return;
            }
            _html = _html.Replace("~comment-content~", Content);
        }
        private void setReplys()
        {
            if (Replys == null || Replys.Length == 0)
            {
                _html = _html.Replace("~replys~", "");
                return;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Replys.Length; i++)
            {
                sb.Append(Replys[i]);
            }
            string replys = sb.ToString();
            _html = _html.Replace("~replys~", replys);
        }

        public string GetHtml()
        {
            setNick();
            setTime();
            setContent();
            setReplys();
            return _html;
        }
    }
    /// <summary>
    /// 页面内,项目模版
    /// </summary>
    class PageItem
    {
        private string _html;

        public string Content { get; set; }
        public string Nick { get; set; }
        public string Time { get; set; }
        public string[] Comments { get; set; }
        public string[] Images { get; set; }

        public PageItem()
        {
            InitHtml();
        }
        public PageItem(PageComment[] comments, PageImage[] images) : this()
        {
            List<string> list = new List<string>();
            if (comments == null)
            {
                Comments = null;
            }
            else
            {
                for (int i = 0; i < comments.Length; i++)
                {
                    list.Add(comments[i].GetHtml());
                }
                Comments = list.ToArray();
            }
            list.Clear();
            if (images == null)
            {
                Images = null;
            }
            else
            {
                for (int i = 0; i < images.Length; i++)
                {
                    list.Add(images[i].GetHtml());
                }
                Images = list.ToArray();
            }

        }

        private void InitHtml()
        {
            StreamReader sr = new StreamReader(Constant.ItemModelPath);
            _html = sr.ReadToEnd().Replace("\n", "").Replace("\t", "");
            sr.Close();
            sr.Dispose();
        }
        private void setImages()
        {
            if (Images == null || Images.Length == 0)
            {
                _html = _html.Replace("~item-images~", "");
                return;
            }
            StringBuilder sb = new StringBuilder("<div class=\"row\">");
            for (int i = 0; i < Images.Length; i++)
            {
                sb.Append(Images[i]);
            }
            sb.Append("</div>");
            string images = sb.ToString();
            _html = _html.Replace("~item-images~", images);
        }
        private void setComments()
        {
            if (Comments == null || Comments.Length == 0)
            {
                _html = _html.Replace("~item-comments~", "");
                return;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Comments.Length; i++)
            {
                sb.Append(Comments[i]);
            }
            string comments = sb.ToString();
            _html = _html.Replace("~item-comments~", comments);
        }
        private void setTime()
        {
            if (string.IsNullOrEmpty(Time))
            {
                return;
            }
            _html = _html.Replace("~item-time~", Time);
        }
        private void setNick()
        {
            if (string.IsNullOrEmpty(Nick))
            {
                return;
            }
            _html = _html.Replace("~item-nick~", Nick);
        }
        private void setContent()
        {
            if (string.IsNullOrEmpty(Content))
            {
                return;
            }
            _html = _html.Replace("~item-content~", Content);
        }

        public string GetHtml()
        {
            setContent();
            setNick();
            setTime();
            setComments();
            setImages();
            return _html;
        }
    }
    /// <summary>
    /// 页面模版
    /// </summary>
    class Page
    {
        private string _html;

        public int PageNum { get; set; }
        public int PageIndex { get; set; }
        public string Kind { get; set; }
        public string Capital { get; set; }
        public string Title { get; set; }
        public string Foot { get; set; }
        public string[] Items { get; set; }

        public Page(string kind, int pageIndex, int pageNum)
        {
            InitHtml();
            Kind = kind;
            PageNum = pageNum;
            PageIndex = pageIndex;
        }
        //public Page(string kind, int pageNum, string capital, string title,string foot, string[] items) : this(kind, pageNum)
        //{
        //    Capital = capital;
        //    Title = title;
        //    Items = items;
        //}
        public Page(string kind, int pageIndex, int pageNum, string capital, string title, string foot, PageItem[] items) : this(kind, pageIndex, pageNum)
        {
            Capital = capital;
            Title = title;
            Foot = foot;
            List<string> list = new List<string>();
            for (int i = 0; i < items.Length; i++)
            {
                list.Add(items[i].GetHtml());
            }
            Items = list.ToArray();
        }

        private void InitHtml()
        {
            StreamReader sr = new StreamReader(Constant.PageModelPath);
            _html = sr.ReadToEnd().Replace("\r", "").Replace("\n", "");
            sr.Close();
            sr.Dispose();
        }
        private void setItems()
        {
            if (Items == null || Items.Length == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Items.Length; i++)
            {
                sb.Append(Items[i]);
            }
            string items = sb.ToString();
            _html = _html.Replace("~items~", items);
            if (Kind == "m")
            {
                _html = _html.Replace("#-->", "").Replace("<!--~", "");
            }
        }
        private void setTitle()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return;
            }
            _html = _html.Replace("~title~", Title);
        }
        private void setCapital()
        {
            if (string.IsNullOrEmpty(Capital))
            {
                return;
            }
            _html = _html.Replace("~capital~", Capital);
        }
        private void setFootContent()
        {
            if (string.IsNullOrEmpty(Foot))
            {
                return;
            }
            _html = _html.Replace("~footContent~", Foot);
        }
        private void setPageNums()
        {
            StringBuilder sb = new StringBuilder();
            string home = Kind + "_1.html";
            //无变量
            string navHead = "<nav aria-label=\"Page navigation\"><ul class=\"pagination\">";
            sb.Append(navHead);
            //同类首页url
            string eleHead = $"<li><a href=\"{home}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>";
            sb.Append(eleHead);
            //当前页url,当前页是否active,当前页码
            int[] showPage = GetFivePage(PageIndex);
            for (int i = 0; i < showPage.Length; i++)
            {
                string active = " class=\"active\"";
                if (showPage[i] != PageIndex)
                {
                    active = "";
                }
                string cur = $"{Kind}_{showPage[i]}.html";
                string eleString = $"<li{active}><a href=\"{cur}\">{showPage[i].ToString()}</a></li>";
                sb.Append(eleString);
            }
            //同类尾页url
            string foot = $"{Kind}_{PageNum}.html";
            string eleFoot = $"<li><a href=\"{foot}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>";
            sb.Append(eleFoot);
            //当前类型
            string btnString = $"<div class=\"col-xs-4 col-sm-4 col-md-4  col-lg-4\"><div class=\"input-group\"><input class=\"form-control\" id=\"pnumber\" type=\"text\"><span class=\"input-group-btn\"><button type=\"button\" class=\"btn btn-default\" onclick=\"location.href='{Kind}_'+document.getElementById('pnumber').value+'.html';\">Go</button></span></div></div>";
            sb.Append(btnString);
            string navFoot = "</ul></nav>";
            sb.Append(navFoot);
            //组合完毕
            _html = _html.Replace("~pageNumbers~", sb.ToString());
        }
        private int[] GetFivePage(int index)
        {
            if (index < 3)
            {
                return new int[] { 1, 2, 3, 4, 5 };
            }
            if (index > PageNum - 2)
            {
                return new int[] { PageNum - 4, PageNum - 3, PageNum - 2, PageNum - 1, PageNum };
            }
            //3 ≤ index ≤ PageNum-2
            return new int[] { index - 2, index - 1, index, index + 1, index + 2 };
        }
        private void setKind()
        {
            string forChange = "";
            switch (Kind)
            {
                case "d":
                    forChange = "~dActive~";
                    break;
                case "c":
                    forChange = "~cActive~";
                    break;
                case "m":
                    forChange = "~mActive~";
                    break;
                default:
                    break;
            }
            if (forChange != "")
            {
                _html = _html.Replace(forChange, "active");
            }
        }

        public string GetHtml()
        {
            setKind();
            setPageNums();
            setCapital();
            setTitle();
            setFootContent();
            setItems();
            return _html;
        }
    }
}
