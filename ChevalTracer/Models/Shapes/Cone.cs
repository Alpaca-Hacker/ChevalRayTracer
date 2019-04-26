using System;
using System.Collections.Generic;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public class Cone: Shape
    {
        public double Minimum { get; set; } = double.NegativeInfinity;

        public double Maximum { get; set; } = double.PositiveInfinity;

        public bool IsClosed { get; set; } = false;

        private bool CheckCap(Ray ray, double t, double r)
        {
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;
            return (x * x + z * z) <= (r * r);
        }

        private void IntersectCaps(Ray ray, List<Intersection> xs)
        {
            if (!IsClosed)
            {
                return;
            }

            if (Math.Abs(ray.Direction.Y) < Cheval.Epsilon)
            {
                return;
            }

            var t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Minimum))
            {
                xs.Add(new Intersection(t, this));
            }

            t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Maximum))
            {
                xs.Add(new Intersection(t, this));
            }
        }

        protected override List<Intersection> LocalIntersect(Ray ray)
        {
            List<Intersection> xs;

            var o = ray.Origin;
            var d = ray.Direction;

            var a = (d.X * d.X) - (d.Y * d.Y) + (d.Z * d.Z);
            var b = (2 * o.X * d.X) - (2 * o.Y * d.Y) + (2 * o.Z * d.Z);
            var c = (o.X * o.X) - (o.Y * o.Y) + (o.Z * o.Z);

            if (Math.Abs(a) < Cheval.Epsilon)
            {
                if (Math.Abs(b) < Cheval.Epsilon)
                {
                    return new Intersections().List;
                }

                var t = -c / (2 * b);
                xs = new List<Intersection>
                {
                    new Intersection(t, this)
                };

                IntersectCaps(ray, xs);
                return new Intersections(xs).List;
            }

            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                return new Intersections().List;
            }

            var t0 = (-b - Math.Sqrt(disc)) / (2 * a);
            var t1 = (-b + Math.Sqrt(disc)) / (2 * a);

            if (t0 > t1)
            {
                var tmp = t0;
                t0 = t1;
                t1 = tmp;
            }

            xs = new List<Intersection>();

            var y0 = o.Y + t0 * d.Y;
            if (Minimum < y0 && y0 < Maximum)
            {
                xs.Add(new Intersection(t0, this));
            }

            var y1 = o.Y + t1 * d.Y;
            if (Minimum < y1 && y1 < Maximum)
            {
                xs.Add(new Intersection(t1, this));
            }

            IntersectCaps(ray, xs);
            return new Intersections(xs).List;
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple point)
        {
            var dist = point.X * point.X + point.Z * point.Z;
            if (dist < 1 && point.Y >= Maximum - Cheval.Epsilon)
            {
                return ChevalTuple.Vector(0, 1, 0);
            }

            if (dist < 1 && point.Y <= Minimum + Cheval.Epsilon)
            {
                return ChevalTuple.Vector(0, -1, 0);
            }

            var y = Math.Sqrt(dist);
            if (point.Y > 0)
            {
                y = -y;
            }

            return ChevalTuple.Vector(point.X, y, point.Z);
        }

    }
}
