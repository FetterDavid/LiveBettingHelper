using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Utilities
{
    public class PoissonDistribution
    {
        public static double PoissonPMF(double lambda, int k)
        {
            return Math.Exp(-lambda) * Math.Pow(lambda, k) / Factorial(k);
        }

        private static double Factorial(int n)
        {
            double result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
