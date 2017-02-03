using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoveSaveDo
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HttpWebRequest request;
            string filename = "";
            request = WebRequest.Create("http://group.store.qq.com/sweet/V108jIYd1MvmdO/V2to5eFNr4RUVdQ*4Mn/670") as HttpWebRequest;//picpath,图片地址
            request.Method = "GET";
            request.Timeout = 30000;
            request.AllowAutoRedirect = true;
            request.ContentType = "image/bmp";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:11.0) Gecko/20100101 Firefox/11.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Bitmap sourcebm = new Bitmap(sr.BaseStream);
                sr.Close();
                sourcebm.Save($"d:\\{filename}.jpg");//filename 保存地址
            }
        }
    }
}
