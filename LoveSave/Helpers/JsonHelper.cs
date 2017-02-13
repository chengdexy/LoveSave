using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    static class JsonHelper
    {
        public static List<JToken> JsonToObjectCollection(string jsonFilePath, string selectToken, Encoding encoding)
        {
            StreamReader sr = new StreamReader(jsonFilePath, encoding);
            string strJson = sr.ReadToEnd();
            JObject jo = JObject.Parse(strJson);
            return jo.SelectToken(selectToken).Select(p => p).ToList();
        }


    }
}
