using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.achievements
{
    public class Traits
    {
        //Trait constants
        public const int NORMAL = 0;
        public const int HARD = 1;
        //Thresholds
        public static readonly int[] KILLTHRESHOLD = { 10,100 };
        public static readonly int[] SCORETHRESHOLD = { 1000, 10000 };

        
        public static Trait KillTrait = new Trait("Kills");
        public static Trait ScoreTrait = new Trait("Score");

    }
}
