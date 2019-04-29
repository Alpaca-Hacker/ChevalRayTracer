using System;

namespace Cheval.Models
{
    public class ChevalColour 
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }

        public ChevalColour(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static bool operator ==(ChevalColour a, ChevalColour b)
        {
            if (a is null || b is null)
            {
                return false;

            }

            var isEqual = MathF.Abs(a.Red - b.Red) < Cheval.Epsilon
                           && MathF.Abs(a.Green - b.Green) < Cheval.Epsilon
                           && MathF.Abs(a.Blue - b.Blue) < Cheval.Epsilon;

            return isEqual;
        }

        public static bool operator !=(ChevalColour a, ChevalColour b)
        {

            return !(a == b);
        }


        protected bool Equals(ChevalColour other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ChevalColour)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Red.GetHashCode();
                hashCode = (hashCode * 697) ^ Green.GetHashCode();
                hashCode = (hashCode * 697) ^ Blue.GetHashCode();
                return hashCode;
            }
        }

        public static ChevalColour operator +(ChevalColour a, ChevalColour b)
        {
            var newRed = a.Red + b.Red;
            var newGreen = a.Green + b.Green;
            var newBlue = a.Blue + b.Blue;

            return new ChevalColour(newRed, newGreen, newBlue);
        }

        public static ChevalColour operator -(ChevalColour a, ChevalColour b)
        {
            var newRed = a.Red - b.Red;
            var newGreen = a.Green - b.Green;
            var newBlue = a.Blue - b.Blue;

            return new ChevalColour(newRed, newGreen, newBlue);
        }

        public static ChevalColour operator *(ChevalColour a, ChevalColour b)
        {
            var newRed = a.Red * b.Red;
            var newGreen = a.Green * b.Green;
            var newBlue = a.Blue * b.Blue;

            return new ChevalColour(newRed, newGreen, newBlue);
        }

        public static ChevalColour operator *(ChevalColour a, float b)
        {
            var newRed = a.Red * b;
            var newGreen = a.Green * b;
            var newBlue = a.Blue * b;

            return new ChevalColour(newRed, newGreen, newBlue);
        }
    }
}
