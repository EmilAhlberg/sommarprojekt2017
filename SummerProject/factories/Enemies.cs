using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class Enemies : Entities
    {
        private Player player;
        private Vector2[] spawnPoints;
        private Random rand;
        private bool isActive;
        private float minSpawnDelay = 0.4f;
        private float defaultSpawnDelay;
        private Timer difficultyTimer;
        public Enemies(List<Sprite> sprites, Player player, int NbrOfEnemies, float eventTime) : base(sprites, NbrOfEnemies, eventTime)
        {
            defaultSpawnDelay = eventTime;
            difficultyTimer = new Timer(eventTime);
            this.player = player;
            InitializeEntities(0);
            rand = new Random();
            spawnPoints = new Vector2[8];
            spawnPoints[0] = new Vector2(-50, -50);
            spawnPoints[1] = new Vector2(1970, -50);
            spawnPoints[2] = new Vector2(-50, 1130);
            spawnPoints[3] = new Vector2(1970, 1130);
            spawnPoints[4] = new Vector2(1970, 540);
            spawnPoints[5] = new Vector2(-50, 540);
            spawnPoints[6] = new Vector2(960, 1130);
            spawnPoints[7] = new Vector2(960, -50);
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();          
            if(isActive)
            {
                UpdateDifficulty(gameTime);                           
                Spawn(spawnPoints[(int)(rand.NextDouble() * 8)], player.Position); //!
                UpdateEntities(gameTime);
            }
        }

        private void CheckActive()
        {
            if (!isActive)
            {
                if (!player.IsDead)
                    isActive = true;
            } else if (player.IsDead)
            {
                isActive = false;
                Reset();
            }
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

        public void Reset()
        {
            eventTimer.maxTime = defaultSpawnDelay;
            foreach (Enemy e in EntityList)
                if(e.IsActive)
                    e.Death();
        }
        public void Spawn(Vector2 source, Vector2 target)
        {
            if (eventTimer.IsFinished)
                ActivateEntities(source, target);
        }
        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEntity(Sprites[index], player);
        }
    }
}
