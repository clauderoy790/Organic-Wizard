using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Encoding.ASCII.GetBytes(str);

            return new byte[0];
        }
    }
}
