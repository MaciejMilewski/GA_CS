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

            Trace.WriteLine(time);
        }

        public static void Main()
        {
            Trace.WriteLine("\n");

            //DateTime timeStart = DateTime.Now;

            f ackley = new f(OptimizationFunctions.Ackley);
            f beale = new f(OptimizationFunctions.Beale);
            f booth = new f(OptimizationFunctions.Booth);
            f hoedlerTable = new f(OptimizationFunctions.HoedlerTable);

            //GeneticAlgorithm ga = new GeneticAlgorithm(200, 2, 1, 0.04, 30, ackley, FunctionConstants.bealeLowerBound, FunctionConstants.bealeUpperBound);
            //ga.GeneticAlgorithmOptimization();
            //ga.PrintResult();

            //DateTime timeEndGA = DateTime.Now;
            //PrintTime(timeStart, timeEndGA);

            //ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            //ps.ParticleSwarmOptimization();
            //ps.PrintResult();

            //DateTime timeEndPSO = DateTime.Now;

            //PrintTime(timeStart, timeEndPSO);
            //GeneticAlgorithm ga = new GeneticAlgorithm(200, 2, 1, 0.04, 30, ackley, FunctionConstants.bealeLowerBound, FunctionConstants.bealeUpperBound);
            //ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);

            List<GeneticAlgorithm> listGA = new List<GeneticAlgorithm>();
            List<ParticleSwarm> listPSO = new List<ParticleSwarm>();
            double[,] ResultsGA = new double[1000, 2];
            double[,] ResultsPSO = new double[1000, 2];
            TimeSpan tsGA = TimeSpan.MinValue;
            TimeSpan tsPSO = TimeSpan.MinValue;
            int fitnessScoreGA = 0;
            int fitnessScorePSO = 0;
            int timeScoreGA = 0;
            int timeScorePSO = 0;
            double sumGA = 0, sumPSO = 0;

            for (int i = 0; i < 10; i++)
            {
                GeneticAlgorithm ga = new GeneticAlgorithm(200, 2, 1, 0.04, 30, ackley, FunctionConstants.bealeLowerBound, FunctionConstants.bealeUpperBound);
                ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
                //Trace.WriteLine("GA check = " + ga.BestFitness.ToString());
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

                ga = null;
                ps = null;
            }

            Trace.WriteLine("Avarage GA fitness = " + (sumGA / 10).ToString());
            Trace.WriteLine("Avarage PS fitness = " + (sumPSO / 10).ToString());
            Trace.WriteLine("GA fitness score = " + fitnessScoreGA);
            Trace.WriteLine("GA time score = " + timeScoreGA);
            Trace.WriteLine("PS fitness score = " + fitnessScorePSO);
            Trace.WriteLine("PS time score = " + timeScorePSO);

            Trace.WriteLine("\n");
        }
    }
}
