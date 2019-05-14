using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public class domain
    {
        public Decimal lowerLimit { get; set; }
        public Decimal upperLimit { get; set; }
    }

    public class GeneticAlgorithm
    {
        public Decimal PopulationSize { get; set; }
        public Decimal CrossoverRate { get; set; }
        public Decimal MutationrRate { get; set; }
        public Decimal ElitismRate { get; set; }
        public int Iterations { get; set; }

        public Func<Decimal, Decimal> fitnessFunction;
        public domain Domain { get; set; }

        public GeneticAlgorithm (Decimal popSize, Decimal crossoverRate, Decimal mutationRate, Decimal elitismRate, int iterations, Func<Decimal, Decimal> f, Decimal lowerBound, Decimal upperBound)
        {
            this.PopulationSize = popSize;
            this.CrossoverRate = crossoverRate;
            this.MutationrRate = mutationRate;
            this.ElitismRate = elitismRate;
            this.Iterations = iterations;
            this.fitnessFunction = f;
            this.Domain.lowerLimit = lowerBound;
            this.Domain.upperLimit = upperBound;
        }

        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Elitism rate: " +
            ElitismRate + " Iterations: " + Iterations + " Domain: " + "[" +
            Domain.lowerLimit + "," + Domain.upperLimit + "]";
        }
    }
}
