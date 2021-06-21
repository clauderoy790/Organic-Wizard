using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Int32Rect = System.Windows.Int32Rect;
using System.Windows.Forms;

namespace Shared.Image_Processing
{
    public class DiffFinder
    {
        Dictionary<Tuple<Point, Point>, int> pointDiffs;
        Dictionary<Tuple<Point, Color>, int> colorDiffs;
        Rectangle rect;
        PointGroup group;

        public DiffFinder(int x, int y, int width, int height)
        {
            pointDiffs = new Dictionary<Tuple<Point, Point>, int>();
            colorDiffs = new Dictionary<Tuple<Point, Color>, int>();
            rect = new Rectangle(x, y, width, height);
            group = new PointGroup();
        }

        public void AddDiff(Point p1, Point p2, int diff)
        {
            if (pointDiffs.ContainsKey(new Tuple<Point, Point>(p1, p2)))
                return;
            Point point1 = new Point(rect.X + p1.X, rect.Y + p1.Y);
            Point point2 = new Point(rect.X + p2.X, rect.Y + p2.Y);
            if (rect.Contains(point1) && rect.Contains(point2) && diff >= 0)
            {
                pointDiffs.Add(new Tuple<Point, Point>(p1, p2), diff);

                if (!group.Points.Contains(p1))
                    group.AddPoint(p1);

                if (!group.Points.Contains(p2))
                    group.AddPoint(p2);
            }
        }

        public void AddDiff(Point point, Color color, int diff)
        {
            if (!rect.Contains(GetAbsolutePoint(point)) || colorDiffs.ContainsKey(new Tuple<Point, Color>(point, color)))
                return;

            if (!group.Points.Contains(point))
                group.AddPoint(point);

            colorDiffs.Add(new Tuple<Point, Color>(point, color), diff);
        }

        public List<Point> ProcessImage(int maxEncounters = 1)
        {
            List<Point> points = new List<Point>();
            int encounters = 0;
            PointGroup pointGroup = group.OffsetCopy(0, 0);
            LockBitmap lockB = ImgUtils.GetLockBitmap(rect);
            lockB.LockBits();
            Point fartherDownRight = GetFartherPoint();
            int x = 0;


            while (encounters < maxEncounters && (fartherDownRight.X + x) < lockB.Width)
            {
                int y = 0;
                while (encounters < maxEncounters && (fartherDownRight.Y + y) < lockB.Height)
                {
                    bool valid = true;
                    foreach(var pointDiff in pointDiffs)
                    {
                        Point p1 = GetCorrespondingPoint(pointGroup, pointDiff.Key.Item1,x,y);
                        Point p2 = GetCorrespondingPoint(pointGroup, pointDiff.Key.Item2,x,y);
                        Color c1 = lockB.GetPixel(p1.X,p1.Y);
                        Color c2 = lockB.GetPixel(p2.X,p2.Y);
                        int diff = ColorUtils.ColorDiff(c1, c2);
                        if (diff != pointDiff.Value)
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid) // Check color diffs
                    {
                        foreach (var colorDiff in colorDiffs)
                        {
                            foreach (var point in pointGroup.Points)
                            {
                                Point relative = new Point(point.X - x, point.Y - y);
                                if (relative == colorDiff.Key.Item1)
                                {
                                    int diff = ColorUtils.ColorDiff(lockB.GetPixel(point.X, point.Y), colorDiff.Key.Item2);
                                    if (diff != colorDiff.Value)
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (valid)
                    {
                        encounters++;
                        points.Add(new Point(rect.X + x, rect.Y + y));
                    }

                    pointGroup.OffsetY(1);
                    ++y;
                }
                pointGroup.Offset(1, -y);
                ++x;
            }
            lockB.UnlockBits();


            return points;
        }

        private Point GetCorrespondingPoint(PointGroup currentGroup, Point originalPoint, int currentX, int currentY)
        {
            Point corresponding = new Point(-1,-1);

            foreach(var point in currentGroup.Points)
            {
                if (point.X-currentX == originalPoint.X && point.Y-currentY == originalPoint.Y)
                {
                    corresponding = point;
                    break;
                }
            }

            if (corresponding.X == -1 && corresponding.Y == -1)
                throw new Exception("Failed to find corresponding point");
            return corresponding;
        }

        private Point GetAbsolutePoint(Point point)
        {
            return new Point(rect.X + point.X, rect.Y + point.Y);
        }

        private Point GetFartherPoint()
        {
            Point pt = new Point(0, 0);

            foreach (var point in group.Points)
            {
                if (point.X > pt.X)
                {
                    pt.X = point.X;
                }
                if (point.Y > pt.Y)
                {
                    pt.Y = point.Y;
                }
            }

            return pt;
        }
    }
}
