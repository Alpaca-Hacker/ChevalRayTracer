using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cheval.Models;
using Cheval.Models.Shapes;
using static System.MathF;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.DataStructure
{
    public class Computations
    {
        public float T { get; set; }
        public Shape Shape { get; set; }
        public ChevalTuple Point { get; set; }
        public ChevalTuple EyeV { get; set; }
        public ChevalTuple NormalV { get; set; }
        public ChevalTuple OverPoint { get; set; }
        public ChevalTuple UnderPoint { get; set; }
        public bool Inside { get; set; }
        public ChevalTuple ReflectV { get; set; }
        public float N1 { get; set; }
        public float N2 { get; set; }

        public Computations(Intersection intersection, Ray ray, Intersections xs = null)
        {
            T = intersection.T;
            Shape = intersection.Object;
            Point = ray.Position(T);
            EyeV = -ray.Direction;
            NormalV = Shape.NormalAt(Point);
            if (Dot(NormalV, EyeV) < 0)
            {
                Inside = true;
                NormalV = -NormalV;
            }

            OverPoint = Point + NormalV * Cheval.Epsilon;
            UnderPoint = Point - NormalV * Cheval.Epsilon;
            ReflectV = Reflect(ray.Direction, NormalV);
            var containers = new List<Shape>();

            if (xs == null) return;
            foreach (var inter in xs.List)
            {
                if (inter == intersection)
                {
                    N1 = containers.Count == 0 ? 1 :  
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
                N2 = containers.Count == 0 ? 1 : 
                    containers[containers.Count - 1].Material.RefractiveIndex;

                break;

            }
        }
        public float Schlick()
        {
            //OMG This looks confusing! 
            var cos = Dot(EyeV, NormalV);
            if (N1 > N2)
            {
                var n = N1 / N2;
                var sin2T = Pow(n, 2) * (1 - Pow(cos, 2));
                if (sin2T > 1.0)
                {
                    return 1;
                }

                var cosT = Sqrt(1f - sin2T);
                cos = cosT;
            }

            var r0 = Pow((N1 - N2) / (N1 + N2), 2);
            return r0 + Pow((1 - r0) * (1 - cos), 5);
        }
    }
}
