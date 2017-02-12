using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    static class Constant
    {
        #region 正则表达式
        public const string splitRichvalToImageInfos = "(?<={).*?(?=})";                //(?<={).*?(?=})
        public const string findUrlInRichval = "(?<=url\":\").*?(?=\")";                //(?<="url":").*?(?=")
        public const string findLloclistInRichval = "(?<=\"lloclist\":\").*?(?=\")";    //(?<="lloclist":").*?(?=")
        public const string findTotal = "(?<=\"total\":)\\d*?(?=,)";           //(?<="total":)\d*?(?=,)
        public const string findPeerUin = "(?<=\"peeruin\":)\\d*?(?=,)";                //(?<="peeruin":)\d*?(?=,)
        #endregion
        #region 存储路径
        public const string DatabasePath = "Result\\";
        public const string DiaryImageDownloadPath = "Result\\DiaryImage\\";
        public const string ChatImageDownloadPath = "Result\\ChatImage\\";
        #endregion
        #region 固定格式方法

        #endregion
    }
}
