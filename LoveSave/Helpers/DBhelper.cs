using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    static class DBhelper
    {
        #region 静态字段
        private static string ConnectionString = $"Provider=Microsoft.Jet.Oledb.4.0;Data Source=Result\\Data.mdb";
        private static OleDbConnection cnxn = new OleDbConnection();
        private static OleDbCommand cmd = new OleDbCommand();
        #endregion

        #region 私有方法
        private static void OpenCnxn()
        {
            if (cnxn.State == ConnectionState.Closed)
            {
                cnxn.ConnectionString = ConnectionString;
                cnxn.Open();
            }
        }
        private static void ExecuteNonQuery(string strsql)
        {
            OpenCnxn();

            cmd.Connection = cnxn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql;
            cmd.ExecuteNonQuery();
        }
        private static OleDbDataReader ExecuteDataReader(string strsql)
        {
            OpenCnxn();

            cmd.Connection = cnxn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql;
            return cmd.ExecuteReader();
        }
        #endregion

        #region 公共方法
        public static void CloseCnxn()
        {
            if (cnxn.State != ConnectionState.Closed)
            {
                cnxn.Close();
                cnxn.Dispose();
                cmd.Dispose();
            }
        }
        //Chat's
        public static void Save2tbChat(string name, DateTime time, string content, string imgUrl, bool hasImage, string fileName)
        {
            string strsql = "insert into tbChat(ct_name,ct_time,ct_content,ct_imgUrl,ct_hasImage,ct_fileName)" +
                           $" values('{name}',#{time}#,'{content}','{imgUrl}',{hasImage},'{fileName }')";
            ExecuteNonQuery(strsql);
        }
        //Memos's
        public static void Save2tbMemos(string name, int year, int month, int day, bool lunar)
        {
            string strsql = "insert into tbMemos(m_name,m_year,m_month,m_day,m_lunar)" +
                           $" values('{name}',{year},{month},{day},{lunar})";
            ExecuteNonQuery(strsql);
        }
        //Diary's
        public static void Save2tbDiaryItem(string gid, string content, string nick, DateTime date, string qq, bool hasComment, bool hasImage)
        {
            string strsql = "insert into tbDiaryItem(i_gid,i_content,i_nick,i_date,i_qq,i_hasComment,i_hasImage)" +
                           $" values('{gid}','{content}','{nick}',#{date}#,'{qq}',{hasComment},{hasImage})";
            ExecuteNonQuery(strsql);
        }
        public static void Save2tbDiaryComment(string gid, string diaryGid, string content, string nick, string qq, DateTime date, bool hasReply)
        {
            string strsql = "insert into tbDiaryComment(c_gid,c_diaryGid,c_content,c_nick,c_qq,c_date,c_hasReply)" +
                           $" values('{gid}','{diaryGid}','{content}','{nick}','{qq}',#{date}#,{hasReply})";
            ExecuteNonQuery(strsql);
        }
        public static void Save2tbDiaryReply(int index, string commentGid, string content, string nick, string qq, DateTime date)
        {
            string strsql = "insert into tbDiaryReply(r_index,r_commentGid,r_content,r_nick,r_qq,r_date)" +
                           $" values({index},'{commentGid}','{content}','{nick}','{qq}',#{date}#)";
            ExecuteNonQuery(strsql);
        }
        public static void Save2tbDiaryImage(string diaryGid, string url, string fileName)
        {
            string strsql = "insert into tbDiaryImage(img_diaryGid,img_url,img_filename)" +
                           $" values('{diaryGid}','{url}','{fileName}')";
            ExecuteNonQuery(strsql);
        }
        #endregion
    }
}
