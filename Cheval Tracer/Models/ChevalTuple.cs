using System;

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


    }

}
