using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public class ParticleSwarm
    {
        public int Size { get; set; }
        public int Particles { get; set; }
        public int Iterations { get; set; }
        private int It { get; set; }
        public double[] LowerLimit { get; set; }
        public double[] UpperLimit { get; set; }
        public f Function { get; set; }
        public double BestResult { get; set; }
        public double[] BestParameters { get; set; }

        public ParticleSwarm(f function, int size, int particles, int iterations, double [] lowerlimit, double[] upperlimit)
        {
            this.Function = function;
            this.Size = size;
            this.Particles = particles;
            this.Iterations = iterations;
            this.LowerLimit = lowerlimit;
            this.UpperLimit = upperlimit;
            this.BestParameters = new double[Size];
        }

    }
}
