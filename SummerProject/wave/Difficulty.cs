using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class Difficulty
    {
        public const int EASY = 1;
        public const int NORMAL = 2;
        public const int HARD = 3;

        //Active difficulty parameters
        // Timer:
        public static float TIMER1_DECREASINGMODE;
        public static float TIMER1_CONSTANTMODE;
        public static float BURSTMODE_SPAWN_INTERVAL;
        public static float BURSTMODE_WAVE_INTERVAL;
        //SpawnPoint + Timer:
        public static int BURST_WAVE_INIT;

        //Easy difficulty values:
        //Timer:      
        private const float TIMER1_DECREASINGMODE_EASY = 1f;
        private const float TIMER1_CONSTANTMODE_EASY = 5f;
        private const float BURSTMODE_SPAWN_INTERVAL_EASY = 0.25f;
        private const float BURSTMODE_WAVE_INTERVAL_EASY = 4.5f;
        //SpawnPoint + Timer:
        private const int BURST_WAVE_INIT_EASY = 2;

        //SpawnPoint:

        //Normal difficulty values:
        //Timer:      
        private const float TIMER1_DECREASINGMODE_NORMAL = 0.7f;
        private const float TIMER1_CONSTANTMODE_NORMAL = 4f;
        private const float BURSTMODE_SPAWN_INTERVAL_NORMAL = 0.2f;
        private const float BURSTMODE_WAVE_INTERVAL_NORMAL = 4.0f;
        //SpawnPoint + Timer:
        private const int BURST_WAVE_INIT_NORMAL = 3;

        //SpawnPoint:

        //Hard difficulty values:
        //Timer:      
        private const float TIMER1_DECREASINGMODE_HARD = 0.4f;
        private const float TIMER1_CONSTANTMODE_HARD = 3f;
        private const float BURSTMODE_SPAWN_INTERVAL_HARD = 0.15f;
        private const float BURSTMODE_WAVE_INTERVAL_HARD = 3f;
        //SpawnPoint + Timer:
        private const int BURST_WAVE_INIT_HARD = 5;

        //SpawnPoint:



        public Difficulty()
        {          
            EasyMode(); //DEFAULT  (Change difficultyMenu iff default mode is changed)
        }

        public void ChangeDifficulty(int newDifficulty)
        {
            switch (newDifficulty)
            {
                case EASY:
                    EasyMode();
                    break;
                case NORMAL:
                    Normal();
                    break;
                case HARD:
                    Hard();
                    break;
            }
        }

        private void Hard()
        {
            TIMER1_DECREASINGMODE = TIMER1_DECREASINGMODE_HARD;

            TIMER1_CONSTANTMODE = TIMER1_CONSTANTMODE_HARD;
            BURSTMODE_SPAWN_INTERVAL = BURSTMODE_SPAWN_INTERVAL_HARD;
            BURSTMODE_WAVE_INTERVAL = BURSTMODE_WAVE_INTERVAL_HARD;
            BURST_WAVE_INIT = BURST_WAVE_INIT_HARD;
        }

        private void Normal()
        {
            TIMER1_DECREASINGMODE = TIMER1_DECREASINGMODE_NORMAL;

            TIMER1_CONSTANTMODE = TIMER1_CONSTANTMODE_NORMAL;
            BURSTMODE_SPAWN_INTERVAL = BURSTMODE_SPAWN_INTERVAL_NORMAL;
            BURSTMODE_WAVE_INTERVAL = BURSTMODE_WAVE_INTERVAL_NORMAL;
            BURST_WAVE_INIT = BURST_WAVE_INIT_NORMAL;
        }

        private void EasyMode()
        {
            TIMER1_DECREASINGMODE = TIMER1_DECREASINGMODE_EASY;

            TIMER1_CONSTANTMODE = TIMER1_CONSTANTMODE_EASY;
            BURSTMODE_SPAWN_INTERVAL = BURSTMODE_SPAWN_INTERVAL_EASY;
            BURSTMODE_WAVE_INTERVAL = BURSTMODE_WAVE_INTERVAL_EASY;
            BURST_WAVE_INIT = BURST_WAVE_INIT_EASY;

        }
    }
}
