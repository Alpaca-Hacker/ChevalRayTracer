using System;
using System.Collections.Generic;
using Cheval.DataStructure;

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
            return ChevalTuple.Vector(0,1,0);
        }
    }
}
