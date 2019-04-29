using System;
using System.Collections.Generic;
using Cheval.DataStructure;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Models
{
    public struct BoundingBox
    {
        public static BoundingBox Infinity =>
            new BoundingBox(
                Point(
                    float.NegativeInfinity,
                    float.NegativeInfinity,
                    float.NegativeInfinity),
                Point(
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity));

        public static BoundingBox Empty =>
            new BoundingBox(
                Point(
                    float.PositiveInfinity,
                    float.PositiveInfinity,
                    float.PositiveInfinity),
                Point(
                    float.NegativeInfinity,
                    float.NegativeInfinity,
                    float.NegativeInfinity));


        public readonly ChevalTuple Min;
        public readonly ChevalTuple Max;

        public BoundingBox(ChevalTuple min, ChevalTuple max)
        {
            Min = min;
            Max = max;
        }

        public static BoundingBox operator +(BoundingBox box, ChevalTuple p) =>
            Add(box, p);

        public static BoundingBox operator +(BoundingBox a, BoundingBox b) =>
            Add(a, b);

        public static BoundingBox operator *(BoundingBox b, Matrix m) =>
            Multiply(b, m);

        public BoundingBox Add(ChevalTuple point) =>
            Add(this, point);

        public BoundingBox Add(BoundingBox b) =>
            Add(this, b);

        public static BoundingBox Add(BoundingBox box, ChevalTuple point)
        {
            var min = Point(
                MathF.Min(box.Min.X, point.X),
                MathF.Min(box.Min.Y, point.Y),
                MathF.Min(box.Min.Z, point.Z));

            var max = Point(
                MathF.Max(box.Max.X, point.X),
                MathF.Max(box.Max.Y, point.Y),
                MathF.Max(box.Max.Z, point.Z));

            return new BoundingBox(min, max);
        }

        public static BoundingBox Add(BoundingBox a, BoundingBox b)
        {
            a += b.Min;
            a += b.Max;
            return a;
        }

        public static BoundingBox Multiply(BoundingBox b, Matrix m)
        {
            var p1 = b.Min;
            var p2 = Point(b.Min.X, b.Min.Y, b.Max.Z);
            var p3 = Point(b.Min.X, b.Max.Y, b.Min.Z);
            var p4 = Point(b.Min.X, b.Max.Y, b.Max.Z);
            var p5 = Point(b.Max.X, b.Min.Y, b.Min.Z);
            var p6 = Point(b.Max.X, b.Min.Y, b.Max.Z);
            var p7 = Point(b.Max.X, b.Max.Y, b.Min.Z);
            var p8 = b.Max;

            var box2 = Empty;

            foreach (var p in new[] { p1, p2, p3, p4, p5, p6, p7, p8 })
            {
                box2 += (m * p);
            }

            return box2;
        }

        public IEnumerable<ChevalTuple> Corners()
        {
            return new List<ChevalTuple>
            {
                Min,
                Point(Min.X, Min.Y, Max.Z),
                Point(Min.X, Max.Y, Min.Z),
                Point(Min.X, Max.Y, Max.Z),
                Point(Max.X, Min.Y, Min.Z),
                Point(Max.X, Min.Y, Max.Z),
                Point(Max.X, Max.Y, Min.Z),
                Max
            };
        }

        public bool Contains(ChevalTuple p) =>
            Min.X <= p.X && p.X <= Max.X &&
            Min.Y <= p.Y && p.Y <= Max.Y &&
            Min.Z <= p.Z && p.Z <= Max.Z;

        public bool Contains(BoundingBox b) =>
            Contains(b.Min) &&
            Contains(b.Max);

        private static void CheckAxis(float origin, float direction, float tmin, float tmax, out float min, out float max)
        {
            var tminNum = (tmin - origin);
            var tmaxNum = (tmax - origin);

            if (MathF.Abs(direction) >= Cheval.Epsilon)
            {
                min = tminNum / direction;
                max = tmaxNum / direction;
            }
            else
            {
                min = tminNum * float.PositiveInfinity;
                max = tmaxNum * float.PositiveInfinity;
            }

            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
        }

        public bool Intersect(Ray ray)
        {
            CheckAxis(ray.Origin.X, ray.Direction.X, Min.X, Max.X, out var xtmin, out var xtmax);
            CheckAxis(ray.Origin.Y, ray.Direction.Y, Min.Y, Max.Y, out var ytmin, out var ytmax);
            CheckAxis(ray.Origin.Z, ray.Direction.Z, Min.Z, Max.Z, out var ztmin, out var ztmax);

            var tmin = MathF.Max(xtmin, MathF.Max(ytmin, ztmin));
            var tmax = MathF.Min(xtmax, MathF.Min(ytmax, ztmax));

            if (tmin > tmax)
            {
                return false;
            }

            return true;
        }

        public void Split(out BoundingBox left, out BoundingBox right)
        {
            var dx = Max.X - Min.X;
            var dy = Max.Y - Min.Y;
            var dz = Max.Z - Min.Z;

            var greatest = MathF.Max(dx, MathF.Max(dy, dz));

            var x0 = Min.X;
            var y0 = Min.Y;
            var z0 = Min.Z;

            var x1 = Max.X;
            var y1 = Max.Y;
            var z1 = Max.Z;

            if (MathF.Abs(greatest - dx) < Cheval.Epsilon)
            {
                x1 = x0 + dx / 2;
                x0 = x1;
            }
            else if (MathF.Abs(greatest - dy) < Cheval.Epsilon)
            {
                y1 = y0 + dy / 2;
                y0 = y1;
            }
            else
            {
                z1 = z0 + dz / 2;
                z0 = z1;
            }

            var midMin = Point(x0, y0, z0);
            var midMax = Point(x1, y1, z1);

            left = new BoundingBox(Min, midMax);
            right = new BoundingBox(midMin, Max);
        }
    }
}

