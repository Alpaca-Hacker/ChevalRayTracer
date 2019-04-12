using System;
using static Cheval.Models.ChevalTuple;
using static Cheval.Models.Matrix;

namespace Cheval.Models.Primitives
{
    public class Sphere
    {
        public Guid Id { get; }
        public ChevalTuple Origin { get; set; }
        public double Size { get; set; }
        public Matrix Transform { get; set; }
        public Material Material { get; set; }

        public Sphere()
        {
            Id = Guid.NewGuid();
            Origin = Point(0,0,0);
            Size = 1.0;
            Transform = Helper.Transform.IdentityMatrix;
            Material = new Material();
        }
        public ChevalTuple NormalAt(ChevalTuple point)
        {
            var objectPoint = (Inverse(Transform) * point);
            var normal = Normalize((objectPoint - Origin));
            var objectNormal = (Transpose(Inverse(Transform)) * normal);
            normal = Vector(objectNormal.X,objectNormal.Y,objectNormal.Z);

            return Normalize(normal);
        }
    }
}
