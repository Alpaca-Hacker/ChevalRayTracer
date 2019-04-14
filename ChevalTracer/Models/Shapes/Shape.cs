using System;
using System.Collections.Generic;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public abstract class Shape
    {
        public Guid Id { get; }
        public Matrix Transform { get; set; }
        public Material Material { get; set; } = new Material();

        protected Shape()
        {
            Transform = Helper.Transform.IdentityMatrix;
            Id = Guid.NewGuid();
        }

        protected abstract List<Intersection> LocalIntersect(Ray localRay);
        protected abstract ChevalTuple LocalNormalAt(ChevalTuple localPoint);

        public ChevalTuple NormalAt(ChevalTuple point)
        {
            var localPoint = Matrix.Inverse(Transform) * point;

            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = Matrix.Transpose(Matrix.Inverse(Transform)) * localNormal;

            return ChevalTuple.Normalize(worldNormal);
        }

        public List<Intersection> Intersect(Ray ray)
        {
            var localRay = ray.Transform(Matrix.Inverse(Transform));
            return LocalIntersect(localRay);
        }
    }

}
