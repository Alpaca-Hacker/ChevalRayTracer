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

            if (MathF.Abs(localRay.Direction.Y) < Cheval.Epsilon)
            {
                return new List<Intersection>();
            }

            var t = -localRay.Origin.Y / localRay.Direction.Y;

            return new List<Intersection>{new Intersection(t, this)};
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple point, Intersection hit = null)
        {
            return Vector(0,1,0);
        }

        public override BoundingBox Bounds()
        {
            var min = Point(float.NegativeInfinity, 0, float.NegativeInfinity);
            var max = Point(float.PositiveInfinity, 0, float.PositiveInfinity);
            return new BoundingBox(min, max);
        }
    }
}
