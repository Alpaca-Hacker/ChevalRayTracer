using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Cheval.Models.Primitives;

namespace Cheval.Models
{
    public class Ray
    {
        public ChevalPoint Origin { get; set; }
        public ChevalVector Direction { get; set; }

        public Ray(ChevalPoint origin, ChevalVector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public ChevalPoint Position(double t)
        {
            var position = (ChevalPoint)(Origin + Direction * t);
            return position;
        }

        public double[] Intersect(Sphere sphere)
        {
            var sphereToRay = Origin - sphere.Origin;
            var a = ChevalVector.Dot(Direction, Direction);
            var b = 2 * ChevalVector.Dot(Direction, sphereToRay);
            var c = ChevalVector.Dot(sphereToRay, sphereToRay)-1;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return new double[0];
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return new double[]{t1,t2};
        }
    }
}
