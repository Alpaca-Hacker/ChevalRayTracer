using System.Collections.Generic;
using System.Linq;
using Cheval.Models.Shapes;

namespace Cheval.DataStructure
{
    public class Intersection
    {
        public double T { get; set; }
        public Shape Object { get; set; }

        public Intersection(double t, Shape o)
        {
            T = t;
            Object = o;
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
