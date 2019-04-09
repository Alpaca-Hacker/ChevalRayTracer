using System;

namespace Cheval.Models
{
    public class Matrix
    {
        private readonly double[,] _data;
        public int Size => _data.GetUpperBound(0) + 1;


        public Matrix(double[,] data)
        {
            _data = data;
        }

        public Matrix(int size)
        {
            _data = new double[size,size];
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
                    for (var m = 0; m < c.Size; m++)
                    {
                        s += a[i, m] * b[m, j];
                    }
                    c[i, j] = s;
                }
            }
            return c;
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
    }
}