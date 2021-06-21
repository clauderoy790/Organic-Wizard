using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Point = System.Drawing.Point;

namespace Shared
{
    public class PointGroup
    {
        protected List<Point> _points;

        public List<Point> Points { get { return _points; } }

        public int Count { get { return _points.Count; } }

        public PointGroup()
        {
            _points = new List<Point>();
        }

        public PointGroup(params Point[] points) : this()
        {
            if (points == null)
                throw new ArgumentException("Points cannot be null.");

            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                if (_points.Contains(point))
                    throw new ArgumentException($"Cannot have duplicate of the same point. X:{point.X}, Y:{point.Y}");
                _points.Add(point);
            }
        }

        public void AddPoint(Point p)
        {
            _points.Add(p);
        }

        public PointGroup OffsetCopy(Point point)
        {
            return OffsetCopy(point.X, point.Y);
        }

        public PointGroup OffsetCopy(int x, int y)
        {
            PointGroup points = new PointGroup();

            foreach (var point in _points)
            {
                Point newPoint = new Point(point.X + x, point.Y + y);
                points.AddPoint(newPoint);
            }

            return points;
        }

        public void Offset(int x, int y)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Point(_points[i].X + x, _points[i].Y + y);
            }
        }

        public void Offset(Point point)
        {
            Offset(point.X, point.Y);
        }

        public void OffsetX(int x)
        {
            Offset(x, 0);
        }

        public void OffsetY(int y)
        {
            Offset(0, y);
        }

        public bool SameColor(PointGroup points, int tolerance = 5)
        {
            if (points == null || points.Count == 0)
                return false;

            int length = Math.Min(points.Count, this.Count);
            List<double> diffs = new List<double>();
            for (int i = 0; i < length; ++i)
            {
                Point p1 = _points[i];
                Point p2 = points[i];
                int diff = ColorUtils.ColorDiff(p1, p2);
                diffs.Add(diff);
            }
            double avg = 0;
            diffs.ForEach((diff) => { avg += diff; });
            avg = avg / diffs.Count;
            return avg <= tolerance;
        }

        public Point this[int index]
        {
            get
            {
                if (index < 0 && index >= _points.Count)
                    throw new InvalidOperationException("Bad index");

                return _points[index];
            }
        }
    }
}
