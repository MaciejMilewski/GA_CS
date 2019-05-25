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
        private int Evaluations { get; set; }
        private bool end { get; set; }
        private int minimalID { get; set; }
        private int ID { get; set; }
        private double[] possibleX { get; set; }
        private double[] possibleY { get; set; }

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
            this.It = 0;
            this.end = false;
            this.minimalID = 0;
            this.ID = 0;
            this.possibleX = new double[GeneSize];
            this.possibleY = new double[GeneSize];
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
            List<int> parents = new List<int>();

            for (int i = 0; i < GeneSize; i++)
            {
                parents.Add(random.Next(PopulationSize));
            }

            double minimalFitness = Population[parents[0]].Fitness;

            for (int i = 1; i < parents.Count; i++)
            {
                if (Population[parents[i]].Fitness < minimalFitness)
                {
                    minimalID = parents[i];
                    minimalFitness = Population[parents[i]].Fitness;
                }
            }
        }

        public void Mutation()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            ID = random.Next(GeneSize);

            if (random.NextDouble() < MutationrRate)
            {
                for (int i = 0; i < GeneSize; i++)
                {
                    if (i != ID)
                    {
                        possibleX[i] = Population[minimalID].Genes[i];
                    }
                    else
                    {
                        int x = 0;
                        if (random.Next(2) == 1)
                            x = 1;
                        else
                            x = -1;

                        possibleX[i] += x * random.NextDouble();

                        if (possibleX[i] < LowerLimit[i])
                        {
                            possibleX[i] = LowerLimit[i];
                        }
                        else if (possibleX[i] > UpperLimit[i])
                        {
                            possibleX[i] = UpperLimit[i];
                        }
                    }
                }
                end = true;
            }
        }

        public void Crossover()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() < CrossoverRate)
            {
                for (int i = 0; i < GeneSize; i++)
                {
                    possibleY[i] = Population[minimalID].Genes[i];
                }
                for (int i = ID; i < GeneSize; i++)
                {
                    possibleX[i - ID] = possibleY[i];
                }
                for (int i = 0; i < ID; i++)
                {
                    possibleX[i + ID] = possibleY[i];
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
                Evaluations++;
                double childFitness = FitnessFunction(possibleX[0], possibleX[1]);

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
                        Population[ID].Genes[i] = possibleX[i];
                    }

                    if (childFitness < BestFitness)
                    {
                        BestFitness = childFitness;

                        for (int i = 0; i < GeneSize; i++)
                        {
                            BestGene[i] = possibleX[i];
                        }
                    }
                }
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
