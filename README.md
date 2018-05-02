# Neural-Network
A quick neural network in C# written for my experiments... Inspired from the JavaScript [Toy-Neural-Network](https://github.com/CodingTrain/Toy-Neural-Network-JS) by [@shiffman](https://github.com/shiffman).

### Usage

```c#
// three-layer neural network...
NeuralNetwork nn = new NeuralNetwork(2, 4, 1);
```

```
nn.Train(double[] x_arr, double[] y_arr)
nn.Predict(double[] arr)
nn.Copy()
nn.Mutate(Func<double, double> f)
nn.Serialize()
NeuralNetwork.Deserialize(Stream s)
```