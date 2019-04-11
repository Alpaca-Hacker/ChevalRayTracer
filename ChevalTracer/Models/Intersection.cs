using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cheval.Models
{
    public class Intersection
    {
        public double T { get; set; }
        public object Object { get; set; }

        public Intersection(double t, object o)
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
