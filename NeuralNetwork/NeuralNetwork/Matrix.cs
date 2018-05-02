using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    [Serializable]
    class Matrix
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public double[,] Data { get; set; }

        public Matrix(int Rows, int Cols)
        {
            this.Rows = Rows;
            this.Cols = Cols;
            this.Data = new double[Rows, Cols];
            this.Data.Initialize();
        }

        public Matrix Copy()
        {
            Matrix m = new Matrix(Rows, Cols);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    m.Data[i, j] = Data[i, j];
                }
            }
            return m;
        }

        public static Matrix FromArray(double[] arr)
        {
            Matrix m = new Matrix(arr.Length, 1);
            for (int i = 0; i < arr.Length; i++)
            {
                m.Data[i, 0] = arr[i];
            }
            return m;
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
            {
                throw new Exception();
            }

            Matrix Result = new Matrix(a.Rows, a.Cols);
            for (int i = 0; i < Result.Rows; i++)
            {
                for (int j = 0; j < Result.Cols; j++)
                {
                    Result.Data[i, j] = a.Data[i, j] - b.Data[i, j];
                }
            }
            return Result;
        }

        public double[] ToArray()
        {
            double[] arr = new double[Rows * Cols];
            int Index = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    arr[Index++] = Data[i, j];
                }
            }
            return arr;
        }

        public void Randomize()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] = new CryptoRandom().NextDouble() * 2 - 1;
                }
            }
        }

        public void Add(object x)
        {
            if (x is Matrix)
            {
                Matrix m = x as Matrix;

                if (Rows != m.Rows || Cols != m.Cols)
                {
                    throw new Exception();
                }

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Data[i, j] += m.Data[i, j];
                    }
                }
            }
            else
            {
                double n = Convert.ToDouble(x);
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Data[i, j] += n;
                    }
                }
            }
        }

        public static Matrix Transpose(Matrix m)
        {
            Matrix Result = new Matrix(m.Cols, m.Rows);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Cols; j++)
                {
                    Result.Data[j, i] = m.Data[i, j];
                }
            }
            return Result;
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
            {
                throw new Exception();
            }

            Matrix Result = new Matrix(a.Rows, b.Cols);
            for (int i = 0; i < Result.Rows; i++)
            {
                for (int j = 0; j < Result.Cols; j++)
                {
                    double Sum = 0;
                    for (int k = 0; k < a.Cols; k++)
                    {
                        Sum += a.Data[i, k] * b.Data[k, j];
                    }
                    Result.Data[i, j] = Sum;
                }
            }

            return Result;
        }

        public void Multiply(object x)
        {
            if (x is Matrix)
            {
                Matrix m = x as Matrix;

                if (Rows != m.Rows || Cols != m.Cols)
                {
                    throw new Exception();
                }

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Data[i, j] *= m.Data[i, j];
                    }
                }
            }
            else
            {
                double n = Convert.ToDouble(x);
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Data[i, j] *= n;
                    }
                }
            }
        }

        public void Map(Func<double, double> f)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] = f(Data[i, j]);
                }
            }
        }

        public static Matrix Map(Matrix m, Func<double, double> f)
        {
            Matrix Result = new Matrix(m.Rows, m.Cols);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Cols; j++)
                {
                    Result.Data[i, j] = f(m.Data[i, j]);
                }
            }
            return Result;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    s.Append(Data[i, j]);
                    s.Append("\t");
                }
                s.Append("\n");
            }

            return s.ToString();

            //return base.ToString();
        }
    }
}
