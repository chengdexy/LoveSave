using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoveSave
{
    static class RegexHelper
    {
        public static string GetMatch(string resourceStr, string regularExpression)
        {
            if (Regex.IsMatch(resourceStr, regularExpression))
            {
                return Regex.Match(resourceStr, regularExpression).Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string[] GetMatches(string resourceStr, string regularExpression)
        {
            if (Regex.IsMatch(resourceStr, regularExpression))
            {
                List<string> list = new List<string>();
                foreach (Match match in Regex.Matches(resourceStr, regularExpression))
                {
                    list.Add(match.Value);
                }
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
