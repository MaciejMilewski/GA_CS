using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{

    public class GeneticAlgorithm
    {
        public double PopulationSize { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationrRate { get; set; }
        public double ElitismRate { get; set; }

        public int Iterations { get; set; }

        public f fitnessFunction;
        public double lowerLimit { get; set; }
        public double upperLimit { get; set; }

        public double bestFitness { get; set; }

        public GeneticAlgorithm (double popSize, double crossoverRate, double mutationRate, double elitismRate, int iterations, f f1, double lowerBound, double upperBound)
        {
            this.PopulationSize = popSize;
            this.CrossoverRate = crossoverRate;
            this.MutationrRate = mutationRate;
            this.ElitismRate = elitismRate;
            this.Iterations = iterations;
            this.fitnessFunction = f1;
            this.lowerLimit = lowerBound;
            this.upperLimit = upperBound;
        }

        public override string ToString()
        {
            return "Population size: " + PopulationSize + " Crossover rate: " +
            CrossoverRate + " Mutation rate: " + MutationrRate + " Elitism rate: " +
            ElitismRate + " Iterations: " + Iterations + " Domain: " + "[" +
            lowerLimit + "," + upperLimit + "]";
        }
    }
}
