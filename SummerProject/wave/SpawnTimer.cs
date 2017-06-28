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
        //!
        private const float TIMER1_DECREASINGMODE = 3f;
        private const float TIMER2_DECREASINGMODE = 3f;

        private const float TIMER1_CONSTANTMODE = 4f;
      
        private const float BURSTMODE_SPAWN_INTERVAL = 0.2f;
        private const float BURSTMODE_WAVE_INTERVAL = 4.0f;
        //private const float TIMER1_DEBUGMODE = 30f;
        private float decreasingModeTimeCap = 0.4f;
        //!

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
                case GameMode.DEBUG_TIME:
                    ConstantTimeMode(gameTime);
                    return timer1.IsFinished;
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
                        timer1.maxTime = TIMER1_DECREASINGMODE;
                        timer2.maxTime = TIMER2_DECREASINGMODE;
                        break;
                    case GameMode.CONSTANT_TIME:
                        timer1.maxTime = TIMER1_CONSTANTMODE;
                        break;
                    case GameMode.BURST_TIME: //times must be modified carefully, has to fit exactly spawnSize as the # of timer pulses
                        float upperBound = BURSTMODE_SPAWN_INTERVAL * gameMode.Level + (float)BURSTMODE_WAVE_INTERVAL; //!
                        timer1.maxTime = upperBound;
                        timer2.maxTime = upperBound - (BURSTMODE_SPAWN_INTERVAL * (float)(gameMode.Level + GameMode.BURST_WAVE_INIT + 1));
                        timer3.maxTime = BURSTMODE_SPAWN_INTERVAL;
                        break;
                }
                timer1.Reset();
                timer2.Reset();
                timer3.Reset();              
            }
        }

        /*
         *  MODES:                                                                                                                      Level dependant progression:
         *          DecreasingTimeMode: Spawn frequency is continually increased, until it hits it's timeCap.                                       No
         *          ConstantTimeMode: Spawn frequency is unchanged.                                                                                 Yes
         *          BurstTimeMode: Bursts of fixed short interval spawns occurs between longer, increasing 'pause' intervals.                       Yes
         */


        private void DecreasingTimeMode(GameTime gameTime)
        {
            if (timer1.IsFinished)
                timer1.Reset();
            timer2.CountDown(gameTime);
            if (timer1.maxTime > decreasingModeTimeCap && timer2.IsFinished)
            {
                timer2.Reset();
                if (timer1.maxTime > 1.5f)
                    timer1.maxTime *= 0.75f;
                else
                    timer1.maxTime *= 0.97f;
            }

            timer1.CountDown(gameTime);
        }

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
