using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using Cheval.DataStructure;
using static System.Math;

namespace Cheval.Models.Shapes
{
    public class Cylinder : Shape
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Closed { get; set; }

        public Cylinder()
        {
            Minimum = double.NegativeInfinity;
            Maximum = double.PositiveInfinity;
        }
        protected override List<Intersection> LocalIntersect(Ray localRay)
        {
            var result = new List<Intersection>();
            var a = Pow(localRay.Direction.X, 2) + Pow(localRay.Direction.Z, 2);
            // # ray is parallel to the y axis
            if (Abs(a) > Cheval.Epsilon)
            {
                var b = 2 * localRay.Origin.X * localRay.Direction.X +
                        2 * localRay.Origin.Z * localRay.Direction.Z;

                var c = Pow(localRay.Origin.X, 2) + Pow(localRay.Origin.Z, 2) - 1;
                var disc = Pow(b, 2) - (4 * a * c);
                // # ray does not intersect the cylinder
                if (disc < 0)
                {
                    return result;
                }

                var t0 = (-b - Sqrt(disc)) / (2 * a);
                var t1 = (-b + Sqrt(disc)) / (2 * a);
                if (t0 > t1)
                {
                    var temp = t0;
                    t0 = t1;
                    t1 = temp;
                }

                var y0 = localRay.Origin.Y + t0 * localRay.Direction.Y;
                if (Minimum < y0 && y0 < Maximum)
                {
                    result.Add(new Intersection(t0, this));
                }

                var y1 = localRay.Origin.Y + t1 * localRay.Direction.Y;
                if (Minimum < y1 && y1 < Maximum)
                {
                    result.Add(new Intersection(t1, this));
                }
            }
            
            IntersectCaps(localRay, result);

            return result;
        }

        private bool CheckCap(Ray ray, double t)
        {
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;

            return Pow(x, 2) + Pow(z, 2) <= 1;
        }

        private void IntersectCaps(Ray ray, List<Intersection> xs)
        {
            if (!Closed || Abs(ray.Direction.Y) < Cheval.Epsilon)
            {
                return;
            }

            var t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }

            t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            var dist = Pow(localPoint.X, 2) + Pow(localPoint.Z, 2);
            if (dist < 1 && localPoint.Y >= Maximum - Cheval.Epsilon)
            {
                return ChevalTuple.Vector(0,1,0);
            }
            if (dist < 1 && localPoint.Y <= Minimum + Cheval.Epsilon)
            {
                return ChevalTuple.Vector(0, -1, 0);
            }

            return ChevalTuple.Vector(localPoint.X, 0, localPoint.Z);
        }
    }
}
