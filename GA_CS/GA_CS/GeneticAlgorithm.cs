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
        private Chromosome[] TemporaryPopulation { get; set; }
        private int GeneSize { get; set; }
        //
        private f FitnessFunction { get; set; }
        private double[] LowerLimit { get; set; }
        private double[] UpperLimit { get; set; }
        public double BestFitness { get; set; }
        private double[] BestGene { get; set; }
        //
        private int Iterations { get; set; }
        private int It { get; set; }
        private int[] selected { get; set; }
        private int ID { get; set; }
        private double[] child { get; set; }
        private double[] parentsGenes { get; set; }
        private int tmpID { get; set; }

        public GeneticAlgorithm(int popSize, int geneSize, double crossoverRate, double mutationRate, int iterations, f f1, double[] lowerBound, double[] upperBound)
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
            this.TemporaryPopulation = InitializeArray<Chromosome>(popSize);
            this.BestGene = new double[GeneSize];
            this.It = 0;
            this.selected = new int[2];
            this.ID = 0;
            this.tmpID = 0;
            this.child = new double[GeneSize];
            this.parentsGenes = new double[GeneSize];
        }

        public GeneticAlgorithm() { }

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

            }
        }

        public void InsertChild()
        {
            TemporaryPopulation[tmpID].Fitness = FitnessFunction(child[0], child[0]);
            for (int i = 0; i < GeneSize; i++)
            {
                TemporaryPopulation[tmpID].Genes[i] = child[i];
            }
            
            if (TemporaryPopulation[tmpID].Fitness < BestFitness)
            {
                BestFitness = TemporaryPopulation[tmpID].Fitness;

                for (int i = 0; i < GeneSize; i++)
                {
                    BestGene[i] = child[i];
                }
            }

            tmpID++;
        }

        public void GeneticAlgorithmOptimization()
        {
            GenerateInitialGenes();

            while (It < Iterations) 
            {
                for (int i = 0; i < PopulationSize; i++)
                {
                    TournamentSelection();
                    Crossover();
                    Mutation();
                    InsertChild(); 
                } 
                Array.Copy(TemporaryPopulation, Population, PopulationSize);
                tmpID = 0;
                It++;
            } 
        }

        public double RandomGene(int x, Random random)
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

        public double GaResult()
        {
            double tmp = BestFitness;
            BestFitness = 0;
            return tmp;
        }

        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Iterations: " + Iterations + " Domain: " + "[" +
            LowerLimit + "," + UpperLimit + "]";
        }
    }
}
