using Deedle;
using MathNet.Numerics.Statistics;
using ScottPlot;
using System;
using System.IO;
using System.Linq;

namespace Chapter04_1
{
    public class Program
    {
        public static void Run()
        {
            Console.SetWindowSize(100, 50);

            // Path to the dataset
            string dataDirPath = @"<path-to-your-data-dir>";
            string ohlcDataPath = Path.Combine(dataDirPath, "eurusd-daily-ohlc.csv");
            Console.WriteLine($"Loading {ohlcDataPath}\n");

            // Load the OHLC data into a DataFrame
            var ohlcDF = Frame.ReadCsv(
                ohlcDataPath,
                hasHeaders: true,
                inferTypes: true
            );

            // Plot close prices as a time-series line chart
            var closePrices = ohlcDF.GetColumn<double>("Close").ValuesAll.ToArray();
            var rowKeys = ohlcDF.RowKeys.Select(x => (double)x).ToArray();
            PlotLineChart("Close Prices", rowKeys, closePrices);

            // Plot daily returns as a time-series line chart
            var dailyReturns = ohlcDF.FillMissing(0.0)["DailyReturn"].ValuesAll.ToArray();
            PlotLineChart("Daily Returns", rowKeys, dailyReturns);

            // Plot daily return histogram
            PlotHistogram("Daily Return Histogram", dailyReturns, 20);

            // Calculate and display daily return statistics
            double returnMean = dailyReturns.Mean();
            double returnStdDev = dailyReturns.StandardDeviation();
            double returnMin = dailyReturns.Min();
            double returnMax = dailyReturns.Max();
            double returnMedian = dailyReturns.Median();

            var quantiles = Statistics.Quantile(dailyReturns, new[] { 0.25, 0.5, 0.75 });

            Console.WriteLine("-- DailyReturn Distribution --");
            Console.WriteLine($"Mean: \t\t\t{returnMean:0.00}");
            Console.WriteLine($"StdDev: \t\t{returnStdDev:0.00}");
            Console.WriteLine($"Min: \t\t\t{returnMin:0.00}");
            Console.WriteLine($"Q1 (25%): \t\t{quantiles[0]:0.00}");
            Console.WriteLine($"Median (Q2): \t\t{quantiles[1]:0.00}");
            Console.WriteLine($"Q3 (75%): \t\t{quantiles[2]:0.00}");
            Console.WriteLine($"Max: \t\t\t{returnMax:0.00}");

            Console.WriteLine("\nDONE!!!");
            Console.ReadKey();
        }

        static void PlotLineChart(string title, double[] x, double[] y)
        {
            var plt = new ScottPlot.Plot(700, 500);
            plt.AddScatter(x, y);
            plt.Title(title);
            plt.XLabel("Time");
            plt.YLabel("Value");
            plt.SaveFig($"{title.Replace(" ", "_")}.png");
            Console.WriteLine($"Saved plot: {title}.png");
        }

        static void PlotHistogram(string title, double[] data, int binCount)
        {
            var plt = new ScottPlot.Plot(700, 500);
            var hist = new ScottPlot.Statistics.Histogram(data, binCount);
            plt.AddBar(hist.Counts, hist.BinEdges.Skip(1).ToArray());
            plt.Title(title);
            plt.XLabel("Bins");
            plt.YLabel("Frequency");
            plt.SaveFig($"{title.Replace(" ", "_")}.png");
            Console.WriteLine($"Saved histogram: {title}.png");
        }
    }
}
