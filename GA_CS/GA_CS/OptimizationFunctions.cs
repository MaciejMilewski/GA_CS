using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_CS
{
    public class OptimizationFunctions
    {
        public static double Beale(double x, double y)
        {
            return (1.5 - x + x * y) * (1.5 - x + x * y) + (2.25 - x + x * y * y) * (2.25 - x + x * y * y) + (2.625 - x + x * y * y * y) * (2.625 - x + x * y * y * y);
        }

        public static double Ackley(double x, double y)
        {
            return -20 * Math.Exp(-0.2 * Math.Sqrt(0.5 * (x * x + y * y))) -
                Math.Exp(0.5 * (Math.Cos(2 * Math.PI * x) + Math.Cos(2 * Math.PI * y))) + Math.E + 20;
        }

        public static double Booth(double x, double y)
        {
            return (x + 2 * y - 7) * (x + 2 * y - 7) + (2 * x + y - 5) * (2 * x + y - 5);
        }

        public static double HoedlerTable(double x, double y)
        {
            return -Math.Abs(Math.Sin(x) * Math.Cos(y) *
                Math.Exp(Math.Abs(1 - Math.Sqrt(x * x + y * y) / Math.PI)));
        }
    }
}
