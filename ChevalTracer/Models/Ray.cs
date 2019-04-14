using System.Collections.Generic;
using System.Linq;
using Cheval.DataStructure;

namespace Cheval.Models
{
    public class Ray
    {
        public ChevalTuple Origin { get; set; }
        public ChevalTuple Direction { get; set; }

        public Ray(ChevalTuple origin, ChevalTuple direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public ChevalTuple Position(double t)
        {
            var position = (Origin + Direction * t);
            return position;
        }

        public List<Intersection> Intersect(Scene scene)
        {
            var xs = new List<Intersection>();
            foreach (var shape in scene.Shapes)
            {
                var objectXs = shape.Intersect(this);
                xs.AddRange(objectXs);
            }

            return xs.OrderBy(x => x.T).ToList();
        }

        public Ray Transform(Matrix matrix)
        {
            var newOrigin = Origin * matrix;
            var newDirection = Direction * matrix;

            return new Ray(newOrigin, newDirection);
        }
    }
}
