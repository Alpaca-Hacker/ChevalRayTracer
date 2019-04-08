using System;

namespace Cheval.Models
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
                           && Math.Abs(a.W - b.W) < Cheval.Epsilon);

            return isEqual;
        }

        public static bool operator !=(ChevalTuple a, ChevalTuple b)
        {

            return !(a == b);
        }


        protected bool Equals(ChevalTuple other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChevalTuple)obj);
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

            return CreateReturnTuple(newW, newX, newY, newZ);
        }

        public static ChevalTuple operator -(ChevalTuple a, ChevalTuple b)
        {
            var newX = a.X - b.X;
            var newY = a.Y - b.Y;
            var newZ = a.Z - b.Z;
            var newW = a.W - b.W;

            return CreateReturnTuple(newW, newX, newY, newZ);
        }

        public static ChevalTuple operator -(ChevalTuple a)
        {

            var newX = -a.X;
            var newY = -a.Y;
            var newZ = -a.Z;
            var newW = -a.W;

            return CreateReturnTuple(newW, newX, newY, newZ);
        }
        public static ChevalTuple operator *(ChevalTuple t, double x)
        {
            var newX = t.X * x;
            var newY = t.Y * x;
            var newZ = t.Z * x;
            var newW = t.W * x;

            return CreateReturnTuple(newW, newX, newY, newZ);
        }
        public static ChevalTuple operator *(double x, ChevalTuple t)
        {
            var newX = t.X * x;
            var newY = t.Y * x;
            var newZ = t.Z * x;
            var newW = t.W * x;

            return CreateReturnTuple(newW, newX, newY, newZ);
        }

        public static ChevalTuple operator /(ChevalTuple t, double x)
        {
            var newX = t.X / x;
            var newY = t.Y / x;
            var newZ = t.Z / x;
            var newW = t.W / x;

            return CreateReturnTuple(newW, newX, newY, newZ);
        }

        private static ChevalTuple CreateReturnTuple(double newW, double newX, double newY, double newZ)
        {
            switch (newW)
            {
                case 0.0:
                {
                    return new ChevalVector(newX, newY, newZ);
                }
                case 1.0:
                {
                    return new ChevalPoint(newX, newY, newZ);
                }
                default:
                {
                    return new ChevalTuple(newX, newY, newZ, newW);
                }
            }
        }

        #endregion
    }

}
