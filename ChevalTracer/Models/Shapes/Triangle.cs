using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models.Shapes
{
    public class Triangle : Shape
    {
        public ChevalTuple Point1 { get; set; }
        public ChevalTuple Point2 { get; set; }
        public ChevalTuple Point3 { get; set; }
        public ChevalTuple Edge1 { get; } 
        public ChevalTuple Edge2 { get; }
        public ChevalTuple Normal { get; } 

        public Triangle(ChevalTuple point1, ChevalTuple point2, ChevalTuple point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Edge1 = Point2 - Point1;
            Edge2 = Point3 - Point1;
            Normal = Normalize(Cross(Edge2, Edge1));
        }

        protected override List<Intersection> LocalIntersect(Ray localRay)
        {
            var result = new List<Intersection>();

            var dirCrossE2 = Cross(localRay.Direction, Edge2);
            var det = Dot(Edge1, dirCrossE2);
            if (MathF.Abs(det) < Cheval.Epsilon)
            {
                return result;
            }

            var f = 1.0f / det;
            var p1ToOrigin = localRay.Origin - Point1; // Can precompute
            var u = f * Dot(p1ToOrigin, dirCrossE2);

            if (u < 0 || u > 1)
            {
                return result;
            }

            var originCrossE1 = Cross(p1ToOrigin, Edge1); //Can Precompute
            var v = f * Dot(localRay.Direction, originCrossE1);

            if (v < 0 || (u + v) > 1)//
            {
                return result;
            }

            var t = f * Dot(Edge2, originCrossE1); //  Dot(Edge2, originCrossE1) can be precomputed

            result.Add(new Intersection(t, this));

            return result;
        }

        protected override ChevalTuple LocalNormalAt(ChevalTuple localPoint)
        {
            return Normal;
        }

        public override BoundingBox Bounds()
        {
            var box = BoundingBox.Empty;
            box += Point1;
            box += Point2;
            box += Point3;
            return box;
        }
    }
}
