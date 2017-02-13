using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    class Diary
    {
        #region 字段
        private string _gid;            //Guid
        private string _content;        //内容
        private string _nick;           //昵称
        private long _time;             //时间戳
        private string _uin;            //QQ号
        private int _com_total;         //包含评论数(0为不包含,非0即包含,值并不准确)
        private Comment[] _comments;    //评论对象数组
        private string _richval;        //图片信息字符串,解析后就是图片
        #endregion
        #region 属性
        /// <summary>
        /// 识别串
        /// </summary>
        public string Gid
        {
            get
            {
                return _gid;
            }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
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
        /// <summary>
        /// 日志发布者昵称
        /// </summary>
        public string Nick
        {
            get
            {
                return _nick;
            }

            set
            {
                _nick = value;
            }
        }
        /// <summary>
        /// 日志发布时间
        /// </summary>
        public DateTime Date
        {
            get
            {
                return DateHelper.StampToDate(_time);
            }

            set
            {
                _time = DateHelper.DateToStamp(value,true );
            }
        }
        /// <summary>
        /// 日志发布者QQ号码
        /// </summary>
        public string QQ
        {
            get
            {
                return _uin;
            }

            set
            {
                _uin = value;
            }
        }
        /// <summary>
        /// 日志是否包含评论
        /// </summary>
        public bool hasComment
        {
            get
            {
                return _com_total != 0;
            }

            set
            {
                _com_total = value ? 1 : 0;
            }
        }
        /// <summary>
        /// 所包含的评论.无评论时为null
        /// </summary>
        public Comment[] Comments
        {
            get
            {
                return _comments;
            }

            set
            {
                _comments = value;
            }
        }
        /// <summary>
        /// 所包含的图片信息.无图片时为null
        /// </summary>
        public ImageInfo[] ImageInfos
        {
            get
            {
                return GetImageInfo(_richval, _gid);
            }
        }
        #endregion
        #region 构造
        /// <summary>
        /// 解析Json对象时调用的构造函数
        /// </summary>
        /// <param name="jDiary"></param>
        public Diary(JToken jDiary)
        {
            _gid = Guid.NewGuid().ToString("N");
            _content = jDiary["content"].ToString();
            _nick = jDiary["nick"].ToString();
            _uin = jDiary["uin"].ToString();
            _time = Convert.ToInt64(jDiary["time"].ToString());
            _com_total = Convert.ToInt32(jDiary["com_total"].ToString());
            if (hasComment)
            {
                _comments = GetComments(jDiary["comments"], Gid);
            }
            else
            {
                _comments = null;
            }
            _richval = jDiary["richval"].ToString();
        }
        #endregion
        #region 私有方法
        private ImageInfo[] GetImageInfo(string _richval, string gid)
        {
            List<ImageInfo> list = new List<ImageInfo>();
            string[] richvals = RegexHelper.GetMatches(_richval, Constant.splitRichvalToImageInfos);
            if (richvals != null)
            {
                for (int i = 0; i < richvals.Length; i++)
                {
                    list.Add(new ImageInfo(richvals[i], gid));
                }
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        private Comment[] GetComments(JToken jComments, string gid)
        {
            List<Comment> list = new List<Comment>();
            foreach (var jComment in jComments)
            {
                list.Add(new Comment(jComment, gid));
            }
            return list.ToArray();
        }
        #endregion
        #region 公共方法
        public void Save()
        {
            bool hasImage = ImageInfos == null ? false : true;
            DBhelper.Save2tbDiaryItem(Gid, Content, Nick, Date, QQ, hasComment, hasImage);
            if (hasImage)
            {
                for (int i = 0; i < ImageInfos.Length; i++)
                {
                    ImageInfos[i].Save();
                    ImageInfos[i].Download();
                }
            }
            if (hasComment)
            {
                for (int i = 0; i < Comments.Length; i++)
                {
                    Comments[i].Save();
                }
            }
        }
        #endregion
    }
}
