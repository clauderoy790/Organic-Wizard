using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class IntUtils
    {
        public static bool  Between(this int nb,int min,int max)
        {
            return nb >= min && nb <= max;
        }
        public static bool Between(this int nb, int variation)
        {
            return nb >= nb + variation && nb <= nb - variation;
        }
    }
}
