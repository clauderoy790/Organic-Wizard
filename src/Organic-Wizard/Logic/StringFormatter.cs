using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Organic_Wizard
{
    public static class StringFormatter
    {
        public static bool TryConvertToInt(string str,out int nb)
        {
            nb = 0;
            if (string.IsNullOrEmpty(str))
                return false;

            bool valid = true;
            str = str.ToLower().Trim();
            nb = 0;
            str = ProcessString(str);
            valid = int.TryParse(str, out nb);

            return valid;
        }

        private static string ProcessString(string str)
        {
            foreach (var entry in stringToReplace)
            {
                str = str.Replace(entry.Key, entry.Value);
            }
            return str.Trim();
        }

        private static Dictionary<string, string> stringToReplace = new Dictionary<string, string>()
        {
            { "o","0" },
            { "l","1" },
            { "i","1" },
            { "t","1" },
            { "!","1" },
            { " ","" },
        };
    }
}
