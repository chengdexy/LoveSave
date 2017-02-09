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
        #endregion
        #region 存储路径
        public const string DiaryImageDownloadPath = "Result\\DiaryImage\\";
        #endregion
    }
}
