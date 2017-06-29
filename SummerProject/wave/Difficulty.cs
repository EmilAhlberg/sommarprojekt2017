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
        public static float T1_DECREASING;
        public static float T1_CONSTANT;
        public static float BURST_SPREE;
        public static float BURST_INTERVAL;      
        //SpawnPoint + Timer:
        public static int BURST_SIZE;
        //SpawnPoint
        public static int WAVE_SIZE;
        //Drops
        public static double DROP_RATE;
        //Enemies
        public static float ENEMY_FIRE_RISK;
        public static float CAN_SHOOT_RISK;
        public static float IS_SPEEDY_RISK;


        /*
         * Difficulty values: 
         */
        // <tweakZone>
        //Timer:      
        private const float T1_DECREASINGE_EASY = 1f;
        private const float T1_DECREASING_NORMAL = 0.7f;
        private const float T1_DECREASING_HARD = 0.4f;

        private const float T1_CONSTANT_EASY = 5f;
        private const float T1_CONSTANT_NORMAL = 4f;
        private const float T1_CONSTANT_HARD = 3f;

        private const float BURST_SPREE_EASY = 0.25f;
        private const float BURST_SPREE_NORMAL = 0.2f;
        private const float BURST_SPREE_HARD = 0.15f;

        private const float BURST_INTERVAL_EASY = 4.5f;
        private const float BURST_INTERVAL_NORMAL = 4.0f;
        private const float BURST_INTERVAL_HARD = 3f;

        //SpawnPoint + Timer:
        private const int BURST_SIZE_EASY = 2;
        private const int BURST_SIZE_NORMAL = 3;
        private const int BURST_SIZE_HARD = 5;

        //SpawnPoint:
        private const int WAVE_SIZE_EASY = 1; // - waveMode: spawnSize = WAVE_SIZE + current Level 
        private const int WAVE_SIZE_NORMAL = 2;
        private const int WAVE_SIZE_HARD = 3;
        
        //Drops:        
        private const float DROP_RATE_EASY = 0.4f; // - i.e. 40% chance of drop on death
        private const float DROP_RATE_NORMAL = 0.25f;
        private const float DROP_RATE_HARD = 0.1f;

        //Enemies
        private const float ENEMY_FIRE_RISK_EASY = 0.01f; // 1% risk of enemy shooting every game cycle of enemy shooting
        private const float ENEMY_FIRE_RISK_NORMAL = 0.02f;
        private const float ENEMY_FIRE_RISK_HARD = 0.03f;

        private const float CAN_SHOOT_RISK_EASY = 0.2f; // - i.e. 20% 'chance' of shooting enemy spawning
        private const float CAN_SHOOT_RISK_NORMAL = 0.25f;
        private const float CAN_SHOOT_RISK_HARD = 0.3f;

        private const float IS_SPEEDY_RISK_EASY = 0.3f; // - i.e. 10% 'chance' of shuupedo enemy spawning (real risk is canshootRisk - IsSpeedyRisk)
        private const float IS_SPEEDY_RISK_NORMAL = 0.4f;
        private const float IS_SPEEDY_RISK_HARD = 0.45f;

        

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
            T1_DECREASING = T1_DECREASING_HARD;
            T1_CONSTANT = T1_CONSTANT_HARD;
            BURST_SPREE = BURST_SPREE_HARD;
            BURST_INTERVAL = BURST_INTERVAL_HARD;

            BURST_SIZE = BURST_SIZE_HARD;

            WAVE_SIZE = WAVE_SIZE_HARD;

            DROP_RATE = DROP_RATE_HARD;

            ENEMY_FIRE_RISK = ENEMY_FIRE_RISK_HARD;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_HARD;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_HARD;
        }

        private void Normal()
        {
            T1_DECREASING = T1_DECREASING_NORMAL;

            T1_CONSTANT = T1_CONSTANT_NORMAL;
            BURST_SPREE = BURST_SPREE_NORMAL;
            BURST_INTERVAL = BURST_INTERVAL_NORMAL;
            BURST_SIZE = BURST_SIZE_NORMAL;

            WAVE_SIZE = WAVE_SIZE_NORMAL;

            DROP_RATE = DROP_RATE_NORMAL;

            ENEMY_FIRE_RISK = ENEMY_FIRE_RISK_NORMAL;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_NORMAL;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_NORMAL;
        }

        private void EasyMode()
        {
            T1_DECREASING = T1_DECREASINGE_EASY;

            T1_CONSTANT = T1_CONSTANT_EASY;
            BURST_SPREE = BURST_SPREE_EASY;
            BURST_INTERVAL = BURST_INTERVAL_EASY;
            BURST_SIZE = BURST_SIZE_EASY;

            WAVE_SIZE = WAVE_SIZE_NORMAL;

            DROP_RATE = DROP_RATE_EASY;

            ENEMY_FIRE_RISK = ENEMY_FIRE_RISK_EASY;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_EASY;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_EASY;
        }
    }
}
