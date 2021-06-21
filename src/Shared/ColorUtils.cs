using System;
using System.Collections.Generic;
using System.Drawing;

using Vector = System.Windows.Vector;

namespace Shared
{
    public static class ColorUtils
    {
        const int DEFAULT_COLOR_TOLERANCE = 5;

        public static int ColorDiff(Color col1, Color col2)
        {
            Color temp = Color.Empty;
            if (col2.R < col1.R)
            {
                temp = col2;
            }
            else if (col2.R == col1.R && col2.G < col1.G)
            {
                temp = col2;
            }
            else if (col2.R == col1.R && col2.G == col1.G && col2.B < col1.B)
            {
                temp = col2;
            }

            if (temp != Color.Empty)
            {
                col2 = col1;
                col1 = temp;
            }

            ColorFormulas oColor1 = new ColorFormulas(col1.R, col1.G, col2.B);
            ColorFormulas oColor2 = new ColorFormulas(col2.R, col2.G, col2.B);
            return oColor1.CompareTo(oColor2);
        }

        public static int ColorDiff(Point p1, Point p2)
        {
            Color col1 = GetColorAt(p1);
            Color col2 = GetColorAt(p2);
            return ColorDiff(col1, col2);
        }

        public static int ColorDiff(PointGroup group)
        {
            return ColorDiffWithColor(group, Color.Empty);
        }

        public static int ColorDiffWithColor(PointGroup group, Color color)
        {
            if (group == null || group.Points.Count < 2)
                return 0;

            double diff = 0;
            for (int i = 1; i < group.Points.Count; i++)
            {
                Point p1 = group.Points[i];
                Point p2 = group.Points[i - 1];
                diff += ColorDiff(p1, p2);
            }

            return (int)Math.Round(diff / group.Points.Count);
        }

        public static bool IsSimilarColor(PointGroup group, int tolerance = DEFAULT_COLOR_TOLERANCE)
        {
            return ColorDiff(group) <= tolerance;
        }

        public static bool IsSimilarColor(Color c1, Color c2, int tolerance = DEFAULT_COLOR_TOLERANCE)
        {
            return ColorDiff(c1, c2) <= tolerance;
        }

        public static int GetAverageDiff(List<Color> colors)
        {
            if (colors == null || colors.Count < 2)
                return 0;

            double diff = 0;
            for (int i = 1; i < colors.Count; i++)
            {
                diff += ColorDiff(colors[i], colors[i - 1]);
            }

            return (int)Math.Round(diff / (colors.Count - 1));
        }

        public static List<Color> GetColors(LockBitmap lockb, PointGroup pg)
        {
            List<Color> colors = new List<Color>();

            if (!lockb.Locked)
                throw new InvalidOperationException("Bits must be locked to get colors.");

            foreach (var point in pg.Points)
            {
                if (point.X < 0 || point.X >= lockb.Width)
                    throw new InvalidOperationException($"Invalid point x {point.X} must be between 0 and {lockb.Width}");
                else if (point.Y < 0 || point.Y >= lockb.Width)
                    throw new InvalidOperationException($"Invalid point x {point.Y} must be between 0 and {lockb.Height}");

                Color col = lockb.GetPixel(point.X, point.Y);
                colors.Add(col);
            }

            return colors;
        }

        public static int GetAverageDiff(List<Color> colors, Color compare)
        {
            if (colors == null || colors.Count < 2)
                return 0;

            if (compare == Color.Empty)
                compare = colors[0];

            double diff = 0;
            foreach (var color in colors)
            {
                diff += ColorDiff(color, compare);
            }

            return (int)Math.Round(diff / colors.Count);
        }

        public static Color GetAverageColor(List<Color> colors)
        {
            if (colors == null || colors.Count == 0)
                throw new ArgumentException("color list must cotain at least one color.");

            int r, g, b;
            r = g = b = 0;

            foreach (var color in colors)
            {
                r += color.R;
                g += color.G;
                b += color.B;
            }

            r /= colors.Count;
            g /= colors.Count;
            b /= colors.Count;

            return Color.FromArgb(r, g, b);
        }

        public static Color GetColorAt(Point p)
        {
            return GetColorAt(p.X, p.Y);
        }

        public static Color GetColorAt(Vector v)
        {
            if (v.X % 1 != 0)
                throw new ArgumentException("Invalid Vector, X must be an integer number. " + v.X);
            else if (v.Y % 1 != 0)
                throw new ArgumentException("Invalid Vector, Y must be an integer number. " + v.Y);

            return GetColorAt(v.X.ToInt(), v.Y.ToInt());
        }

        public static Color GetColorAt(int x, int y)
        {
            Color col = Color.Empty;
            Rectangle bounds = new Rectangle(x, y, 1, 1);
            try
            {
                using (Bitmap bmp = new Bitmap(1, 1))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    col = bmp.GetPixel(0, 0);
                }
            }
            catch
            {
                col = Color.Empty;
            }

            return col;
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
                    if (!hex.StartsWith("#"))
                        hex = "#" + hex;
                    col = ColorTranslator.FromHtml(hex);
                }
                catch
                {
                }
            }

            return col;
        }

        public class PointInfo
        {
            public Point Point { get; set; }
            public Color Color { get; set; }
        }
    }
}
