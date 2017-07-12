using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class Timer
    {
        public float maxTime { get; set; }
        public float currentTime { get; private set; }
        public bool IsFinished { get; private set; }

        public Timer(float timeInSeconds)
        {
            maxTime = timeInSeconds;
            currentTime = maxTime;
            IsFinished = false;
        }

        public void Finish()
        {
            currentTime = 0;
        }

        public void Reset()
        {
            currentTime = maxTime;
            IsFinished = false;
        }

        public void CountDown(GameTime gameTime)
        {
            if (currentTime <= 0)
            {
                IsFinished = true;
                currentTime = 0;
            }
            else
                currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void AddTime(float timeInSeconds)
        {
            currentTime += timeInSeconds;
            IsFinished = false;
        }
    }
}
