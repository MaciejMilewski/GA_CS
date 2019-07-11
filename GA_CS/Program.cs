using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GA_CS
{
    public class Program
    {

        public static void PrintTime(DateTime t1, DateTime t2)
        {
            string time = "Time = ";
            TimeSpan timeSpan = t2 - t1;

            time += timeSpan.Minutes.ToString("D2") + ":";
            time += timeSpan.Seconds.ToString("D2") + ".";
            time += timeSpan.Milliseconds.ToString("D3");
            time += " (min:sec.ms) " + "\r\n";

            Console.WriteLine(time);
        }

        public static void Main()
        {
            f ackley = new f(OptimizationFunctions.Ackley);
            f beale = new f(OptimizationFunctions.Beale);
            f booth = new f(OptimizationFunctions.Booth);
            f hoedlerTable = new f(OptimizationFunctions.HoedlerTable);

            Console.WriteLine();

            /*
            DateTime timeStart = DateTime.Now;
                      
            GeneticAlgorithm ga = new GeneticAlgorithm(200, 2, 1, 0.04, 30, ackley, FunctionConstants.bealeLowerBound, FunctionConstants.bealeUpperBound);
            ga.GeneticAlgorithmOptimization();
            ga.PrintResult();

            DateTime timeEndGA = DateTime.Now;
            PrintTime(timeStart, timeEndGA);
            */

            /*
            DateTime timeStart = DateTime.Now;
                      
            ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            ps.ParticleSwarmOptimization();
            ps.PrintResult();

            DateTime timeEndPSO = DateTime.Now;
            PrintTime(timeStart, timeEndPSO); 
            */

            TimeSpan tsGA = TimeSpan.MinValue;
            TimeSpan tsPSO = TimeSpan.MinValue;
            int fitnessScoreGA = 0;
            int fitnessScorePSO = 0;
            int timeScoreGA = 0;
            int timeScorePSO = 0;
            double sumGA = 0, sumPSO = 0;
            int iterations = 20;

            for (int i = 0; i < iterations; i++)
            {
                GeneticAlgorithm ga = new GeneticAlgorithm(100, 2, 1, 0.04, 45, ackley, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
                ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
                //Console.WriteLine("GA check = " + ga.BestFitness.ToString());
                DateTime gaStart = DateTime.Now;
                ga.GeneticAlgorithmOptimization();
                DateTime gaEnd = DateTime.Now;
                tsGA = gaEnd - gaStart;

                DateTime psoStart = DateTime.Now;
                ps.ParticleSwarmOptimization();
                DateTime psoEnd = DateTime.Now;
                tsPSO = psoEnd - psoStart;

                sumGA += ga.BestFitness;
                sumPSO += ps.BestResult;

                if (ga.BestFitness < ps.BestResult)
                    fitnessScoreGA++;
                else
                    fitnessScorePSO++;

                if (tsGA > tsPSO)
                    timeScorePSO++;
                else
                    timeScoreGA++;
            }

            Console.WriteLine("Iterations: " + iterations.ToString());
            Console.WriteLine("Avarage GA fitness = " + (sumGA / iterations).ToString());
            Console.WriteLine("Avarage PS fitness = " + (sumPSO / iterations).ToString());
            Console.WriteLine("GA fitness score = " + fitnessScoreGA);
            Console.WriteLine("GA time score = " + timeScoreGA);
            Console.WriteLine("PS fitness score = " + fitnessScorePSO);
            Console.WriteLine("PS time score = " + timeScorePSO);
            Console.WriteLine("\n");
        }
    }
}
