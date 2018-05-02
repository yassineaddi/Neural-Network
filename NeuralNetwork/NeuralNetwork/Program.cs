using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(2, 4, 1);
            double[,] TrainingsetX = new double[,] { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 } };
            double[,] TrainingsetY = new double[,] { { 0 }, { 1 }, { 0 }, { 1 } };

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < TrainingsetX.GetLength(0); j++)
                {
                    double[] x = new double[TrainingsetX.GetLength(1)];
                    for (int k = 0; k < x.Length; k++)
                    {
                        x[k] = TrainingsetX[j, k];
                    }

                    double[] y = new double[TrainingsetY.GetLength(1)];
                    for (int k = 0; k < y.Length; k++)
                    {
                        y[k] = TrainingsetY[j, k];
                    }

                    nn.Train(x, y);
                }
            }

            StringBuilder s = new StringBuilder();
            s.Append("{ 0, 0 } ----> ");
            s.Append(nn.Predict(new double[] { 0, 0 })[0]);
            s.Append("\n");
            s.Append("{ 1, 1 } ----> ");
            s.Append(nn.Predict(new double[] { 1, 1 })[0]);
            s.Append("\n");
            s.Append("{ 0, 1 } ----> ");
            s.Append(nn.Predict(new double[] { 0, 1 })[0]);
            s.Append("\n");
            s.Append("{ 1, 0 } ----> ");
            s.Append(nn.Predict(new double[] { 1, 0 })[0]);

            Console.Write(s.ToString());

            Console.ReadKey();
        }
    }
}
