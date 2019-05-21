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

            /*Chromosome ch1 = new Chromosome(2.0, 2.4);
            Chromosome ch2 = new Chromosome(1.0, 77.08);
            Chromosome ch3 = new Chromosome(3.33, -0.3);
            ch1.PrintChromosome();
            ch2.PrintChromosome();
            ch3.PrintChromosome();*/

            f f1 = new f(OptimizationFunctions.Ackley);
            GeneticAlgorithm ga = new GeneticAlgorithm(6500, 2, 0.15, 0.14, 450, f1, lowerBound, upperBound);
            ga.GenerateInitialGenes();
            ga.Initialize();

            /*Trace.Write("Beale: ");
            Trace.Write(ga.FitnessFunction(3, 0.5).ToString());
            Trace.Write("\n");
            Trace.WriteLine(ga.ToString());
            //ga.Population[0] = ch1;
            //ga.Population[1] = ch2;
            //ga.Population[2] = ch3;
            ga.GenerateInitialGenes();
            ga.PrintPopulation();*/

            // End
            DateTime timeEnd = DateTime.Now;
            PrintTime(timeStart, timeEnd);
            Trace.Write("Best Fitness: " + ga.BestFitness.ToString() + "\r\n");
            Trace.Write("Best X: " + ga.BestGene[0] + "\r\n");
            Trace.Write("Best Y: " + ga.BestGene[1] + "\r\n");
            Trace.WriteLine("\n");
        }
    }
}
