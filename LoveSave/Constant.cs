using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    static class Constant
    {
        public const string findUrlInRichval = "<?<=\"url\":\">.*?<?=\">";  //<?<="url":">.*?<?=">
        public const string findLloclistInRichval = "<?<=\"lloclist\":\">.*?<?=\">"; //<?<="lloclist":">.*?<?=">
        public const string splitRichvalToImageInfos = "<?<={>.*?<?=}>";  //<?<={>.*?<?=}>

        public const string ImageDownloadPath = "Result\\DiaryImage\\";
    }
}
