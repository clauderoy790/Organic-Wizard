using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Vector = System.Windows.Vector;

namespace Shared
{
    public static class Maths
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
                min = max;
            value = Math.Max(value,min);
            value = Math.Min(value,max);
            return value;
        }

        public  static double Angle(Vector v1, Vector v2)
        {
            return Vector.AngleBetween(v1, v2);
        }

        public static double Distance(Vector v1, Vector v2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((v2.X - v1.X), 2) + Math.Pow((v2.Y - v1.Y), 2)), 1);
        }

        public static Vector Translate(Vector v,Vector axis,double length)
        {
            if (axis.Length == 0)
                throw new ArgumentException("Translate axis cannot be of zero length.");
                
            axis.Normalize();
            Vector translation = axis*length;
            Vector translated = v + translation;
            return translated;
        }
    }
}