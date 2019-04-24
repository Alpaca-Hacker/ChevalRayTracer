using System;
using System.Collections.Generic;
using System.Text;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public class Cube : Shape
    {
        protected override List<Intersection> LocalIntersect(Ray localRay)
        {
            return new List<Intersection>();
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            throw new NotImplementedException();
        }

    }
}
