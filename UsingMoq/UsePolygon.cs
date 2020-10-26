using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UsingMoq
{

    public class Polygon
    {
        Point[] _points;
        List<LineSegment> _segments;

        public Polygon(params Point[] points)
        {
            if (points[0] == points[points.Length - 1])
            {
                _points = points;
            }
            else
            {
                _points = points.Concat(new Point[] { points[0] }).ToArray();
                // auto-close the polygon
            }

            _segments = new List<LineSegment>();
            for (int i = 0; i < _points.Length; i++)
            {
                if (i != _points.Length - 1)
                {
                    _segments.Add(new LineSegment(_points[i], _points[i + 1]));
                }
            }
        }

        public bool PointInPolygon(Point point)
        {
            Ray ray = new Ray(point, new Point(point.X + 1, point.Y + 1));
            int counter = 0;
            foreach (LineSegment segment in _segments)
            {
                if (segment.IntersectsWithRay(ray))
                {
                    counter++;
                }
            }
            return counter % 2 == 1;
        }
    }
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class LineSegment
    {
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public LineSegment(Point p1, Point p2)
        {
            Point1 = p1;
            Point2 = p2;
        }

        public bool IntersectsWithRay(Ray ray)
        {
            Line raySupport = new Line(ray.Origin, ray.AlsoGoesThrough);
            Line segmentSupport = new Line(Point1, Point2);

            Point intersection = raySupport.IntersectionWithOtherLine(segmentSupport);
            if (intersection == null) return false;

            bool intersectionOnRay = !((ray.Direction.Item1 == 1 && intersection.Y < ray.Origin.Y) || (ray.Direction.Item1 == -1 && intersection.Y > ray.Origin.Y) ||
                (ray.Direction.Item2 == 1 && intersection.X < ray.Origin.X) || (ray.Direction.Item1 == -1 && intersection.X > ray.Origin.X));

            double segmentMinX = Math.Min(Point1.X, Point2.X);
            double segmentMaxX = Math.Max(Point1.X, Point2.X);
            double segmentMinY = Math.Min(Point1.Y, Point2.Y);
            double segmentMaxY = Math.Max(Point1.Y, Point2.Y);
            bool intersectionOnSegment = intersection.X >= segmentMinX && intersection.X <= segmentMaxX && intersection.Y >= segmentMinY && intersection.Y <= segmentMaxY;

            return intersectionOnRay && intersectionOnSegment;
        }
    }

    public class Ray
    {
        public Point Origin { get; private set; }
        public Point AlsoGoesThrough { get; private set; }

        public Tuple<int, int> Direction { get; private set; }
        // Item1 specifies up (1) or down (-1), Item2 specifies right (1) or left (-1)

        public Ray(Point origin, Point alsoGoesThrough)
        {
            Origin = origin;
            AlsoGoesThrough = alsoGoesThrough;

            int directionUp;
            if (origin.Y == alsoGoesThrough.Y)
            {
                directionUp = 0;
            }
            else if (origin.Y > alsoGoesThrough.Y)
            {
                directionUp = -1;
            }
            else // origin.Y < alsoGoesThrough.Y
            {
                directionUp = 1;
            }

            int directionRight;
            if (origin.X == alsoGoesThrough.X)
            {
                directionRight = 0;
            }
            else if (origin.X > alsoGoesThrough.X)
            {
                directionRight = -1;
            }
            else // origin.X < alsoGoesThrough.X
            {
                directionRight = 1;
            }

            Direction = new Tuple<int, int>(directionUp, directionRight);
        }
    }

    public class Line
    {
        public double Slope { get; private set; }
        public double YOfIntersectionWithYAxis { get; private set; }

        public Line(Point goesThrough1, Point goesThrough2)
        {
            Slope = (goesThrough1.Y - goesThrough2.Y) / (goesThrough1.X - goesThrough2.X);
            YOfIntersectionWithYAxis = goesThrough1.Y - Slope * goesThrough1.X;
        }

        public Point IntersectionWithOtherLine(Line other)
        {
            if (Slope == other.Slope) return null;

            double intersectionX = (other.YOfIntersectionWithYAxis - YOfIntersectionWithYAxis) / (Slope - other.Slope);
            double intersectionY = Slope * intersectionX + YOfIntersectionWithYAxis;
            return new Point(intersectionX, intersectionY);
        }
    }
}
