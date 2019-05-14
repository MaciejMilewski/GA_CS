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
        public Decimal Beale (Decimal x, Decimal y)
        {
            return (1.5M - x + x * y) * (1.5M - x + x * y) + (2.25M - x + x * y * y) * (2.25M - x + x * y * y) + (2.625M - x + x * y * y * y) * (2.625M - x + x * y * y * y);
        }
        static void Main()
        {
            Trace.WriteLine("\n");

            Chromosome ch1 = new Chromosome(2.0M, 2.4M);
            ch1.PrintChromosome();

            //Func<Decimal, Decimal> purposeFunction = new Func<Decimal, Decimal>(Beale);
            //GeneticAlgorithm ga = new GeneticAlgorithm(10, 0.5M, 0.4M, 0.2M, 50, Beale, -5, 5);

            Trace.WriteLine("\n");
        }
    }
}
