using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_CS
{
    public class Program
    {
        public static double[] lowerBound = { -5, -5 };
        public static double[] upperBound = { +5, +5 };
        public static double bestFitness = 0.0;
        public static double[] bestGenes = { 0.0, 0.0 };

        public static void PrintTime(DateTime t1, DateTime t2)
        {
            string time = "Time = ";
            TimeSpan timeSpan = t2 - t1;

            time += timeSpan.Minutes.ToString("D2") + ":";
            time += timeSpan.Seconds.ToString("D2") + ".";
            time += timeSpan.Milliseconds.ToString("D3");
            time += " (min:sec.ms) " + "\r\n";

            Trace.WriteLine(time);
        }
        
        public static void Main()
        {
            Trace.WriteLine("\n");
            DateTime timeStart = DateTime.Now;

            f f1 = new f(OptimizationFunctions.Ackley);
            GeneticAlgorithm ga = new GeneticAlgorithm(6500, 2, 0.05, 0.04, 450, f1,FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            ga.GenerateInitialGenes();
            ga.Initialize();

            DateTime timeEnd = DateTime.Now;
            PrintTime(timeStart, timeEnd);
            Trace.Write("Best Fitness: " + ga.BestFitness.ToString() + "\r\n");
            Trace.Write("Best X: " + ga.BestGene[0] + "\r\n");
            Trace.Write("Best Y: " + ga.BestGene[1] + "\r\n");
            Trace.WriteLine("\n");
        }
    }
}
