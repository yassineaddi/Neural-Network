using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeuralNetwork
{
    [Serializable]
    class NeuralNetwork
    {
        public int X { get; set; }
        public int H { get; set; }
        public int Y { get; set; }
        public Matrix WeightsXH { get; set; }
        public Matrix WeightsHY { get; set; }
        public Matrix BiasH { get; set; }
        public Matrix BiasY { get; set; }
        public double LearningRate { get; set; }

        public NeuralNetwork(int X, int H, int Y)
        {
            this.X = X;
            this.H = H;
            this.Y = Y;

            WeightsXH = new Matrix(H, X);
            WeightsHY = new Matrix(Y, H);
            WeightsXH.Randomize();
            WeightsHY.Randomize();

            BiasH = new Matrix(H, 1);
            BiasY = new Matrix(Y, 1);
            BiasH.Randomize();
            BiasY.Randomize();

            LearningRate = 0.1;
        }

        public double[] Predict(double[] arr)
        {
            Matrix x = Matrix.FromArray(arr);
            Matrix h = Matrix.Multiply(WeightsXH, x);
            h.Add(this.BiasH);

            h.Map(NeuralNetwork.Sigmoid);

            Matrix y = Matrix.Multiply(WeightsHY, h);
            y.Add(BiasY);
            y.Map(NeuralNetwork.Sigmoid);

            return y.ToArray();
        }

        public void Train(double[] x_arr, double[] y_arr)
        {
            Matrix x = Matrix.FromArray(x_arr);
            Matrix h = Matrix.Multiply(WeightsXH, x);
            h.Add(this.BiasH);

            h.Map(NeuralNetwork.Sigmoid);

            Matrix y = Matrix.Multiply(WeightsHY, h);
            y.Add(BiasY);
            y.Map(NeuralNetwork.Sigmoid);

            Matrix Targets = Matrix.FromArray(y_arr);

            Matrix y_err = Matrix.Subtract(Targets, y);

            Matrix Gradients = Matrix.Map(y, NeuralNetwork.DerivSigmoid);
            Gradients.Multiply(y_err);
            Gradients.Multiply(LearningRate);

            Matrix ht = Matrix.Transpose(h);
            Matrix WeightHYDeltas = Matrix.Multiply(Gradients, ht);

            WeightsHY.Add(WeightHYDeltas);
            BiasY.Add(Gradients);

            Matrix whyt = Matrix.Transpose(WeightsHY);
            Matrix h_err = Matrix.Multiply(whyt, y_err);

            Matrix HiddenGradient = Matrix.Map(h, NeuralNetwork.DerivSigmoid);
            HiddenGradient.Multiply(h_err);
            HiddenGradient.Multiply(LearningRate);

            Matrix xt = Matrix.Transpose(x);
            Matrix WeightXHDeltas = Matrix.Multiply(HiddenGradient, xt);

            WeightsXH.Add(WeightXHDeltas);
            BiasH.Add(HiddenGradient);
        }
        
        public NeuralNetwork Copy()
        {
            return this.MemberwiseClone() as NeuralNetwork;
        }

        public void Mutate(Func<double, double> f)
        {
            WeightsXH.Map(f);
            WeightsHY.Map(f);
            BiasH.Map(f);
            BiasY.Map(f);
        }

        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public static double DerivSigmoid(double y)
        {
            return y * (1 - y);
        }
        
        public Stream Serialize()
        {
            IFormatter Formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            Formatter.Serialize(ms, this);
            return ms;
        }

        public static NeuralNetwork Deserialize(Stream s)
        {
            IFormatter Formatter = new BinaryFormatter();
            NeuralNetwork nn = (NeuralNetwork)Formatter.Deserialize(s);

            return nn;
        }
    }
}
