using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models.Shapes
{
    public class Sphere : Shape
    {
        protected override ChevalTuple LocalNormalAt(ChevalTuple point)
        {
            var objectNormal = Normalize(point - Point(0,0,0));
            objectNormal = Vector(objectNormal.X,objectNormal.Y,objectNormal.Z);

            return Normalize(objectNormal);
        }

        protected override List<Intersection> LocalIntersect(Ray ray)
        {
            var sphereToRay = ray.Origin - Point(0,0,0);
            var a = Dot(ray.Direction, ray.Direction);
            var b = 2 * Dot(ray.Direction, sphereToRay);
            var c = Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return new List<Intersection>();
            }

            var t1 = (-b - MathF.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + MathF.Sqrt(discriminant)) / (2 * a);
            return new List<Intersection>
            {
                new Intersection(t1, this),
                new Intersection(t2,this)
            };
        }
        public override BoundingBox Bounds()
        {
            var min = Point(-1, -1, -1);
            var max = Point(1, 1, 1);
            return new BoundingBox(min, max);
        }
    }
}
