﻿using Microsoft.Xna.Framework;
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

        private const float TIMER1_BURSTMODE = 8f;
        private const float TIMER2_BURSTMODE = 5.0f;
        private const float TIMER3_BURSTMODE = 0.2f;

        private const float TIMER1_DEBUGMODE = 30f;


        private float decreasingModeTimeCap = 0.4f;
        //!

        private Timer timer3;
        private Timer timer2;
        private Timer timer1;
        private GameMode gameMode;
        private int oldMode;

        public SpawnTimer(GameMode gameMode)
        {
            oldMode = gameMode.TimeMode;
            this.gameMode = gameMode;
            timer1 = new Timer(TIMER1_BURSTMODE); //! changing time settings when game inits is bugged
            timer2 = new Timer(TIMER2_BURSTMODE);
            timer3 = new Timer(TIMER3_BURSTMODE);
        }

        private void UpdateMode()
        {
            if (oldMode != gameMode.TimeMode)
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
                    case GameMode.BURST_TIME:
                        timer1.maxTime = TIMER1_BURSTMODE;
                        timer2.maxTime = TIMER2_BURSTMODE;
                        timer3.maxTime = TIMER3_BURSTMODE;
                        break;
                    case GameMode.DEBUG_TIME:
                        timer1.maxTime = TIMER1_DEBUGMODE;
                        break;
                }
                timer1.Reset();
                timer2.Reset();
                oldMode = gameMode.TimeMode;
            } 
        }
       

        public bool Update(GameTime gameTime)
        {
            UpdateMode();
            ChangeLevel();
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

        private void ChangeLevel()
        {
            if (gameMode.ChangeLevel)
            {
                timer1.Reset();
                timer2.Reset();
            }
        }

        //refactor?
        public void JustSpawned()
        {
            switch (gameMode.TimeMode)
            {
                case GameMode.DECREASING_TIME:
                    timer1.Reset();
                    break;
            }
        }


       
        /*
         *  MODES:
         *          DecreasingTimeMode: Spawn frequency is continually increased.
         *          ConstantTimeMode: Spawn frequency is unchanged.         
         */


        private void DecreasingTimeMode(GameTime gameTime)
        {
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