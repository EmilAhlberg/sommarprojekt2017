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
        private const float TIMER1_INCREASINGMODE = 3f;
        private const float TIMER2_INCREASINGMODE = 3f;

        private const float TIMER1_WAVESPAWNMODE = 2.5f;
        private const float TIMER2_WAVESPAWNMODE = 0.06f;


        private float increasingModeTimeCap = 0.4f;


        private Timer timer2;
        private Timer timer1;
        private int mode;

        public SpawnTimer(int mode)
        {
            this.mode = mode;
            timer1 = new Timer(TIMER1_INCREASINGMODE);
            timer2 = new Timer(TIMER2_INCREASINGMODE);
        }

        public void ChangeMode(int modeType)
        {
            switch (modeType)
            {
                case WaveGenerator.INCREASING_PRESSURE:                  
                    timer1.maxTime = TIMER1_INCREASINGMODE; 
                    timer2.maxTime = TIMER2_INCREASINGMODE;                    
                    break;
                case WaveGenerator.WAVESPAWN_MODE:
                    timer1.maxTime = TIMER1_WAVESPAWNMODE;
                    //timer2.maxTime = TIMER2_WAVESPAWNMODE;                   
                    break;
            }
            timer1.Reset();
            timer2.Reset();
            mode = modeType;
        }
       

        public bool Update(GameTime gameTime)
        {            
            switch (mode)
            {
                case WaveGenerator.INCREASING_PRESSURE:
                    IncreasingPressureMode(gameTime);                   
                    return timer1.IsFinished;                    
                case WaveGenerator.WAVESPAWN_MODE:
                    WaveSpawnMode(gameTime);
                    return timer1.IsFinished;                    
            }

            return false;
        }

        public void JustSpawned()
        {
            switch (mode)
            {
                case WaveGenerator.INCREASING_PRESSURE:
                    timer1.Reset();
                    break;
            }
        }

        //
        // MODES:
        //

        private void IncreasingPressureMode(GameTime gameTime)
        {
            timer2.CountDown(gameTime);
            if (timer1.maxTime > increasingModeTimeCap && timer2.IsFinished)
            {
                timer2.Reset();
                if (timer1.maxTime > 1.5f)
                    timer1.maxTime *= 0.75f;
                else
                    timer1.maxTime *= 0.97f;
            }

            timer1.CountDown(gameTime);
        }

        private void WaveSpawnMode(GameTime gameTime)
        {
            if (timer1.IsFinished)
                timer1.Reset();
                       
               timer1.CountDown(gameTime);            
            
            //if (timer1.IsFinished)
            //    timer2.CountDown(gameTime);                   
        }

      
    }
}
