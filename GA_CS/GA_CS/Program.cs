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

            DateTime timeStart = DateTime.Now;

            f ackley = new f(OptimizationFunctions.Ackley);
            f beale = new f(OptimizationFunctions.Beale);
            f booth = new f(OptimizationFunctions.Booth);
            f hoedlerTable = new f(OptimizationFunctions.HoedlerTable);

            GeneticAlgorithm ga = new GeneticAlgorithm(120000, 2, 0.05, 0.04, 450, ackley, FunctionConstants.bealeLowerBound, FunctionConstants.bealeUpperBound);
            ga.GeneticAlgorithmOptimization();
            ga.PrintResult();

            DateTime timeEndGA = DateTime.Now;
            PrintTime(timeStart, timeEndGA);

            ParticleSwarm ps = new ParticleSwarm(ackley, 2, 8000, 1000, FunctionConstants.ackleyLowerBound, FunctionConstants.ackleyUpperBound);
            ps.ParticleSwarmOptimization();
            ps.PrintResult();

            DateTime timeEndPSO = DateTime.Now;

            PrintTime(timeStart, timeEndPSO);

            Trace.WriteLine("\n");
        }
    }
}
