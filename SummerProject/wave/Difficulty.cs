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

        /*
         * Active difficulty parameters:
         */
        // Timer:
        public static float TIMER1_DECREASINGMODE;
        public static float TIMER1_CONSTANTMODE;
        public static float BURSTMODE_SPAWN_INTERVAL;
        public static float BURSTMODE_WAVE_INTERVAL;      
        //SpawnPoint + Timer:
        public static int BURST_WAVE_INIT;
        //SpawnPoint
        public static int WAVESPAWN_INIT;
        //Drops
        public static double DROP_RATE;

        //Enemies
        public static float CAN_SHOOT_RATE;
        public static float IS_SPEEDY_RATE;


        /*
         * Difficulty values: 
         */
        // <tweakZone>
        //Timer:      
        private const float TIMER1_DECREASINGMODE_EASY = 1f;
        private const float TIMER1_DECREASINGMODE_NORMAL = 0.7f;
        private const float TIMER1_DECREASINGMODE_HARD = 0.4f;

        private const float TIMER1_CONSTANTMODE_EASY = 5f;
        private const float TIMER1_CONSTANTMODE_NORMAL = 4f;
        private const float TIMER1_CONSTANTMODE_HARD = 3f;

        private const float BURSTMODE_SPAWN_INTERVAL_EASY = 0.25f;
        private const float BURSTMODE_SPAWN_INTERVAL_NORMAL = 0.2f;
        private const float BURSTMODE_SPAWN_INTERVAL_HARD = 0.15f;

        private const float BURSTMODE_WAVE_INTERVAL_EASY = 4.5f;
        private const float BURSTMODE_WAVE_INTERVAL_NORMAL = 4.0f;
        private const float BURSTMODE_WAVE_INTERVAL_HARD = 3f;

        //SpawnPoint + Timer:
        private const int BURST_WAVE_INIT_EASY = 2;
        private const int BURST_WAVE_INIT_NORMAL = 3;
        private const int BURST_WAVE_INIT_HARD = 5;

        //SpawnPoint:
        private const int WAVESPAWN_INIT_EASY = 1; // - wavemode adds this value to spawnSize, every level 
        private const int WAVESPAWN_INIT_NORMAL = 2;
        private const int WAVESPAWN_INIT_HARD = 3;
        
        //Drops:        
        private const float DROP_RATE_EASY = 0.4f; // - i.e. 40% chance of drop on death
        private const float DROP_RATE_NORMAL = 0.25f;
        private const float DROP_RATE_HARD = 0.1f;

        //Enemies
        private const float CAN_SHOOT_RATE_EASY = 0.2f; // - i.e. 20% 'chance' of shooting enemy spawning
        private const float CAN_SHOOT_RATE_NORMAL = 0.4f;
        private const float CAN_SHOOT_RATE_HARD = 0.6f;

        private const float IS_SPEEDY_RATE_EASY = 0.05f; // - i.e. 5% 'chance' of shuupedo enemy spawning
        private const float IS_SPEEDY_RATE_NORMAL = 0.15f;
        private const float IS_SPEEDY_RATE_HARD = 0.25f;
        // </tweakZone>




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

            WAVESPAWN_INIT = WAVESPAWN_INIT_HARD;

            DROP_RATE = DROP_RATE_HARD;

            CAN_SHOOT_RATE = CAN_SHOOT_RATE_HARD;
            IS_SPEEDY_RATE = IS_SPEEDY_RATE_HARD;
        }

        private void Normal()
        {
            TIMER1_DECREASINGMODE = TIMER1_DECREASINGMODE_NORMAL;

            TIMER1_CONSTANTMODE = TIMER1_CONSTANTMODE_NORMAL;
            BURSTMODE_SPAWN_INTERVAL = BURSTMODE_SPAWN_INTERVAL_NORMAL;
            BURSTMODE_WAVE_INTERVAL = BURSTMODE_WAVE_INTERVAL_NORMAL;
            BURST_WAVE_INIT = BURST_WAVE_INIT_NORMAL;

            WAVESPAWN_INIT = WAVESPAWN_INIT_NORMAL;

            DROP_RATE = DROP_RATE_NORMAL;

            CAN_SHOOT_RATE = CAN_SHOOT_RATE_NORMAL;
            IS_SPEEDY_RATE = IS_SPEEDY_RATE_NORMAL;
        }

        private void EasyMode()
        {
            TIMER1_DECREASINGMODE = TIMER1_DECREASINGMODE_EASY;

            TIMER1_CONSTANTMODE = TIMER1_CONSTANTMODE_EASY;
            BURSTMODE_SPAWN_INTERVAL = BURSTMODE_SPAWN_INTERVAL_EASY;
            BURSTMODE_WAVE_INTERVAL = BURSTMODE_WAVE_INTERVAL_EASY;
            BURST_WAVE_INIT = BURST_WAVE_INIT_EASY;

            WAVESPAWN_INIT = WAVESPAWN_INIT_NORMAL;

            DROP_RATE = DROP_RATE_EASY;

            CAN_SHOOT_RATE = CAN_SHOOT_RATE_EASY;
            IS_SPEEDY_RATE = IS_SPEEDY_RATE_EASY;
        }
    }
}
