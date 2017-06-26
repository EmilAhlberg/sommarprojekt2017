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
        private const float TIMER1_DECREASINGMODE = 3f;
        private const float TIMER2_DECREASINGMODE = 3f;

        private const float TIMER1_CONSTANTMODE = 4f;      

        private const float TIMER1_DEBUGMODE = 30f;


        private float decreasingModeTimeCap = 0.4f;


        private Timer timer2;
        private Timer timer1;
        private GameMode gameMode;

        public SpawnTimer(GameMode gameMode)
        {
            this.gameMode = gameMode;
            timer1 = new Timer(TIMER1_DECREASINGMODE);
            timer2 = new Timer(TIMER2_DECREASINGMODE);
        }

        public void ChangeMode()
        {
            switch (gameMode.TimeMode)
            {
                case GameMode.DECREASING_TIME:                  
                    timer1.maxTime = TIMER1_DECREASINGMODE; 
                    timer2.maxTime = TIMER2_DECREASINGMODE;                    
                    break;
                case GameMode.RANDOM_WAVESPAWN:
                    timer1.maxTime = TIMER1_CONSTANTMODE;
                    break;                          
                case GameMode.DEBUG_MODE:
                    timer1.maxTime = TIMER1_DEBUGMODE;     
                    break;
            }
            timer1.Reset();
            timer2.Reset();            
        }
       

        public bool Update(GameTime gameTime)
        {            
            switch (gameMode.TimeMode)
            {
                case GameMode.DECREASING_TIME:
                    DecreasingTimeMode(gameTime);                   
                    return timer1.IsFinished;                    
                case GameMode.CONSTANT_TIME:
                    ConstantTimeMode(gameTime);
                    return timer1.IsFinished;
                case GameMode.DEBUG_MODE:
                    ConstantTimeMode(gameTime);
                    return timer1.IsFinished;                
            }
            return false;
        }

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
            
            //if (timer1.IsFinished)
            //    timer2.CountDown(gameTime);                   
        }
    }
}
