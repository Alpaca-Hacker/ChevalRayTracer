using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cheval.Models
{
    public class ChevalColour : ChevalTuple
    {
        public double Red
        {
            get => X;
            set => X = value;
        }

        public double Green
        {
            get => Y;
            set => Y = value;
        }

        public double Blue
        {
            get => Z;
            set => Z = value;
        }

        public ChevalColour(double red, double green, double blue) : base(red, green, blue, double.NaN)
        {
        }
    }
}
