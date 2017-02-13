using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    class PageHelper
    {
        private void CreateHtml(Page page)
        {
            string html = page.GetHtml();
            string fileName = $"{page.Kind}_{page.PageIndex}.html";
            StreamWriter sw = new StreamWriter(Constant.PagesPath + fileName);
            sw.Write(html);
            sw.Close();
            sw.Dispose();
        }
        private PageItem[] ToPageItems(List<Diary> list)
        {
            List<PageItem> pList = new List<PageItem>();
            list.ForEach(diary =>
            {
                PageItem item = new PageItem(DCommentsToPComments(diary.Comments), DImagesToPImages(diary.ImageInfos))
                {
                    Nick = diary.Nick,
                    Time = diary.Date.ToString("yyyy-MM-dd hh:mm"),
                    Content = diary.Content
                };
                pList.Add(item);
            });
            return pList.ToArray();
        }
        private PageItem[] ToPageItems(List<Memos> list)
        {
            List<PageItem> pList = new List<PageItem>();
            list.ForEach(memos =>
            {
                PageItem item = new PageItem(null, null)
                {
                    Nick = memos.Name,
                    Time = memos.Date,
                    Content = $"几经{memos.DaysPast}天"
                };
                pList.Add(item);
            });
            return pList.ToArray();
        }
        private PageItem[] ToPageItems(List<ChatItem> list)
        {
            List<PageItem> pList = new List<PageItem>();
            list.ForEach(chat =>
            {
                PageItem item = new PageItem(null, null)
                {
                    Nick = chat.Name,
                    Time = chat.Time.ToString("yyyy-MM-dd hh:mm"),
                    Content = chat.Content
                };
                pList.Add(item);
            });
            return pList.ToArray();
        }
        private void CreatePage(List<Memos> list)
        {
            int yu = list.Count % Constant.MemosPageSize;
            int shang = list.Count / Constant.MemosPageSize;
            int pageCount = yu == 0 ? shang : shang + 1;
            PageItem[] pis = ToPageItems(list);
            for (int p = 0; p < pageCount; p++)
            {
                string temp = $"Memories page {p + 1}/{pageCount}";
                Page newPage = new Page(
                    "m",
                    p + 1,
                    pageCount,
                    "LoveSave::Memories",
                    temp,
                    Constant.FootStringDefault,
                    ArrayBreak(pis, p + 1, Constant.MemosPageSize)
                    );
                CreateHtml(newPage);
            }
        }
        private void CreatePage(List<ChatItem> list)
        {
            int yu = list.Count % Constant.ChatPageSize;
            int shang = list.Count / Constant.ChatPageSize;
            int pageCount = yu == 0 ? shang : shang + 1;
            PageItem[] pis = ToPageItems(list);
            for (int p = 0; p < pageCount; p++)
            {
                string temp = $"Chats page {p + 1}/{pageCount}";
                Page newPage = new Page(
                    "c",
                    p + 1,
                    pageCount,
                    "LoveSave::Chats",
                    temp,
                    Constant.FootStringDefault,
                    ArrayBreak(pis, p + 1, Constant.ChatPageSize)
                    );
                CreateHtml(newPage);
            }
        }
        private void CreatePage(List<Diary> list)
        {
            int yu = list.Count % Constant.DiaryPageSize;
            int shang = list.Count / Constant.DiaryPageSize;
            int pageCount = yu == 0 ? shang : shang + 1;
            PageItem[] pis = ToPageItems(list);
            for (int p = 0; p < pageCount; p++)
            {
                string temp = $"Diaries page {p + 1}/{pageCount}";
                Page newPage = new Page(
                    "d",
                    p + 1,
                    pageCount,
                    "LoveSave::Diaries",
                    temp,
                    Constant.FootStringDefault,
                    ArrayBreak(pis, p + 1, Constant.DiaryPageSize)
                    );
                CreateHtml(newPage);
            }
        }
        private PageImage[] DImagesToPImages(ImageInfo[] imageInfos)
        {
            if (imageInfos == null)
            {
                return null;
            }
            List<PageImage> pList = new List<PageImage>();
            imageInfos.ToList().ForEach(image =>
            {
                PageImage item = new PageImage(Constant.DiaryImageQueotePath + image.FileName);
                pList.Add(item);
            });
            return pList.ToArray();
        }
        private PageComment[] DCommentsToPComments(Comment[] comments)
        {
            if (comments == null)
            {
                return null;
            }
            List<PageComment> pList = new List<PageComment>();
            comments.ToList().ForEach(comment =>
            {
                PageComment item = new PageComment(DReplysToPReplys(comment.Replys))
                {
                    Nick = comment.Nick,
                    Time = comment.Date.ToString("yyyy-MM-dd hh:mm"),
                    Content = comment.Content
                };
                pList.Add(item);
            });
            return pList.ToArray();
        }
        private PageReply[] DReplysToPReplys(Reply[] replys)
        {
            if (replys == null)
            {
                return null;
            }
            List<PageReply> pList = new List<PageReply>();
            replys.ToList().ForEach(reply =>
            {
                PageReply item = new LoveSave.PageReply(
                    reply.Nick,
                    reply.Date.ToString("yyyy-MM-dd hh:mm"),
                    reply.Content
                    );
                pList.Add(item);
            });
            return pList.ToArray();
        }

        private void CreateHome(int dcount, int ccount, int mcount)
        {
            StreamReader sr = new StreamReader(Constant.IndexModelPath);
            string html = sr.ReadToEnd().Replace("~dCount~", dcount.ToString()).Replace("~cCount~", ccount.ToString()).Replace("~mCount~", mcount.ToString());
            sr.Close();
            sr.Dispose();
            StreamWriter sw = new StreamWriter(Constant.ResultPath + "Index.html");
            sw.Write(html);
            sw.Close();
            sw.Dispose();
        }
        public void CreateAll(List<Diary> dList, List<ChatItem> cList, List<Memos> mList)
        {
            CreateHome(dList.Count, cList.Count, mList.Count);
            CreatePage(dList);
            CreatePage(cList);
            CreatePage(mList);
        }

        private T[] ArrayBreak<T>(T[] ts, int page, int size)
        {
            int begin = (page - 1) * size;
            int end = page * size - 1;
            if (end > ts.Length - 1)
            {
                end = ts.Length - 1;
            }
            List<T> list = new List<T>();
            for (int i = begin; i < +end; i++)
            {
                list.Add(ts[i]);
            }
            return list.ToArray();
        }
    }
}
