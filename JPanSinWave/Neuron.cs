using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPanBackprop
{
    public class Neuron
    {
        public double Bias;
        public double[] Weights;
        public double Output;
        public double Input;
        public Func<double, double> Activation;
        public Func<double, double> ActDerivative;
        public double PartialDerivative;
        public double BiasUpdate;
        public double PrevBiasUpdate;
        public double[] WeightUpdates;
        public double[] PrevWeightUpdates;

        public Neuron(Func<double, double> activation, Func<double, double> actDerivative, int inputCount)
        {
            Activation = activation;
            ActDerivative = actDerivative;
            Weights = new double[inputCount];
            WeightUpdates = new double[inputCount];
            PrevWeightUpdates = new double[inputCount];
        }

        public void Randomize(Random rand)
        {
            Bias = rand.NextDouble(-0.5, 0.5);
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = rand.NextDouble(-0.5, 0.5);
            }
        }

        public double Compute(double[] input)
        {
            Output = 0;
            for (int i = 0; i < input.Length; i++)
            {
                Output += input[i] * Weights[i];
            }
            Input = Output + Bias;
            Output = Activation(Input);

            return Output;
        }

        /*public double Derivative(double x)
        {
            return 1 - Math.Pow(Function(x), 2);
        }*/

        /*public double Function(double x)
        {
            return (Math.Pow(Math.E, x) - Math.Pow(Math.E, -x)) / (Math.Pow(Math.E, x) + Math.Pow(Math.E, -x));
        }*/
    }
}
