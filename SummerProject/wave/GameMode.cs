using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class GameMode
    {

        public const int DEBUG_MODE = 0;

        //Modes for the timer
        public const int CONSTANT_TIME = 1;
        public const int DECREASING_TIME = 2;

        //Modes for the point generator
        public const int RANDOM_SINGLESPAWN = 10;
        public const int RANDOM_WAVESPAWN = 11;

        public int TimeMode { get; set; }
        public int SpawnMode { get; set; }

        public GameMode()
        {
            TimeMode = DECREASING_TIME;
            SpawnMode = RANDOM_SINGLESPAWN;
        }

        public void Reset()
        {
            TimeMode = DECREASING_TIME;
            SpawnMode = RANDOM_SINGLESPAWN;
        }
    }
}
