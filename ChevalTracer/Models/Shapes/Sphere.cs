using System;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public class Sphere : Shape
    {
        public override Guid Id { get; }
        public override ChevalTuple Origin { get; set; }
        public override double Size { get; set; }
        public override Matrix Transform { get; set; }
        public override Material Material { get; set; }

        public Sphere()
        {
            Id = Guid.NewGuid();
            Origin = ChevalTuple.Point(0,0,0);
            Size = 1.0;
            Transform = Helper.Transform.IdentityMatrix;
            Material = new Material();
        }
        public override ChevalTuple NormalAt(ChevalTuple point)
        {
            var objectPoint = (Matrix.Inverse(Transform) * point);
            var normal = ChevalTuple.Normalize((objectPoint - Origin));
            var objectNormal = (Matrix.Transpose(Matrix.Inverse(Transform)) * normal);
            normal = ChevalTuple.Vector(objectNormal.X,objectNormal.Y,objectNormal.Z);

            return ChevalTuple.Normalize(normal);
        }
    }
}
