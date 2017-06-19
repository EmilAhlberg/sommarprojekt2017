using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;

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
        public Enemies(List<Sprite> sprites, Player player, int NbrOfEnemies, float eventTime, int windowWidth, int windowHeight) : base(sprites, NbrOfEnemies, eventTime)
        {
            defaultSpawnDelay = eventTime;
            difficultyTimer = new Timer(eventTime);
            this.player = player;
            InitializeEntities(0);
            rand = new Random();
            spawnPoints = new Vector2[8];
            spawnPoints[0] = new Vector2(-50, -50);     // Top left
            spawnPoints[1] = new Vector2(windowWidth + 50, -50);  // top right
            spawnPoints[2] = new Vector2(-50, windowHeight + 50); // bottom left
            spawnPoints[3] = new Vector2(windowWidth + 50, windowHeight + 50); // bottom right
            spawnPoints[4] = new Vector2(windowWidth + 50, windowHeight / 2 ); // right  (bugged)
            spawnPoints[5] = new Vector2(-50, windowHeight / 2 ); // left      (bugged)
            spawnPoints[6] = new Vector2(windowWidth / 2, windowHeight + 50); // bottom
            spawnPoints[7] = new Vector2(windowWidth / 2, -50); // top
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)
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
            }
            else if (player.IsDead)
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
                if (e.IsActive)
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
