using System;
using System.Collections.Generic;

namespace Cheval.Models
{
    public class Matrix : IEquatable<Matrix>
    {
        private readonly double[,] _data;
        public int Size => _data.GetUpperBound(0) + 1;

        public static Matrix Identity = new Matrix(new double[,]
            {{1, 0, 0, 0}, {0, 1, 0, 0}, {0, 0, 1, 0}, {0, 0, 0, 1}});

        public Matrix(double[,] data)
        {
            _data = data;
        }

        public Matrix(int size)
        {
            _data = new double[size, size];
        }

        public ref double this[int row, int column] => ref _data[row, column];


        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Size != b.Size)
            {
                throw new ArgumentException("Matrices must be of the same size");
            }

            var c = new Matrix(a.Size);
            for (var i = 0; i < c.Size; i++)
            {
                for (var j = 0; j < c.Size; j++)
                {
                    double s = 0.0;
                    for (var k = 0; k < c.Size; k++)
                    {
                        s += a[i, k] * b[k, j];
                    }

                    c[i, j] = s;
                }
            }

            return c;
        }

        public static ChevalTuple operator *(Matrix matrix, ChevalTuple tuple)
        {
            if (matrix.Size != 4)
            {
                throw new ArgumentException("Matrix must be of size 4");
            }

            var newX = (matrix[0, 0] * tuple.X)
                       + (matrix[0, 1] * tuple.Y)
                       + (matrix[0, 2] * tuple.Z)
                       + (matrix[0, 3] * tuple.W);

            var newY = (matrix[1, 0] * tuple.X)
                       + (matrix[1, 1] * tuple.Y)
                       + (matrix[1, 2] * tuple.Z)
                       + (matrix[1, 3] * tuple.W);

            var newZ = (matrix[2, 0] * tuple.X)
                       + (matrix[2, 1] * tuple.Y)
                       + (matrix[2, 2] * tuple.Z)
                       + (matrix[2, 3] * tuple.W);

            var newW = (matrix[3, 0] * tuple.X)
                       + (matrix[3, 1] * tuple.Y)
                       + (matrix[3, 2] * tuple.Z)
                       + (matrix[3, 3] * tuple.W);

            var result = new ChevalTuple(newX, newY, newZ, newW);

            return result;
        }

        public static ChevalTuple operator *(ChevalTuple tuple, Matrix matrix)
        {
            return matrix * tuple;
        }


        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Size != b.Size)
            {
                throw new ArgumentException("Matrices must be of the same size");
            }

            var c = new Matrix(a.Size);
            for (var i = 0; i < c.Size; i++)
            {
                for (var j = 0; j < c.Size; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }

            return c;
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            if (right is null || left is null || left.Size != right.Size)
            {
                return false;
            }

            for (var i = 0; i < left.Size; i++)
            {
                for (var j = 0; j < left.Size; j++)
                {
                    if (Math.Abs(left[i, j] - right[i, j]) > Cheval.Epsilon)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix);
        }

        public bool Equals(Matrix other)
        {
            return other != null &&
                   this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_data);
        }

        public static Matrix Submatrix(Matrix matrix, int row, int column)
        {
            var newMat = new Matrix(matrix.Size - 1);

            var newRow = 0;
            var newCol = 0;
            for (var i = 0; i < matrix.Size; i++)
            {
                if (i == column)
                {
                    continue;
                }

                for (var j = 0; j < matrix.Size; j++)
                {
                    if (j == row)
                    {
                        continue;
                    }
                    newMat[newRow, newCol] = matrix[j, i];
                    newRow++;
                }
                newCol++;
                newRow = 0;
            }

            return newMat;
        }
                public static Matrix Transpose(Matrix matrix)
        {
            var newMatrix = new Matrix(matrix.Size);
            for (int column = 0; column < matrix.Size; column++)
            {
                for (var row = 0; row < matrix.Size; row++)
                {
                    newMatrix[row, column] = matrix[column, row];
                }
            }

            return newMatrix;
        }

        public static double Determinant(Matrix matrix)
        {
            if (matrix.Size != 2)
            {
                throw new ArgumentException("Matrix must be of size 2");
            }

            var result = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            return result;
        }

    }
}
