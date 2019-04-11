﻿using System;
using static Cheval.Models.ChevalVector;
using static Cheval.Models.Matrix;

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
            Id = Guid.NewGuid();
            Origin = new ChevalPoint(0,0,0);
            Size = 1.0;
            Transform = Helper.Transform.IdentityMatrix;
        }
        public ChevalVector NormalAt(ChevalPoint point)
        {
            var objectPoint = (ChevalPoint)(Inverse(Transform) * point);
            var normal = Normalize((ChevalVector)(objectPoint - Origin));
            var objectNormal = (Transpose(Inverse(Transform)) * normal);
            normal = new ChevalVector(objectNormal.X,objectNormal.Y,objectNormal.Z);

            return Normalize(normal);
        }
    }
}
