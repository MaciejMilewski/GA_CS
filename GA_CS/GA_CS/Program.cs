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
            // Start

            Chromosome ch1 = new Chromosome(2.0, 2.4);
            ch1.PrintChromosome();

            f f1 = new f(OptimizationFunctions.Beale);
            GeneticAlgorithm ga = new GeneticAlgorithm(10, 0.5, 0.4, 50, f1, lowerBound, upperBound);
            Trace.Write("Beale: ");
            Trace.Write(ga.FitnessFunction(3, 0.5).ToString());
            Trace.Write("\n");
            Trace.WriteLine(ga.ToString());
            
            // End
            DateTime timeEnd = DateTime.Now;
            PrintTime(timeStart, timeEnd);
            Trace.WriteLine("\n");
        }
    }
}
