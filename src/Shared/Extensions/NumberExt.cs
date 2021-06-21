using NLog.LayoutRenderers.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Shared
{
    public static class NumberExt
    {
        #region double
        public static int ToInt(this double val)
        {
            return Convert.ToInt32(val);
        }

        public static bool Within(this double val,double diff)
        {
            return (val >= val-diff) && (val <= val+diff);
        }

        public static int RoundToInt(this double val)
        {
            return Convert.ToInt32(Math.Round(val));
        }
        #endregion

        #region int

        public static bool Within(this int val, int diff)
        {
            return (val >= val - diff) && (val <= val + diff);
        }

        #endregion
    }
}
