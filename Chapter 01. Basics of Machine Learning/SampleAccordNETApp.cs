using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord.Controls;
using Accord.Statistics;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;

namespace Chapter01_2
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Run();
        }
        public static void Run()
        {
            double[][] inputs =
            {
                new double[] { 0, 0 },
                new double[] { 0.25, 0.25 }, 
                new double[] { 0.5, 0.5 }, 
                new double[] { 1, 1 },
            };

            int[] outputs =
            { 
                0,
                0,
                1,
                1,
            };

            // Train a Logistic Regression model
            var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
            {
                MaxIterations = 100
            };
            var logit = learner.Learn(inputs, outputs);

            // Predict output
            bool[] predictions = logit.Decide(inputs);

            // Plot the results
            Console.WriteLine("Expected Results", inputs, outputs);
            Console.WriteLine("Actual Logistic Regression Output", inputs, predictions.ToZeroOne());

            Console.ReadKey();
        }
    }
}
