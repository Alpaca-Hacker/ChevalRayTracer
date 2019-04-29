using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static System.Double;
using static System.Math;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models.Shapes
{
    public class Cube : Shape
    {
        protected override List<Intersection> LocalIntersect(Ray localRay)
        {
            // localRay = localRay.Transform(InverseTransform);
            var xMinMax = CheckAxis(localRay.Origin.X, localRay.Direction.X);
            var yMinMax = CheckAxis(localRay.Origin.Y, localRay.Direction.Y);
            var zMinMax = CheckAxis(localRay.Origin.Z, localRay.Direction.Z);

            var tMin = Maximum(xMinMax.Item1, yMinMax.Item1, zMinMax.Item1);
            var tMax = Minimum(xMinMax.Item2, yMinMax.Item2, zMinMax.Item2);

            var result = new List<Intersection>();
            if (tMin < tMax)
            {
                result.Add(new Intersection(tMin, this));
                result.Add(new Intersection(tMax, this));
            }

            return result;
        }

        private double Minimum(double v1, double v2, double v3)
        {
            return Min(Min(v1, v2), v3);
        }

        private double Maximum(double v1, double v2, double v3)
        {
            return Max(Max(v1, v2), v3);
        }

        private Tuple<double, double> CheckAxis(double origin, double direction)
        {
            var tMinNumerator = (-1 - origin);
            var tMaxNumerator = (1 - origin);

            var tMin = tMinNumerator / direction;
            var tMax = tMaxNumerator / direction;

            if (tMin > tMax)
            {
                var temp = tMin;
                tMin = tMax;
                tMax = temp;
            }

            return new Tuple<double, double>(tMin, tMax);
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            var max = Maximum(Abs(localPoint.X), Abs(localPoint.Y), Abs(localPoint.Z));
            
             if (Abs(max - Abs(localPoint.X)) < Cheval.Epsilon) {
                 return Vector(localPoint.X, 0, 0);
             }

             if (Abs(max - Abs(localPoint.Y)) < Cheval.Epsilon)
             {
                 return Vector(0, localPoint.Y, 0);
             }

             return Vector(0,0, localPoint.Z);
        }

        public override BoundingBox Bounds()
        {
            var min = Point(-1, -1, -1);
            var max = Point(1, 1, 1);
            return new BoundingBox(min, max);
        }
    }
}
