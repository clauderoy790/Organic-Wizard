using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Shared
{
    public class PointGroup
    {
        HashSet<Point> _points;

        public HashSet<Point> Points { get { return _points; } }

        public PointGroup()
        {
            _points = new HashSet<Point>();
        }

        public PointGroup(params Point[] points) : this()
        {
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    _points.Add(points[i]);
                }
            }
        }

        public void AddPoint(Point p)
        {
            _points.Add(p);
        }
    }
}
