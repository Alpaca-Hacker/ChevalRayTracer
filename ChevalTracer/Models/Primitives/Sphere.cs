using System;

namespace Cheval.Models.Primitives
{
    public class Sphere
    {

        public Guid Id { get; }
        public ChevalPoint Origin { get; set; }
        public double Size { get; set; }
        public Matrix Transform { get; set; }

        public Sphere()
        {
            Id = new Guid();
            Origin = new ChevalPoint(0,0,0);
            Size = 1.0;
            Transform = Helper.Transform.IdentityMatrix;
        }


    }
}
