using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace Shared
{
    public static class PointExt
    {
        public static Point New(int x,int y)
        {
            return new Point(x, y);
        }

        public static Vector ToVector(this Point p)
        {
            return new Vector(p.X, p.Y);
        }

        public static bool IsZero(this Point p)
        {
            return p.X == 0 && p.Y == 0;
        }
    }
}
