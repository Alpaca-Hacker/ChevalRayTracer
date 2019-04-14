using System.Drawing;
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
        public bool Inside { get; set; }

        public Computations(Intersection intersection, Ray ray)
        {
            T = intersection.T;
            Shape = intersection.Object;
            Point = ray.Position(T);
            EyeV = -ray.Direction;
            NormalV = Shape.NormalAt(Point);
            if (ChevalTuple.Dot(NormalV, EyeV) < 0){
                Inside = true;
                NormalV = -NormalV;
            }

            OverPoint = Point + NormalV * Cheval.Epsilon;
        }

    }
}
