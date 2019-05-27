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
        private int PopulationSize { get; set; }
        private double CrossoverRate { get; set; }
        private double MutationrRate { get; set; }
        private Chromosome[] Population { get; set; }
        private int GeneSize { get; set; }
        //
        private f FitnessFunction { get; set; }
        private double[] LowerLimit { get; set; }
        private double[] UpperLimit { get; set; }
        private double BestFitness { get; set; }
        private double[] BestGene { get; set; }
        //
        private int Iterations { get; set; }
        private int It { get; set; }
        private bool end { get; set; }
        private int minimalID { get; set; }
        private int ID { get; set; }
        private double[] parent1 { get; set; }
        private double[] parent2 { get; set; }

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
            this.It = 0;
            this.end = false;
            this.minimalID = 0;
            this.ID = 0;
            this.parent1 = new double[GeneSize];
            this.parent2 = new double[GeneSize];
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

        public void TournamentSelection()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            int i = random.Next(PopulationSize);
            int j = random.Next(PopulationSize);

            if (Population[j].Fitness < Population[i].Fitness)
            {
                minimalID = j;
            }
            else
                minimalID = i;
        }

        public void Mutation()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() <= MutationrRate)
            {
                ID = random.Next(GeneSize);

                for (int i = 0; i < GeneSize; i++)
                {
                    if (i != ID)
                    {
                        parent1[i] = Population[minimalID].Genes[i];
                    }
                    else
                    {
                        parent1[i] += (random.Next(0, 1) * 2 - 1) * random.NextDouble();

                        if (parent1[i] < LowerLimit[i])
                        {
                            parent1[i] = LowerLimit[i];
                        }
                        else if (parent1[i] > UpperLimit[i])
                        {
                            parent1[i] = UpperLimit[i];
                        }
                    }
                }
            }
        }

        public void Crossover()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() <= CrossoverRate)
            {
                for (int i = 0; i < GeneSize; i++)
                {
                    parent2[i] = Population[minimalID].Genes[i];
                }
                for (int i = ID; i < GeneSize; i++)
                {
                    parent1[i - ID] = parent2[i];
                }
                for (int i = 0; i < ID; i++)
                {
                    parent1[i + ID] = parent2[i];
                }

                end = true;
            }

            It++;
        }

        public void Elitism()
        {
            if(end)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                double childFitness = FitnessFunction(parent1[0], parent1[1]);

                double maximumFitness = double.MinValue;
                List<int> maximumID = new List<int>();

                for (int i = 0; i < PopulationSize; i++)
                {
                    if (Population[i].Fitness > maximumFitness)
                        maximumFitness = Population[i].Fitness;
                }

                for (int i = 0; i < PopulationSize; i++)
                {
                    if (maximumFitness == Population[i].Fitness)
                        maximumID.Add(i);
                }

                ID = random.Next(maximumID.Count);

                if (childFitness < maximumFitness)
                {
                    Population[ID].Fitness = childFitness;

                    for (int i = 0; i < GeneSize; i++)
                    {
                        Population[ID].Genes[i] = parent1[i];
                    }

                    if (childFitness < BestFitness)
                    {
                        BestFitness = childFitness;

                        for (int i = 0; i < GeneSize; i++)
                        {
                            BestGene[i] = parent1[i];
                        }
                    }
                }
                end = false;
            }
        }

        public void GeneticAlgorithmOptimization()
        {
            GenerateInitialGenes();

            while (It < Iterations)
            {
                TournamentSelection();
                Mutation();
                Crossover();
                Elitism();
            }
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

        public void PrintResult()
        {
            Trace.Write("Best value = " + BestFitness.ToString() + "\r\n");
            Trace.Write("Gene x value: " + BestGene[0].ToString() + "\r\n");
            Trace.Write("Gene y value: " + BestGene[1].ToString() + "\r\n");
        }

        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Iterations: " + Iterations + " Domain: " + "[" +
            LowerLimit + "," + UpperLimit + "]";
        }
    }
}
