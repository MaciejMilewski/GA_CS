using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public delegate double f(double x, double y);

    public class GeneticAlgorithm
    {
        public double PopulationSize { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationrRate { get; set; }
        //
        public Chromosome[] Population { get; set; }
        //
        public f FitnessFunction { get; set; }
        public double[] LowerLimit { get; set; }
        public double[] UpperLimit { get; set; }
        public double BestFitness { get; set; }
        public double[] BestGene { get; set; }
        //
        public int Iterations { get; set; }
        private int it { get; set; }
        private int evaluations { get; set; }

        public GeneticAlgorithm (double popSize, double crossoverRate, double mutationRate, int iterations, f f1, double[] lowerBound, double[] upperBound)
        {
            this.PopulationSize = popSize;
            this.CrossoverRate = crossoverRate;
            this.MutationrRate = mutationRate;
            this.Iterations = iterations;
            this.FitnessFunction = f1;
            this.LowerLimit = lowerBound;
            this.UpperLimit = upperBound;
        }

        public void GenerateInitialGenes()
        {

        }

        public void Initialize()
        {

        }

        public void PrintPopulation()
        {
            foreach (Chromosome c in Population)
                c.PrintChromosome();
        }

        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Iterations: " + Iterations + " Domain: " + "[" +
            LowerLimit + "," + UpperLimit + "]";
        }
    }
}
