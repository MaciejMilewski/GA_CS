using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_CS
{
    

    public class Chromosome
    {
        public double[] Genes = new double[2];
        public double Fitness { get; set; }

        public Chromosome(double gene_1, double gene_2)
        {
            this.Genes[0] = gene_1;
            this.Genes[1] = gene_2;
        }

        public void PrintChromosome()
        {
            Trace.Write("Genes: [");
            Trace.Write(Genes[0].ToString());
            Trace.Write(" ");
            Trace.Write(Genes[1].ToString());
            Trace.Write("]");
            Trace.Write("\n");
        }
    }
}
