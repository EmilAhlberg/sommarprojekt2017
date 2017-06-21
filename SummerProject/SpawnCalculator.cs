using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class SpawnCalculator
    {
        private float minSpawnDelay = 0.4f;

        private Timer difficultyTimer;
        private Timer eventTimer;


        public bool SpawnIsReady { get; internal set; }

        public SpawnCalculator()
        {
            eventTimer = new Timer(3); //!!!!!
            difficultyTimer = new Timer(3); //!!!!!!!
        }

        private void UpdateDifficulty(GameTime gameTime)
        {
            difficultyTimer.CountDown(gameTime);
            if (eventTimer.maxTime > minSpawnDelay && difficultyTimer.IsFinished)
            {
                difficultyTimer.Reset();
                if (eventTimer.maxTime > 1.5f)
                    eventTimer.maxTime *= 0.75f;
                else
                    eventTimer.maxTime *= 0.97f;
            }
        }

        public void JustSpawned()
        {
            SpawnIsReady = false;
            eventTimer.Reset();
        }

        internal void Update(GameTime gameTime)
        {
            UpdateDifficulty(gameTime);
            eventTimer.CountDown(gameTime);
            SpawnIsReady = eventTimer.IsFinished;
        }
    }
}
