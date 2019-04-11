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

        public List<Intersection> Intersect(Sphere sphere)
        {
            var ray2 = Transform(Matrix.Inverse(sphere.Transform));

            var sphereToRay = ray2.Origin - sphere.Origin;
            var a = ChevalVector.Dot(ray2.Direction, ray2.Direction);
            var b = 2 * ChevalVector.Dot(ray2.Direction, sphereToRay);
            var c = ChevalVector.Dot(sphereToRay, sphereToRay)-1;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return new List<Intersection>();
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return new List<Intersection>
            {
                new Intersection(t1, sphere),
                new Intersection(t2,sphere)
            };
        }

        public Ray Transform(Matrix matrix)
        {
            var newOrigin = (ChevalPoint) (Origin * matrix);
            var newDirection = (ChevalVector) (Direction * matrix);

            return new Ray(newOrigin, newDirection);
        }
    }
}
