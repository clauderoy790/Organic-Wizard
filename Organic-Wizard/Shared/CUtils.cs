using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Shared
{
    public static class CUtils
    {
        public static int ColorDiff(Color col1, Color col2)
        {
            ColorFormulas oColor1 = new ColorFormulas(col1.R, col1.G, col2.B);
            ColorFormulas oColor2 = new ColorFormulas(col2.R, col2.G, col2.B);
            return oColor1.CompareTo(oColor2);
        }

        public static bool IsSimilarColor(PointGroup group, int tolerance = 5)
        {
            if (group == null || group.Points == null || group.Points.Count < 2)
                return false;
            bool result = true;
            var points = group.Points.ToList();

            int x, x2, y, y2 = 0;
            x = x2 = points[0].X;
            y = y2 = points[0].Y;

            foreach (var point in points)
            {
                if (point.X < x)
                    x = point.X;
                if (point.X > x2)
                    x2 = point.X;
                if (point.Y < y)
                    y = point.Y;
                if (point.Y > y2)
                    y2 = point.Y;
            }

            int width = (x2 - x) + 1;
            int height = (y2 - y) + 1;
            DirectBitmap bmp = new DirectBitmap(width, height);
            Rectangle bounds = new Rectangle(x, y, width, height);
            try
            {
                using (Graphics g = Graphics.FromImage(bmp.Bitmap))
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                for (int i = 1; i < points.Count; i++)
                {
                    Point previousPoint = points[i - 1];
                    Point currentPoint = points[i];

                    Color previousCol = bmp.GetPixel(previousPoint.X - x, previousPoint.Y - y);
                    Color currentCol = bmp.GetPixel(currentPoint.X - x, currentPoint.Y - y);
                    int diff = ColorDiff(previousCol, currentCol);
                    if (diff > tolerance)
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }


            return result;
        }

        public static Color GetColorAt(Point p)
        {
            return GetColorAt(p.X, p.Y);
        }

        public static int GetColorAtInt(Point p)
        {
            return GetColorAsInt(GetColorAt(p));
        }

        public static Color GetColorAt(int x, int y)
        {
            Color col = Color.Empty;
            DirectBitmap bmp = new DirectBitmap(1, 1);
            Rectangle bounds = new Rectangle(x, y, 1, 1);
            try
            {
                using (Graphics g = Graphics.FromImage(bmp.Bitmap))
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                col = bmp.GetPixel(0, 0);
            }
            catch
            {
                col = Color.Empty;
            }
            
            return col;
        }

        public static int GetColorAsInt(int x, int y)
        {
            return GetColorAsInt(GetColorAt(x, y));
        }

        public static int GetColorAsInt(Color col)
        {
            string hexColor = GetHexColor(col);
            return Convert.ToInt32(hexColor, 16);
        }

        public static Color FromRGB(int nb)
        {
            var col = Color.FromArgb(nb);
            return Color.FromArgb(255, col);
        }

        public static Color GetColorFromInt(int col)
        {
            return Color.FromArgb(col);
        }

        public static string GetHexColor(Color c)
        {
            return "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static string RGBConverter(Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        public static Color GetHexColorFromString(string hex)
        {
            Color col = Color.Empty;

            if (!string.IsNullOrEmpty(hex))
            {
                try
                {
                    col = ColorTranslator.FromHtml(hex);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(string.Format("Error formatting color: {0}",ex.Message));
                }
            }
            
            return col;
        }
    }
}
