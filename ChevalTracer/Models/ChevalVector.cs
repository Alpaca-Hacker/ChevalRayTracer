using System;

namespace Cheval.Models
{
    public class ChevalVector: ChevalTuple
    {
        public ChevalVector(double x, double y, double z) : base(x, y, z, 0.0)
        {
        }

        public static double Magnitude(ChevalVector vector)
        {
            return Math.Sqrt(vector.X * vector.X 
                             + vector.Y * vector.Y 
                             + vector.Z * vector.Z);
        }

        public static ChevalVector Normalize(ChevalVector vector)
        {
            var mag = Magnitude(vector);
            var newX = vector.X / mag;
            var newY = vector.Y / mag;
            var newZ = vector.Z / mag;

            return new ChevalVector(newX, newY, newZ);
        }

        public static double Dot(ChevalTuple a, ChevalTuple b)
        {
            var result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);

            return result;
        }

        public static ChevalVector Cross(ChevalVector a, ChevalVector b)
        {
            return new ChevalVector(a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);
        }


    }
}
