using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public class HillClimbingAlgorithm
    {
        public f Function { get; set; }
        public double[] LowerLimit { get; set; }
        public double[] UpperLimit { get; set; }
        public int Iterations { get; set; }

        public double[] Result = new double[2];


        public HillClimbingAlgorithm(f function, int iterations, double[] lowerlimit, double[] upperrlimit)
        {
            this.Function = function;
            this.Iterations = iterations;
            this.LowerLimit = lowerlimit;
            this.UpperLimit = upperrlimit;
        }

        public void HillClimb(double[] start)
        {
            Result = start;
            Random random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < Iterations; i++)
            {
                double[] newResult = new double[2];
                newResult = Result;

                int x = random.Next(0, 2);

                newResult[x] += 0.01 * ((i % 2) * 2 - 1);

                if (Function(newResult[0], newResult[1]) > Function(Result[0], Result[1]))
                {
                    Result = newResult;
                }
            }

            Trace.Write("Hillclimb best Fitness: " + Function(Result[0], Result[1]).ToString() + "\r\n");
        }

        public double RandomDouble(int x)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return LowerLimit[x] + (UpperLimit[x] - LowerLimit[x]) * random.NextDouble();
        }
    }
}
