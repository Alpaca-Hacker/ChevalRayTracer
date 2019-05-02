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
        private BoundingBox _boundingBox;
        private bool _hasBoundingBox;

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
        public BoundingBox BoundingBox
        {
            get
            {
                if (_hasBoundingBox)
                {
                    return _boundingBox;
                }

                _boundingBox = Bounds();
                _hasBoundingBox = true;

                return _boundingBox;
            }
        }

        protected Shape()
        {
            Transform = Helper.Transform.IdentityMatrix;
            Id = Guid.NewGuid();
        }

        protected abstract List<Intersection> LocalIntersect(Ray localRay);
        protected abstract ChevalTuple LocalNormalAt(ChevalTuple localPoint, Intersection hit = null);

        public ChevalTuple NormalAt(ChevalTuple point, Intersection intersection = null)
        {
            var localPoint = WorldToObject(point);
            var localNormal = LocalNormalAt(localPoint, intersection);

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

        public virtual BoundingBox Bounds() => BoundingBox.Infinity;
           
        public virtual BoundingBox ParentSpaceBounds() => Bounds() * Transform;
    }

}
