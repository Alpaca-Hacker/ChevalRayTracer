using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static Cheval.DataStructure.ChevalTuple;

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
        public Shape Parent { get; set; }
        public BoundingBox BoundingBox { get; }
        protected Shape()
        {
            Transform = Helper.Transform.IdentityMatrix;
            Id = Guid.NewGuid();
            BoundingBox = Bounds();
        }

        protected abstract List<Intersection> LocalIntersect(Ray localRay);
        protected abstract ChevalTuple LocalNormalAt(ChevalTuple localPoint);

        public ChevalTuple NormalAt(ChevalTuple point)
        {
            var localPoint = WorldToObject(point);
            var localNormal = LocalNormalAt(localPoint);

            var worldNormal = NormalToWorld(localNormal);

            return worldNormal;
        }

        public List<Intersection> Intersect(Ray ray)
        {
            var localRay = ray.Transform(_inverseTransform);
            return LocalIntersect(localRay);
        }

        public ChevalTuple WorldToObject(ChevalTuple point)
        {
            if (Parent != null)
            {
                point = Parent.WorldToObject(point);
            }

            return _inverseTransform * point;
        }

        public ChevalTuple NormalToWorld(ChevalTuple normal)
        {
            normal = Matrix.Transpose(_inverseTransform) * normal;
            normal.W = 0;
            normal = Normalize(normal);
            if (Parent != null)
            {
                normal = Parent.NormalToWorld(normal);
            }

            return normal;
        }

        public abstract BoundingBox Bounds();

        public virtual BoundingBox ParentSpaceBounds() => Bounds() * Transform;
    }

}
