using System;

namespace Cheval.DataStructure
{
    public class ChevalTuple
    {

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public bool IsPoint => Math.Abs(W - 1.0) < Cheval.Epsilon;
        public bool IsVector => Math.Abs(W) < Cheval.Epsilon;

        public ChevalTuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static ChevalTuple Point(double x, double y, double z)
        {
            return new ChevalTuple(x, y, z, 1.0);
        }

        public static ChevalTuple Vector(double x, double y, double z)
        {
            return new ChevalTuple(x, y, z, 0.0);
        }

        public static double Magnitude(ChevalTuple vector)
        {
            return Math.Sqrt(vector.X * vector.X
                             + vector.Y * vector.Y
                             + vector.Z * vector.Z);
        }

        public static ChevalTuple Normalize(ChevalTuple vector)
        {
            var mag = Magnitude(vector);
            var newX = vector.X / mag;
            var newY = vector.Y / mag;
            var newZ = vector.Z / mag;

            return Vector(newX, newY, newZ);
        }

        public static double Dot(ChevalTuple a, ChevalTuple b)
        {
            var result = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);

            return result;
        }

        public static ChevalTuple Cross(ChevalTuple a, ChevalTuple b)
        {
            return Vector(a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);
        }
        public static ChevalTuple Reflect(ChevalTuple vectorIn, ChevalTuple vectorNormal)
        {
            return vectorIn - vectorNormal * 2 * Dot(vectorIn, vectorNormal);
        }

        #region Operators

        public static bool operator ==(ChevalTuple a, ChevalTuple b)
        {
            if (a is null || b is null)
            {
                return false;

            }

            var isEqual = (Math.Abs(a.X - b.X) < Cheval.Epsilon
                           && Math.Abs(a.Y - b.Y) < Cheval.Epsilon
                           && Math.Abs(a.Z - b.Z) < Cheval.Epsilon
                           && a.W.Equals(b.W));

            return isEqual;
        }

        public static bool operator !=(ChevalTuple a, ChevalTuple b)
        {

            return !(a == b);
        }


        protected bool Equals(ChevalTuple other)
        {
            return this == other;
            // return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChevalTuple) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public static ChevalTuple operator +(ChevalTuple a, ChevalTuple b)
        {
            var newX = a.X + b.X;
            var newY = a.Y + b.Y;
            var newZ = a.Z + b.Z;
            var newW = a.W + b.W;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator -(ChevalTuple a, ChevalTuple b)
        {
            var newX = a.X - b.X;
            var newY = a.Y - b.Y;
            var newZ = a.Z - b.Z;
            var newW = a.W - b.W;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator -(ChevalTuple a)
        {

            var newX = -a.X;
            var newY = -a.Y;
            var newZ = -a.Z;
            var newW = -a.W;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator *(ChevalTuple t, double x)
        {
            var newX = t.X * x;
            var newY = t.Y * x;
            var newZ = t.Z * x;
            var newW = t.W * x;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator *(double x, ChevalTuple t)
        {
            var newX = t.X * x;
            var newY = t.Y * x;
            var newZ = t.Z * x;
            var newW = t.W * x;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator *(ChevalTuple x, ChevalTuple y)
        {
            var newX = x.X * y.X;
            var newY = x.Y * y.Y;
            var newZ = x.Z * y.Z;
            var newW = x.W * y.Z;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        public static ChevalTuple operator /(ChevalTuple t, double x)
        {
            var newX = t.X / x;
            var newY = t.Y / x;
            var newZ = t.Z / x;
            var newW = t.W / x;

            return new ChevalTuple(newX, newY, newZ, newW);
        }

        #endregion


    }

}
