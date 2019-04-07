using System;
using Microsoft.VisualBasic.CompilerServices;

namespace Cheval_Tracer.Models
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
    }

}
