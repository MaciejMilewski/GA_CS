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
        private bool action { get; set; }
        private int selectedID1 { get; set; }
        private int selectedID2 { get; set; }
        private int[] selected { get; set; }
        private int ID { get; set; }
        private double[] child { get; set; }
        private double[] parentsGenes { get; set; }

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
            this.action = false;
            this.selectedID1 = 0;
            this.selectedID2 = 0;
            this.selected = new int[2];
            this.ID = 0;
            this.child = new double[GeneSize];
            this.parentsGenes = new double[GeneSize];
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
            int x, y;

            for (int i = 0; i < 2; i++)
            {
                x = random.Next(PopulationSize);
                y = random.Next(PopulationSize);

                if (Population[y].Fitness < Population[x].Fitness)
                {
                    selected[i] = y;
                }
                else
                    selected[i] = x;
            }
        }

        public void Mutation()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() <= MutationrRate)
            {
                ID = random.Next(GeneSize);
                int chosen = selected[random.Next(2)];

                for (int i = 0; i < GeneSize; i++)
                {
                    if (i != ID)
                    {
                        child[i] = Population[chosen].Genes[i];
                    }
                    else
                    {
                        child[i] += (random.Next(0, 1) * 2 - 1) * random.NextDouble();

                        if (child[i] < LowerLimit[i])
                        {
                            child[i] = LowerLimit[i];
                        }
                        else if (child[i] > UpperLimit[i])
                        {
                            child[i] = UpperLimit[i];
                        }
                    }
                }

                action = true;
            }
        }

        public void Crossover()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (random.NextDouble() <= CrossoverRate)
            {
                for (int i = 0; i < GeneSize; i++)
                {
                    parentsGenes[i] = Population[selected[random.Next(GeneSize)]].Genes[i];
                }

                for (int i = ID; i < GeneSize; i++)
                {
                    child[i - ID] = parentsGenes[i];
                }
                for (int i = 0; i < ID; i++)
                {
                    child[i + ID] = parentsGenes[i];
                }

                action = true;
            }

            It++;
        }

        public void Elitism()
        {
            if(action)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                double childFitness = FitnessFunction(child[0], child[1]);

                double maximumFitness = double.MinValue;
                int maximumFitnessID = 0;

                for (int i = 0; i < PopulationSize; i++)
                {
                    if (Population[i].Fitness > maximumFitness)
                    {
                        maximumFitness = Population[i].Fitness;
                        maximumFitnessID = i;
                    }
                }

                ID = random.Next(PopulationSize);

                if (childFitness < maximumFitness)
                {
                    Population[ID].Fitness = childFitness;
                    for (int i = 0; i < GeneSize; i++)
                    {
                        Population[ID].Genes[i] = child[i];
                    }

                    if (childFitness < BestFitness)
                    {
                        BestFitness = childFitness;

                        for (int i = 0; i < GeneSize; i++)
                        {
                            BestGene[i] = child[i];
                        }
                    }
                }

                ID = 0;
                action = false;
            }
        }

        public void GeneticAlgorithmOptimization()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());

            GenerateInitialGenes();

            while (It < Iterations)
            {
                for (int i = 1; i < random.Next(PopulationSize); i++)
                {
                    TournamentSelection();
                    Crossover();
                    Mutation();
                    Elitism();
                }
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
