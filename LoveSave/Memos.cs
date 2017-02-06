using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveSave
{
    class Memos
    {
        private string _name;
        private string _date;
        private bool _lunar;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        public string Date
        {
            get
            {
                string year = _date.Substring(0, 4);
                string month = _date.Substring(5, 2);
                string day = _date.Substring(8, 2);
                if (Lunar)
                {
                    //是农历
                    if (Convert.ToInt32(month) == 1)
                    {
                        //是1月
                        month = "正";
                    }
                    else if (Convert.ToInt32(month) == 12)
                    {
                        //是12月
                        month = "腊月";
                    }
                    return $"农历{year}年{month}月{day}日";
                }
                else
                {
                    return $"公历{year}年{month}月{day}日";
                }
            }
        }
        public bool Lunar
        {
            get
            {
                return _lunar;
            }

            set
            {
                _lunar = value;
            }
        }
        public DateTime Time
        {
            get
            {
                int year = Convert.ToInt32(_date.Substring(0, 4));
                int month = Convert.ToInt32(_date.Substring(5, 2));
                int day = Convert.ToInt32(_date.Substring(8, 2));
                DateTime dt;
                if (Lunar)
                {
                    //是农历
                    ChineseLunisolarCalendar clc = new ChineseLunisolarCalendar();
                    dt = clc.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
                else
                {
                    dt = new DateTime(year, month, day);
                }
                return dt;
            }
        }
        public long DaysPast
        {
            get
            {
                return (DateTime.Today - Time).Days;
            }
        }

        public Memos(string name, int year, int month, int day, bool lunar)
        {
            _name = name;
            Lunar = lunar;
            _date = $"{year.ToString("0000")}-{month.ToString("00")}-{day.ToString("00")}";
        }

        public void SaveToDatabase()
        {
            int year = Convert.ToInt32(_date.Substring(0, 4));
            int month = Convert.ToInt32(_date.Substring(5, 2));
            int day = Convert.ToInt32(_date.Substring(8, 2));

            DBhelper.Save2tbMemos(Name, year, month, day, Lunar);
        }
    }
}
