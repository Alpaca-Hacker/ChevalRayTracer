using System.Collections.Generic;
using System.Linq;
using Cheval.Models.Shapes;

namespace Cheval.DataStructure
{
    public class Intersection
    {
        public float T { get; set; }
        public Shape Object { get; set; }
        public float U { get; set; }
        public float V { get; set; }


        public Intersection(float t, Shape o, float u = 0, float v = 0)
        {
            T = t;
            Object = o;
            U = u;
            V = v;
        }

        public Intersection()
        {
            
        }

    }

    public class Intersections
    {
        public List<Intersection> List { get; set; }

        public Intersections()
        {
            List = new List<Intersection>();
        }
        public Intersections(List<Intersection> intersections)
        {
            List = intersections;
        }

        public Intersection Hit()
        {
            var result = List.Where(i => i.T >= 0)
                            .OrderBy(i => i.T)
                            .FirstOrDefault();
            return result;
        }
    }
}
