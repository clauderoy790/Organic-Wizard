using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;

namespace Shared
{
    public class PointGroupColorDiff : PointGroup, IDisposable
    {
        const int NONE = -1;
        List<ColorDiffInfo> diffs;
        Int32Rect rect;
        LockBitmap lockB;
        int tolerance;

        public LockBitmap LockB { get { return LockB; } }
        public int Tolerance { get { return tolerance; } set { tolerance = Math.Max(0, value); } }

        public PointGroupColorDiff(Int32Rect rect, params Point[] points) : base(points)
        {
            this.rect = rect;
            diffs = new List<ColorDiffInfo>();
        }

        public Point GetRelativePoint(Point point)
        {
            Point relative = new Point(point.X - rect.X, point.Y - rect.Y);
            if (!isInBounds(relative))
                throw new ArgumentException("The passed point is invalid, it is not in bound of the PointGroup rect");

            return relative;
        }

        public void AddDiff(int first, int second, int diff)
        {
            AddDiff(first, second, diff, NONE);
        }

        public void AddDiff(int first, int second, int diff, int tolerance)
        {
            if (diff < 0)
                throw new InvalidOperationException("Diff cannot be smaller than zero.");

            if (first < 0 || second < 0)
                throw new InvalidOperationException("Index cannot be smaller than zero.");

            if (first >= Points.Count || second >= Points.Count)
                throw new InvalidOperationException($"Index cannot be great or equal than {Points.Count}.");

            if (tolerance < 0 && tolerance != NONE)
                throw new InvalidOperationException($"Tolerance cannot be less than zero. {tolerance}");

            var existing = diffs.FindAll((d) => { return d.First == first && d.Second == second; });
            if (existing.Count > 0)
            {
                throw new InvalidOperationException("Cannot add the same diff multiple times");
            }

            diffs.Add(new ColorDiffInfo(first, second, diff, tolerance));
        }

        public void AddDiff(int index, Color color, int diff)
        {
            if (index < 0 || index >= Points.Count)
                throw new InvalidOperationException("Invalid index.");

            diffs.Add(new ColorDiffInfo(index, color, diff));
        }

        public bool AreInBounds()
        {
            foreach (var point in Points)
            {
                if (!isInBounds(point))
                {
                    return false;
                }
            }

            return true;
        }

        private bool isInBounds(Point point)
        {
            return point.X >= 0 && point.X < rect.Width && point.Y >= 0 && point.Y < rect.Height;
        }

        public Point FindExactPoint()
        {
            InitBitmapIfNull();

            for (int x = 0; x < rect.Width; ++x)
            {
                for (int y = 0; y < rect.Height; ++y)
                {
                    if (AreInBounds() && ExactColorDiffs())
                    {
                        return new Point(rect.X + x, rect.Y + y);
                    }
                    OffsetY(1);
                }
                Offset(1, -rect.Height);
            }
            return new Point(0, 0);
        }

        public bool FindExact()
        {
            return !FindExactPoint().IsZero();
        }

        public bool ExactColorDiffs()
        {
            InitBitmapIfNull();

            bool isExact = true;
            List<Color> colors = new List<Color>();
            for (int i = 0; i < Points.Count; i++)
            {
                colors.Add(Color.Empty);
            }

            foreach (var diffInfo in diffs)
            {
                if (colors[diffInfo.First] == Color.Empty)
                {
                    colors[diffInfo.First] = lockB.GetPixel(Points[diffInfo.First].X, Points[diffInfo.First].Y);
                }
                if (diffInfo.Second != NONE && colors[diffInfo.Second] == Color.Empty)
                {
                    colors[diffInfo.Second] = lockB.GetPixel(Points[diffInfo.Second].X, Points[diffInfo.Second].Y);
                }

                int diff = int.MaxValue;
                if (diffInfo.Second == NONE)
                {
                    diff = ColorUtils.ColorDiff(colors[diffInfo.First], diffInfo.Color);
                }
                else
                {
                    diff = ColorUtils.ColorDiff(colors[diffInfo.First], colors[diffInfo.Second]);
                }

                diff = Math.Abs(diff - diffInfo.Diff);
                int tol = diffInfo.Tolerance == NONE ? tolerance : diffInfo.Tolerance;
                if (diff > tol)
                {
                    isExact = false;
                    break;
                }
            }
            return isExact;
        }

        private void InitBitmapIfNull()
        {
            if (lockB == null)
            {
                lockB = ImgUtils.GetLockBitmap(rect);
                lockB.LockBits();
            }
        }

        public void Dispose()
        {
            if (lockB != null)
            {
                lockB.UnlockBits();
                lockB.Dispose();
            }
            lockB = null;
        }

        private struct ColorDiffInfo
        {
            public ColorDiffInfo(int first, int second, int diff, int tolerance) : this(first, second, diff)
            {
                this.Tolerance = tolerance;
            }
            public ColorDiffInfo(int first, Color color, int diff, int tolerance) : this(first, color, diff)
            {
                this.Tolerance = tolerance;
            }

            public ColorDiffInfo(int first, int second, int diff)
            {
                this.Tolerance = NONE;
                this.First = first;
                this.Second = second;
                this.Diff = diff;
                this.Color = Color.Empty;
            }

            public ColorDiffInfo(int first, Color color, int diff)
            {
                this.Tolerance = NONE;
                this.First = first;
                this.Second = NONE;
                this.Color = color;
                this.Diff = diff;
            }
            public int Tolerance { get; set; }
            public int First { get; set; }

            public Color Color { get; set; }
            public int Second { get; set; }
            public int Diff { get; set; }
        }
    }
}
