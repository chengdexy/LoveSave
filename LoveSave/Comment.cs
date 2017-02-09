using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    class Comment
    {
        private string _gid;
        private string _diaryGid;
        private string _content;
        private string _nick;
        private string _uin;
        private long _time;
        private int _reply_total;
        private Reply[] _replys;

        public string Gid
        {
            get
            {
                return _gid;
            }
        }
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
        public DateTime Date
        {
            get
            {
                return DateHelper.StampToDate(_time);
            }

            set
            {
                _time = DateHelper.DateToStamp(value);
            }
        }
        public bool hasReply
        {
            get
            {
                return _reply_total != 0;
            }

            set
            {
                _reply_total = value ? 1 : 0;
            }
        }
        public Reply[] Replys
        {
            get
            {
                return _replys;
            }

            set
            {
                _replys = value;
            }
        }


        public Comment(JToken jComment, string diaryGid)
        {
            _gid = Guid.NewGuid().ToString("N");
            _diaryGid = diaryGid;
            _content = jComment["content"].ToString();
            _nick = jComment["nick"].ToString();
            _uin = jComment["uin"].ToString();
            _time = Convert.ToInt64(jComment["time"].ToString());
            _reply_total = Convert.ToInt32(jComment["reply_total"].ToString());
            if (hasReply)
            {
                _replys = GetReplys(jComment["replys"], Gid);
            }
            else
            {
                _replys = null;
            }
        }

        private Reply[] GetReplys(JToken jReplys, string gid)
        {
            List<Reply> list = new List<Reply>();
            foreach (var jReply in jReplys)
            {
                list.Add(new Reply(jReply, gid));
            }
            return list.ToArray();

        }

        public void Save()
        {
            DBhelper.Save2tbDiaryComment(Gid, DiaryGid, Content, Nick, QQ, Date, hasReply);
            if (hasReply)
            {
                for (int i = 0; i < Replys.Length; i++)
                {
                    Replys[i].Save();
                }
            }
        }
    }
}
