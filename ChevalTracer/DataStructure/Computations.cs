using System.Collections.Generic;
using Cheval.Models;
using Cheval.Models.Shapes;

namespace Cheval.DataStructure
{
    public class Computations
    {
        public double T { get; set; }
        public Shape Shape { get; set; }
        public ChevalTuple Point { get; set; }
        public ChevalTuple EyeV { get; set; }
        public ChevalTuple NormalV { get; set; }
        public ChevalTuple OverPoint { get; set; }
        public ChevalTuple UnderPoint { get; set; }
        public bool Inside { get; set; }
        public ChevalTuple ReflectV { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }

        public Computations(Intersection intersection, Ray ray, Intersections xs = null)
        {
            T = intersection.T;
            Shape = intersection.Object;
            Point = ray.Position(T);
            EyeV = -ray.Direction;
            NormalV = Shape.NormalAt(Point);
            if (ChevalTuple.Dot(NormalV, EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }

            OverPoint = Point + NormalV * Cheval.Epsilon;
            UnderPoint = Point - NormalV * Cheval.Epsilon;
            ReflectV = ChevalTuple.Reflect(ray.Direction, NormalV);
            var containers = new List<Shape>();

            if (xs == null) return;
            foreach (var inter in xs.List)
            {
                if (inter == intersection)
                {
                    N1 = containers.Count == 0 ? 1.0 :  
                        containers[containers.Count - 1].Material.RefractiveIndex;
                }

                if (containers.Contains(inter.Object))
                {
                    containers.Remove(inter.Object);
                }
                else
                {
                    containers.Add(inter.Object);
                }

                if (inter != intersection) continue;
                N2 = containers.Count == 0 ? 1.0 : 
                    containers[containers.Count - 1].Material.RefractiveIndex;

                break;

            }
        }

       
    }
}
