using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    static class DateHelper
    {
        public static DateTime StampToDate(long stamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(stamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        public static long DateToStamp(DateTime date, bool secIsOk)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            if (secIsOk)
            {
                return (long)(date - startTime).TotalSeconds;

            }
            else
            {
                return (long)(date - startTime).TotalMilliseconds;
            }
        }
    }
}
