using System;
using System.Collections.Generic;
using Cheval.DataStructure;

namespace Cheval.Models.Shapes
{
    public abstract class Shape
    {
        public Guid Id { get; }
        private Matrix _transform;
        private Matrix _inverseTransform;
        public Matrix Transform
        {
            get => _transform;
            set
            {
                _transform = value;
                _inverseTransform = Matrix.Inverse(value);
            }
        }

        public Matrix InverseTransform => _inverseTransform;

        public Material Material { get; set; } = new Material();
        public bool NoShadow { get; set; }

        protected Shape()
        {
            Transform = Helper.Transform.IdentityMatrix;
            Id = Guid.NewGuid();
        }

        protected abstract List<Intersection> LocalIntersect(Ray localRay);
        protected abstract ChevalTuple LocalNormalAt(ChevalTuple localPoint);

        public ChevalTuple NormalAt(ChevalTuple point)
        {
            var localPoint = _inverseTransform * point;

            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = Matrix.Transpose(_inverseTransform) * localNormal;

            return ChevalTuple.Normalize(worldNormal);
        }

        public List<Intersection> Intersect(Ray ray)
        {
            var localRay = ray.Transform(_inverseTransform);
            return LocalIntersect(localRay);
        }
    }

}
