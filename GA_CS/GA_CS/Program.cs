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

        public static Decimal Beale (Decimal x, Decimal y)
        {
            return (1.5M - x + x * y) * (1.5M - x + x * y) + (2.25M - x + x * y * y) * (2.25M - x + x * y * y) + (2.625M - x + x * y * y * y) * (2.625M - x + x * y * y * y);
        }

        public static Decimal Ackley (Decimal x, Decimal y)
        {
            return -20 * Math.Exp(-0.2 * Math.Sqrt(0.5 * (x * x + y * y))) -
                Math.Exp(0.5 * (Math.Cos(2 * Math.PI * x) + Math.Cos(2 * Math.PI * y))) + Math.E + 20;
        }

        static void Main()
        {
            Trace.WriteLine("\n");

            Chromosome ch1 = new Chromosome(2.0M, 2.4M);
            ch1.PrintChromosome();
            
            f f1 = new f(Beale);
            GeneticAlgorithm ga = new GeneticAlgorithm(10, 0.5M, 0.4M, 0.2M, 50, f1, -5M, 5M);
            Trace.Write("Beale: ");
            Trace.Write(ga.fitnessFunction(3M, 0.5M).ToString());
            Trace.Write("\n");
            Trace.WriteLine(ga.ToString());
            Trace.WriteLine("\n");
        }
    }
}
