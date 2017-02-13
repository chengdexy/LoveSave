using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LoveSave
{
    class Reply
    {
        private int _id;
        private string _commentGid;
        private string _content;
        private string _nick;
        private string _uin;
        private long _time;

        public int Index
        {
            get
            {
                return _id;
            }
        }
        public string CommentGid
        {
            get
            {
                return _commentGid;
            }

            set
            {
                _commentGid = value;
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
                _time = DateHelper.DateToStamp(value,true );
            }
        }


        public Reply(JToken jReply, string commentgid)
        {
            _id = Convert.ToInt32(jReply["id"].ToString());
            _commentGid = commentgid;
            _content = jReply["content"].ToString();
            _nick = jReply["nick"].ToString();
            _uin = jReply["uin"].ToString();
            _time = Convert.ToInt64(jReply["time"].ToString());
        }

        public void Save()
        {
            DBhelper.Save2tbDiaryReply(Index, CommentGid, Content, Nick, QQ, Date);
        }
    }
}
