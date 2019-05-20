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
            Chromosome ch2 = new Chromosome(1.0, 77.08);
            Chromosome ch3 = new Chromosome(3.33, -0.3);
            /*ch1.PrintChromosome();
            ch2.PrintChromosome();
            ch3.PrintChromosome();*/

            f f1 = new f(OptimizationFunctions.Beale);
            GeneticAlgorithm ga = new GeneticAlgorithm(3, 2, 0.5, 0.4, 50, f1, lowerBound, upperBound);
            /*Trace.Write("Beale: ");
            Trace.Write(ga.FitnessFunction(3, 0.5).ToString());
            Trace.Write("\n");
            Trace.WriteLine(ga.ToString());*/
            //ga.Population[0] = ch1;
            //ga.Population[1] = ch2;
            //ga.Population[2] = ch3;
            ga.GenerateInitialGenes();
            ga.PrintPopulation();
            
            // End
            DateTime timeEnd = DateTime.Now;
            PrintTime(timeStart, timeEnd);
            Trace.WriteLine("\n");
        }
    }
}
