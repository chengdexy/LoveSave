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
        private static string ConnectionString = $"Provider=Microsoft.Jet.Oledb.4.0;Data Source={Environment.CurrentDirectory}\\Result\\Data.mdb";
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

        public static void Save2tbDiaryItem(int index, string name, DateTime time, string content, bool hasImage, bool hasComment)
        {
            string strsql = "insert into tbDiaryItem(i_index,i_name,i_time,i_content,i_hasImage,i_hasComment)" +
                           $" values({index},'{name}',#{time}#,'{content}',{hasImage},{hasComment})";
            ExecuteNonQuery(strsql);
        }

        public static void Save2tbDiaryImage(int index, string url, string filename)
        {
            string strsql = "insert into tbDiaryImage(img_index,img_url,img_filename)" +
                           $" values({index},'{url}','{filename}')";
            ExecuteNonQuery(strsql);
        }

        public static void Save2tbDiaryComment(int index, string commentName, DateTime commentTime, string commentContent)
        {
            string strsql = "insert into tbDiaryComment(c_index,c_name,c_time,c_content)" +
                           $" values({index},'{commentName}',#{commentTime }#,'{commentContent}')";
            ExecuteNonQuery(strsql);
        }

        public static void Save2tbChat(string name, DateTime time, string content, string imgUrl, bool hasImage, string fileName)
        {
            string strsql = "insert into tbChat(ct_name,ct_time,ct_content,ct_imgUrl,ct_hasImage,ct_fileName)" +
                           $" values('{name}',#{time}#,'{content}','{imgUrl}',{hasImage},'{fileName }')";
            ExecuteNonQuery(strsql);
        }
        #endregion
    }
}
