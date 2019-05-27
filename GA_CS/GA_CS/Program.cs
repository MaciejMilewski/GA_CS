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

            //double[] p1 = new double[2]; p1[0] = 1; p1[1] = 2;
            //double[] p2 = new double[2]; p2[0] = 3; p1[1] = 4;
            //Random random = new Random(Guid.NewGuid().GetHashCode());
            //int I = random.Next(2);
            ////Trace.Write("p1 [" + p1[0].ToString() + " " + p1[1].ToString() + "] \n");
            //Trace.Write("parent [" + p2[0].ToString() + " " + p2[1].ToString() + "] \n\n");
            //for (int i = I; i < 2; i++)
            //{
            //    p1[i - I] = p2[i];
            //}
            //for (int i = 0; i < I; i++)
            //{
            //    p1[i + I] = p2[i];
            //}
            //Trace.Write("child [" + p1[0].ToString() + " " + p1[1].ToString() + "] \n");

            Trace.WriteLine("\n");
        }
    }
}
