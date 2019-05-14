using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_CS
{
    class Program
    {
        static void Main()
        {
            Trace.WriteLine("\n");

            Chromosome ch1 = new Chromosome(2.0M, 2.4M);
            ch1.PrintChromosome();

            Trace.WriteLine("\n");
        }
    }
}
