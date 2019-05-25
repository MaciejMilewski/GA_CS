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
            f ackley = new f(OptimizationFunctions.Ackley);

            GeneticAlgorithm ga = new GeneticAlgorithm(65000, 2, 0.05, 0.04, 450, ackley, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            ga.GeneticAlgorithmOptimization();
            ga.PrintResult();

            //ParticleSwarm ps = new ParticleSwarm(ackley, 2, 1000, 8000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            //ps.ParticleSwarmOptimization();
            //ps.PrintResult();

            DateTime timeEnd = DateTime.Now;
            PrintTime(timeStart, timeEnd);
            Trace.WriteLine("\n");
        }
    }
}
