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
            Random random = new Random(Guid.NewGuid().GetHashCode());
            It = 0;

            while(It < Iterations)
            {
                bool end = false;
                //SELECTION - tournament
                List<int> parents = new List<int>();

                for(int i = 0; i < GeneSize; i++)
                {
                    parents.Add(random.Next(PopulationSize));
                }

                double minimalFitness = Population[parents[0]].Fitness;
                int minimalID = 0;

                for(int i = 1; i < parents.Count; i++)
                {
                    if(Population[parents[i]].Fitness < minimalFitness)
                    {
                        minimalID = parents[i];
                        minimalFitness = Population[parents[i]].Fitness;
                    }
                }

                //MUTATION
                double[] tmp1 = new double[GeneSize];
                int ID = random.Next(GeneSize);

                if(random.NextDouble() < MutationrRate)
                {
                    for(int i = 0; i < GeneSize; i++)
                    {
                        if (i != ID)
                        {
                            tmp1[i] = Population[minimalID].Genes[i];
                        }
                        else
                        {
                            int x = 0;
                            if (random.Next(2) == 1)
                                x = 1;
                            else
                                x = -1;

                            tmp1[i] += x * random.NextDouble();

                            if (tmp1[i] < LowerLimit[i])
                            {
                                tmp1[i] = LowerLimit[i];
                            }
                            else if (tmp1[i] > UpperLimit[i])
                            {
                                tmp1[i] = UpperLimit[i];
                            }
                        }
                    }
                    end = true;
                }
                //CROSSOVER
                if(random.NextDouble() < CrossoverRate)
                {
                    double[] tmp2 = new double[GeneSize];

                    for(int i = 0; i < GeneSize; i++)
                    {
                        tmp2[i] = Population[minimalID].Genes[i];
                    }
                    for(int i = ID; i < GeneSize; i++)
                    {
                        tmp1[i - ID] = tmp2[i];
                    }
                    for(int i = 0; i < ID; i++)
                    {
                        tmp1[i + ID] = tmp2[i];
                    }

                    end = true;
                }

                It++;

                //ELITISM
                if (end)
                {
                    Evaluations++;
                    double childFitness = FitnessFunction(tmp1[0], tmp1[1]);

                    double maximumFitness = double.MinValue;
                    List<int> maximumID = new List<int>();

                    for(int i = 0; i < PopulationSize; i++)
                    {
                        if(Population[i].Fitness > maximumFitness)
                            maximumFitness = Population[i].Fitness;
                    }

                    for(int i = 0; i < PopulationSize; i++)
                    {
                        if(maximumFitness == Population[i].Fitness)
                            maximumID.Add(i);
                    }

                    ID = random.Next(maximumID.Count);

                    if(childFitness < maximumFitness)
                    {
                        Population[ID].Fitness = childFitness;

                        for(int i = 0; i < GeneSize; i++)
                        {
                            Population[ID].Genes[i] = tmp1[i];
                        }

                        if(childFitness < BestFitness)
                        {
                            BestFitness = childFitness;

                            for(int i = 0; i < GeneSize; i++)
                            {
                                BestGene[i] = tmp1[i];
                            }
                        }
                    }
                    //TODO: CHECK FOR COVERGENCE
                    
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

        // TODO add new params 
        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Iterations: " + Iterations + " Domain: " + "[" +
            LowerLimit + "," + UpperLimit + "]";
        }
    }
}
