using System;
using Cheval.DataStructure;
using Cheval.Models;
using static Cheval.DataStructure.ChevalTuple;

namespace Cheval.Helper
{
    public static class Transform
    {
        public static Matrix IdentityMatrix => new Matrix(new float[,]
            {{1, 0, 0, 0}, 
             {0, 1, 0, 0},
             {0, 0, 1, 0},
             {0, 0, 0, 1}
            });

        public static Matrix Translation(float x, float y, float z)
        {
            var translation = IdentityMatrix;
            translation[0, 3] = x;
            translation[1, 3] = y;
            translation[2, 3] = z;
            return translation;
        }

        public static Matrix Scaling(float x, float y, float z)
        {
            var translation = IdentityMatrix;
            translation[0, 0] = x;
            translation[1, 1] = y;
            translation[2, 2] = z;
            return translation;
        }

        public static Matrix RotationX(float rads)
        {
            var rotation = new Matrix(new[,]
            {
                {1, 0, 0, 0},
                {0, MathF.Cos(rads),-MathF.Sin(rads), 0},
                {0, MathF.Sin(rads), MathF.Cos(rads), 0},
                {0, 0, 0, 1}
            });
            return rotation;
        }
        public static Matrix RotationY(float rads)
        {
            var rotation = new Matrix(new[,]
            {
                {MathF.Cos(rads), 0, MathF.Sin(rads),  0},
                {0, 1,0, 0},
                {-MathF.Sin(rads),0, MathF.Cos(rads), 0},
                {0, 0, 0, 1}
            });
            return rotation;
        }
        public static Matrix RotationZ(float rads)
        {
            var rotation = new Matrix(new[,]
            {
                {MathF.Cos(rads),-MathF.Sin(rads), 0, 0},
                {MathF.Sin(rads), MathF.Cos(rads), 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            });
            return rotation;
        }

        public static Matrix Shearing(float xy, float xz, float yx, float yz, float zx, float zy)
        {
            var shear = new Matrix(new float[,]{
            
               {  1, xy, xz, 0},
               { yx,  1, yz, 0},
               { zx, zy,  1, 0},
               {  0,  0,  0, 1}
            });
            return shear;
        }

        public static Matrix ViewTransform(ChevalTuple from,  ChevalTuple to, ChevalTuple up)
        {
            var forward = Normalize(to - from);
            var upN = Normalize(up);
            var left = Cross(forward, upN);
            var trueUp = Cross(left, forward);
            var orientation = new Matrix(new float[,]
            {
                { left.X, left.Y, left.Z, 0},
                { trueUp.X, trueUp.Y, trueUp.Z, 0},
                {-forward.X, -forward.Y, -forward.Z, 0},
                { 0, 0, 0, 1}
            });
            var result = orientation * Translation(-from.X, -from.Y, -from.Z);
            return result;
        }
    }
}
