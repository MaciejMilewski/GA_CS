using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public static class FunctionConstants
    {
        public static double[] ackleyLowerBound = { -5, -5 };
        public static double[] ackleyUpperBound = { +5, +5 };
        public static double ackleyBestFitness = 0.0;
        public static double[] ackleyBestGenes = { 0.0, 0.0 };

        public static double[] bealeLowerBound = { -4.5, -4.5 };
        public static double[] bealeUpperBound = { +4.5, +4.5 };
        public static double bealeBestFitness = 0.0;
        public static double[] bealeBestGenes = { 3.0, 2.5 };

        public static double[] boothLowerBound = { -10, -10 };
        public static double[] boothUpperBound = { +10, +10 };
        public static double boothBestFitness = 0.0;
        public static double[] boothBestGenes = { 1.0, 3.0 };

        public static double[] hoelderLowerBound = { -10.0, -10.0 };
        public static double[] hoelderUpperBound = { +10.0, +10.0 };
        public static double hoeldeBestFitness = -19.2085;
        public static double[] hoelderBestGenes = { 8.05502, 9.66459 };
    }
}
