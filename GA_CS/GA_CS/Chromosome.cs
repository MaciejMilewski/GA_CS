using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_CS
{
    public delegate Decimal f(Decimal x, Decimal y);

    public class Chromosome
    {
        public Decimal[] Genes = new Decimal[2];
        //public Decimal Fitness { get; set; }
        public f fitness;

        public Chromosome(Decimal gene_1, Decimal gene_2)
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
