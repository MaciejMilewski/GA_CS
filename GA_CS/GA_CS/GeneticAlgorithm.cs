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
        public int PopulationSize { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationrRate { get; set; }
        public Chromosome[] Population { get; set; }
        public int GeneSize { get; set; }
        //
        public f FitnessFunction { get; set; }
        public double[] LowerLimit { get; set; }
        public double[] UpperLimit { get; set; }
        public double BestFitness { get; set; }
        public double[] BestGene { get; set; }
        //
        public int Iterations { get; set; }
        private int It { get; set; }
        private int Evaluations { get; set; }

        public GeneticAlgorithm (int popSize, int geneSize, double crossoverRate, double mutationRate, int iterations, f f1, double[] lowerBound, double[] upperBound)
        {
            this.PopulationSize = popSize;
            this.GeneSize = geneSize;
            this.CrossoverRate = crossoverRate;
            this.MutationrRate = mutationRate;
            this.Iterations = iterations;
            this.FitnessFunction = f1;
            this.LowerLimit = lowerBound;
            this.UpperLimit = upperBound;
            this.Population = InitializeArray<Chromosome>(popSize);
            this.BestGene = new double[GeneSize];
            this.Evaluations = PopulationSize;
        }

        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        public void GenerateInitialGenes()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            BestFitness = double.MaxValue;
            double[] tmp = new double[GeneSize];
            
            for (int i = 0; i < PopulationSize; i++)
            {
                for (int j = 0; j < GeneSize; j++)
                {
                    tmp[j] = RandomGene(j, random);
                    Population[i].Genes[j] = tmp[j];
                }
                Population[i].Fitness = FitnessFunction(tmp[0], tmp[1]);
                if (Population[i].Fitness < BestFitness)
                {
                    BestFitness = Population[i].Fitness;

                    for (int k = 0; k < GeneSize; k++)
                    {
                        BestGene[k] = tmp[k];
                    }
                }
            }
        }

        public void Initialize()
        {

        }

        public double RandomGene (int x, Random random)
        {
            return LowerLimit[x] + (UpperLimit[x] - LowerLimit[x]) * random.NextDouble();
        }

        public void PrintPopulation()
        {
            foreach (Chromosome c in Population)
                c.PrintChromosome();
        }

        // TODO add new params 
        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Iterations: " + Iterations + " Domain: " + "[" +
            LowerLimit + "," + UpperLimit + "]";
        }
    }
}
