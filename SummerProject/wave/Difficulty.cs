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
        public static float ENEMY_FIRE_RATE;
        public static float CAN_SHOOT_RISK;
        public static float IS_SPEEDY_RISK;
        public static float IS_ASTEROID_RISK;
        public static float ENEMY_BULLETDAMAGEFACTOR;
        public static float ENEMY_FRICTIONFACTOR;


        /*
         * Difficulty values: 
         */
        // <tweakZone>
        //Timer:      
        private const float T1_DECREASINGE_EASY = 1f; // decreasing time mode spawn time
        private const float T1_DECREASING_NORMAL = 0.7f;
        private const float T1_DECREASING_HARD = 0.4f;

        private const float T1_CONSTANT_EASY = 5f; //constant spawn time
        private const float T1_CONSTANT_NORMAL = 4f;
        private const float T1_CONSTANT_HARD = 3f;

        private const float BURST_SPREE_EASY = 0.25f; // the smaller interval within a burst spree, in which enemies are spawned
        private const float BURST_SPREE_NORMAL = 0.2f;
        private const float BURST_SPREE_HARD = 0.15f;

        private const float BURST_INTERVAL_EASY = 4.5f; // burst spree is initiated at this longer interval
        private const float BURST_INTERVAL_NORMAL = 4.0f;
        private const float BURST_INTERVAL_HARD = 3f;

        //SpawnPoint + Timer:
        private const int BURST_SIZE_EASY = 0;  // - burstMode: spawnSize = BURST_SIZE + current Level (this affect timer as well, timing burst according to spawnSize)
        private const int BURST_SIZE_NORMAL = 3;
        private const int BURST_SIZE_HARD = 5;

        //SpawnPoint:
        private const int WAVE_SIZE_EASY = 0; // - waveMode: spawnSize = WAVE_SIZE + current Level 
        private const int WAVE_SIZE_NORMAL = 2;
        private const int WAVE_SIZE_HARD = 3;
        
        //Drops:        
        private const float DROP_RATE_EASY = 0.4f; // - i.e. 40% chance of drop on death
        private const float DROP_RATE_NORMAL = 0.25f;
        private const float DROP_RATE_HARD = 0.1f;

        //Enemies
        private const float ENEMY_FIRE_RATE_EASY = 2f; // enemy firerate
        private const float ENEMY_FIRE_RATE_NORMAL = 1.5f;
        private const float ENEMY_FIRE_RATE_HARD = 1f;

        private const float CAN_SHOOT_RISK_EASY = 0.2f; // - i.e. 20% 'chance' of shooting enemy spawning
        private const float CAN_SHOOT_RISK_NORMAL = 0.25f;
        private const float CAN_SHOOT_RISK_HARD = 0.3f;

        private const float IS_SPEEDY_RISK_EASY = 0.1f; // - i.e. 10% 'chance' of shuupedo enemy spawning (real risk is canshootRisk - IsSpeedyRisk)
        private const float IS_SPEEDY_RISK_NORMAL = 0.15f;
        private const float IS_SPEEDY_RISK_HARD = 0.2f;

        public static float ENEMY_BULLETDAMAGEFACTOR_EASY = 0.2f;
        public static float ENEMY_BULLETDAMAGEFACTOR_NORMAL = 0.4f;
        public static float ENEMY_BULLETDAMAGEFACTOR_HARD = 0.6f;

        public static float ENEMY_FRICTION_EASY = 3f;
        public static float ENEMY_FRICTION_NORMAL = 0.5f;
        public static float ENEMY_FRICTION_HARD = 0.00001f;



        //Asteroid
        private const float IS_ASTEROID_RISK_EASY = 0.4f; 
        private const float IS_ASTEROID_RISK_NORMAL = 0.3f;
        private const float IS_ASTEROID_RISK_HARD = 0.2f;
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

            ENEMY_FIRE_RATE = ENEMY_FIRE_RATE_HARD;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_HARD;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_HARD + CAN_SHOOT_RISK;
            IS_ASTEROID_RISK = IS_ASTEROID_RISK_HARD + IS_SPEEDY_RISK;

            ENEMY_BULLETDAMAGEFACTOR = ENEMY_BULLETDAMAGEFACTOR_HARD;
            ENEMY_FRICTIONFACTOR = ENEMY_FRICTION_HARD;
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

            ENEMY_FIRE_RATE = ENEMY_FIRE_RATE_NORMAL;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_NORMAL;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_NORMAL + CAN_SHOOT_RISK;
            IS_ASTEROID_RISK = IS_ASTEROID_RISK_NORMAL + IS_SPEEDY_RISK;

            ENEMY_BULLETDAMAGEFACTOR = ENEMY_BULLETDAMAGEFACTOR_NORMAL;
            ENEMY_FRICTIONFACTOR = ENEMY_FRICTION_NORMAL;
        }

        private void EasyMode()
        {
            T1_DECREASING = T1_DECREASINGE_EASY;

            T1_CONSTANT = T1_CONSTANT_EASY;
            BURST_SPREE = BURST_SPREE_EASY;
            BURST_INTERVAL = BURST_INTERVAL_EASY;
            BURST_SIZE = BURST_SIZE_EASY;

            WAVE_SIZE = WAVE_SIZE_EASY;

            DROP_RATE = DROP_RATE_EASY;

            ENEMY_FIRE_RATE = ENEMY_FIRE_RATE_EASY;
            CAN_SHOOT_RISK = CAN_SHOOT_RISK_EASY;
            IS_SPEEDY_RISK = IS_SPEEDY_RISK_EASY + CAN_SHOOT_RISK;
            IS_ASTEROID_RISK = IS_ASTEROID_RISK_EASY + IS_SPEEDY_RISK;

            ENEMY_BULLETDAMAGEFACTOR = ENEMY_BULLETDAMAGEFACTOR_EASY;
            ENEMY_FRICTIONFACTOR = ENEMY_FRICTION_EASY;
        }
    }
}
