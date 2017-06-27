using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    static class SRandom
    {
        static Random rand;

        static SRandom()
        {
            rand = new Random();
        }

        public static int Next(int lowerBound, int maxBound)
        {
            return rand.Next(lowerBound, maxBound);
        }

        public static float NextFloat()
        {
            return (float)rand.NextDouble();
        }
    }
}
