using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.achievements
{
    public class Traits
    {
        //Achievement constants
        public const int NORMAL_DIFFICULTY = 0;
        public const int HARD_DIFFICULTY = 1;
        public const int WAVE11 = 2;
        public const int WAVE21 = 3;



        //not unlockables that are carried from game session to other session      
        public const int ELITE = 4;

        public const int UNLOCKABLES_CONSTANT = 4; //normal + hard difficulty and waves are unlockables carried over into other games --> constant is 4

        public static readonly Dictionary<int, int> KILLTHRESHOLD =
            new Dictionary<int, int>()
            {
                 {NORMAL_DIFFICULTY, 50 },
                 {HARD_DIFFICULTY,100 }
            };

        public static readonly Dictionary<int, int> SCORETHRESHOLD =
            new Dictionary<int, int>()
            {
                 {NORMAL_DIFFICULTY, 5000 },
                 {HARD_DIFFICULTY,25000 },
                 {ELITE, 100000 }
            };

        public static readonly Dictionary<int, int> TIMETHRESHOLD =
            new Dictionary<int, int>()
            {
                 //{WAVE_MODE, 10 }                
            };

        //public static readonly Dictionary<int, int> LEVELTHRESHOLD =
        //    new Dictionary<int, int>()
        //    {
        //        {BOSS_SLAIN1, 0 },
        //        {BOSS_SLAIN2, 10 },
        //        {BOSS_SLAIN3, 20 }
        //    };



        public static Trait KILLS = new Trait("Kills");
        public static Trait SCORE = new Trait("Score");
        public static Trait CURRENCY = new Trait("Currency");
        public static Trait SHOTSFIRED = new Trait("Shots Fired");
        public static Trait SHOTSHIT = new Trait("Shots Hit");
        public static Trait ENEMIESSPAWNED = new Trait("Enemies Spawned");
        public static Trait TIME = new Trait("Time Elapsed");
        public static Trait LEVEL = new Trait("Time Elapsed");

       
    }
}
