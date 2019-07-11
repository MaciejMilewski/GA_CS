using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public class ParticleSwarm
    {
        private int Size { get; set; }
        private int Particles { get; set; }
        private int Iterations { get; set; }
        private int It { get; set; }
        private double[] LowerLimit { get; set; }
        private double[] UpperLimit { get; set; }
        private f Function { get; set; }
        public double BestResult { get; set; }
        private double[] BestParameters { get; set; }

        //
        private double[,] x { get; set; }
        private double[] y { get; set; }
        private double[,] v1 { get; set; }
        private double[,] v2 { get; set; }
        private double[,] currentBestParameters { get; set; }
        private double[] currentBestResult { get; set; }

        public ParticleSwarm(f function, int size, int particles, int iterations, double[] lowerlimit, double[] upperlimit)
        {
            this.Function = function;
            this.Size = size;
            this.Particles = particles;
            this.Iterations = iterations;
            this.LowerLimit = lowerlimit;
            this.UpperLimit = upperlimit;
            this.BestParameters = new double[Size];
            this.It = 0;
            this.BestResult = double.MaxValue;
            this.x = new double[Particles, Size];
            this.y = new double[Size];
            this.v1 = new double[Particles, Size];
            this.v2 = new double[Particles, Size];
            this.currentBestParameters = new double[Particles, Size];
            this.currentBestResult = new double[Particles];
        }

        public ParticleSwarm() { }

        public void InitializePSO()
        {

            for (int i = 0; i < Particles; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    y[j] = RandomDoubleInRange(j);
                    x[i, j] = y[j];
                    v1[i, j] = 0;
                }

                currentBestResult[i] = double.MaxValue;
            }

        }

        public void SearchLocal()
        {
            for (int i = 0; i < Particles; i++)
            {
                for (int j = 0; j < Size; j++)
                    y[j] = x[i, j];

                double possibleBestResult = Function(y[0], y[1]);
                It++;

                if (possibleBestResult < currentBestResult[i])
                {
                    currentBestResult[i] = possibleBestResult;

                    for (int j = 0; j < Size; j++)
                        currentBestParameters[i, j] = y[j];
                }
            }
        }

        public void SearchGlobal()
        {
            for (int i = 0; i < Particles; i++)
            {
                if (currentBestResult[i] < BestResult)
                {
                    BestResult = currentBestResult[i];
                    for (int j = 0; j < Size; j++)
                        BestParameters[j] = currentBestParameters[i, j];
                }
            }
        }

        public void UpdateVelocity()
        {
            for (int i = 0; i < Particles; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    v2[i, j] = GenerateVelocity(i, j, v1, currentBestParameters, x, currentBestResult);
                }
            }
        }

        public void CheckPositionRange()
        {
            for (int i = 0; i < Particles; i++)
            {
                bool InRange = true;

                for (int j = 0; InRange && j < Size; j++)
                {
                    y[j] = x[i, j] + v2[i, j];
                    InRange = y[j] >= LowerLimit[j] && y[j] <= UpperLimit[j];
                }

                if (InRange)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        x[i, j] = y[j];
                        v1[i, j] = v2[i, j];
                    }
                }
            }
        }

        public void ParticleSwarmOptimization()
        {
            InitializePSO();

            while (It < Iterations)
            {
                SearchLocal();
                SearchGlobal();
                UpdateVelocity();
                CheckPositionRange();
            }
        }

        public double RandomDoubleInRange(int x)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return (LowerLimit[x] + (UpperLimit[x] - LowerLimit[x]) * random.NextDouble());
        }

        public double GenerateVelocity(int i, int j, double[,] v1, double[,] currentBestParameters, double[,] x, double[] currentBestResult)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return v1[i, j] + random.NextDouble() * (currentBestParameters[i, j] - x[i, j]) + random.NextDouble() * (currentBestResult[j] - x[i, j]);
        }

        public void PrintResult()
        {
            Trace.Write("Best value: " + BestResult + "\r\n");
            Trace.Write("Best X: " + BestParameters[0] + "\r\n");
            Trace.Write("Best Y: " + BestParameters[1] + "\r\n");
        }

        public double PsResult()
        {
            return BestResult;
        }

    }
}
