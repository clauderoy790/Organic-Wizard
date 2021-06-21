using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Vector = System.Windows.Vector;
using Point = System.Drawing.Point;
using NLog.LayoutRenderers.Wrappers;

namespace Shared
{
    public static class VecExt
    {
        private const double DegToRad = Math.PI / 180;
        private const double RadToDeg = 180 / Math.PI;

        public static Vector Rotate(this Vector v, double degrees)
        {
            Vector3 axis = new Vector3(0, 0, -1);
            Quaternion quat = Quaternion.CreateFromAxisAngle(axis, (float)(degrees * DegToRad));

            Vector3 vec3 = new Vector3((float)v.X, (float)v.Y, 0);
            vec3 = Vector3.Transform(vec3, quat);
            Vector result = new Vector(Math.Round(vec3.X, 5), Math.Round(vec3.Y, 5));
            return result;
        }

        public static Vector ToIntVector(this Vector v)
        {
            return new Vector((int)Math.Round(v.X), (int)Math.Round(v.Y)); 
        }

        public static bool IsZero(this Vector v)
        {
            return (v.X == 0 && v.Y == 0);
        }

        public static Point ToPoint(this Vector v)
        {
            return new Point(v.X.RoundToInt(), v.Y.RoundToInt());
        } 
    }
}
