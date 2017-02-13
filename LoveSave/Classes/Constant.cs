using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public const string ResultPath = "Result\\";
        public const string ModelsPath = "Models\\";
        public const string PagesPath = "Result\\Pages\\";
        public const string DataModelPath = "Models\\DataModel.mdb";
        public const string IndexModelPath = "Models\\IndexModel.html";
        public const string PageModelPath = "Models\\PageModel.html";
        public const string ItemModelPath = "Models\\ItemModel.html";
        public const string CommentModelPath = "Models\\CommentModel.html";
        public const string DiaryImageDownloadPath = "Result\\DiaryImage\\";
        public const string ChatImageDownloadPath = "Result\\ChatImage\\";
        #endregion
        #region 固定格式方法

        #endregion
        #region Html页面相关
        public const int DiaryPageSize = 10;
        public const int ChatPageSize = 10;
        public const int MemosPageSize = 20;
        public const string FootStringDefault = "Made by Mr.X 2017";
        public const string A_BaseHtml = "<a href=\"{HTTP}\">{TEXT}</a>";
        public const string DiaryImageQueotePath = "..\\DiaryImage\\";
        #endregion
    }
}
