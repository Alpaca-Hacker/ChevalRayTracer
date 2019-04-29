using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models.Shapes
{
    public class Plane: Shape
    {
        protected override List<Intersection> LocalIntersect(Ray localRay)
        {

            if (Math.Abs(localRay.Direction.Y) < Cheval.Epsilon)
            {
                return new List<Intersection>();
            }

            var t = -localRay.Origin.Y / localRay.Direction.Y;

            return new List<Intersection>{new Intersection(t, this)};
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            return Vector(0,1,0);
        }

        public override BoundingBox Bounds()
        {
            var min = Point(double.NegativeInfinity, 0, double.NegativeInfinity);
            var max = Point(double.PositiveInfinity, 0, double.PositiveInfinity);
            return new BoundingBox(min, max);
        }
    }
}
