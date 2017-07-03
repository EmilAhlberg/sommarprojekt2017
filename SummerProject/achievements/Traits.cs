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
        public const int WAVE_MODE = 2;
        public const int BURST_MODE = 3;

        //Thresholds
        public static readonly Dictionary<int, int> KILLTHRESHOLD =
            new Dictionary<int, int>()
            {
                 {NORMAL_DIFFICULTY, 10 },
                 {HARD_DIFFICULTY,100 }
            };

        public static readonly Dictionary<int, int> SCORETHRESHOLD =
            new Dictionary<int, int>()
            {
                 {NORMAL_DIFFICULTY, 1000 },
                 {HARD_DIFFICULTY,20000 }
            };

        public static readonly Dictionary<int, int> TIMETHRESHOLD =
            new Dictionary<int, int>()
            {
                 {WAVE_MODE, 10 }                
            };

        public static readonly Dictionary<int, int> LEVELTHRESHOLD =
            new Dictionary<int, int>()
            {
                {BURST_MODE, 10 }
            };



        public static Trait KillTrait = new Trait("Kills");
        public static Trait ScoreTrait = new Trait("Score");
        public static Trait ShotsFiredTrait = new Trait("Shots Fired");
        public static Trait ShotsHitTrait = new Trait("Shots Hit");
        public static Trait EnemiesSpawnedTrait = new Trait("Enemies Spawned");
        public static Trait TimeTrait = new Trait("Time Elapsed");
        public static Trait LevelTrait = new Trait("Time Elapsed");

    }
}
