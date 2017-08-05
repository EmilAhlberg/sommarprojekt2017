using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class SpawnTimer
    {
        private Timer timer3;
        private Timer timer2;
        private Timer timer1;
        private GameMode gameMode;

        public SpawnTimer(GameMode gameMode)
        {
            this.gameMode = gameMode;
            timer1 = new Timer(0);
            timer2 = new Timer(0);
            timer3 = new Timer(0);            
        }       

        public bool Update(GameTime gameTime)
        {
            UpdateMode();
            return HandleGameMode(gameTime);
        }

        private bool HandleGameMode(GameTime gameTime)
        {
            switch (gameMode.TimeMode)
            {
                case GameMode.DECREASING_TIME:
                    DecreasingTimeMode(gameTime);
                    return timer1.IsFinished;
                case GameMode.CONSTANT_TIME:
                    ConstantTimeMode(gameTime);
                    return timer1.IsFinished;
                case GameMode.BURST_TIME:
                    BurstTimeMode(gameTime);
                    return timer3.IsFinished;                
            }
            return false;
        }

        private void UpdateMode()
        {
            if (gameMode.IsChanged)
            {
                switch (gameMode.TimeMode)
                {
                    case GameMode.DECREASING_TIME:                     
                        timer1.maxTime = Difficulty.T1_DECREASING * (1 - (float)Math.Log10((float)GameMode.Level / 10.0));// 0.05f * gameMode.Level);
                        break;
                    case GameMode.CONSTANT_TIME:
                        timer1.maxTime = Difficulty.T1_CONSTANT;
                        break;
                    case GameMode.BURST_TIME: //times must be modified carefully, has to fit exactly spawnSize as the # of timer pulses
                        float upperBound = Difficulty.BURST_SPREE * GameMode.Level /2.0f + Difficulty.BURST_INTERVAL; //!
                        timer1.maxTime = upperBound;
                        timer2.maxTime = upperBound - (Difficulty.BURST_SPREE * (float)(GameMode.Level + Difficulty.BURST_SIZE + 1));
                        timer3.maxTime = Difficulty.BURST_SPREE;
                        break;
                }
                timer1.Reset();
                timer2.Reset();
                timer3.Reset();              
            }
        }

        /*
         *  MODES:                                                                                                                      
         *          DecreasingTimeMode: Spawn frequency is continually increased.                                                                  
         *          ConstantTimeMode: Spawn frequency is unchanged.                                                                                
         *          BurstTimeMode: Bursts of fixed short interval spawns occurs between longer, decreasing(?) 'pause' intervals.                   
         */

        //identical methods
        private void DecreasingTimeMode(GameTime gameTime)
        {
            if (timer1.IsFinished)
                timer1.Reset();
            timer1.CountDown(gameTime);
        }
        //identical methods
        private void ConstantTimeMode(GameTime gameTime)
        {
            if (timer1.IsFinished)
                timer1.Reset();
                       
               timer1.CountDown(gameTime);           
        }

        private void BurstTimeMode(GameTime gameTime)
        {
            if (timer2.IsFinished)
            {
                if (timer3.IsFinished)
                    timer3.Reset();
                timer3.CountDown(gameTime);          
            }
                if (timer1.IsFinished)
                {
                    timer1.Reset();
                    timer2.Reset();
                    timer3.Reset();
                }
            timer1.CountDown(gameTime);
            timer2.CountDown(gameTime);
        }
    }
}
