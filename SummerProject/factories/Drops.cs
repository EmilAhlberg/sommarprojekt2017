using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SummerProject.factories
{
    class Drops : Entities
    {
        private Timer spawnTimer;
        private const float spawnTime = 5f;
        private int width;
        private int height;
        private Random rand;
        public bool IsActive { get; set; }
        public Drops(List<Sprite> sprites, int entityCap, int windowWidth, int windowHeight) : base(sprites, entityCap)
        {
            IsActive = true;
            width = windowWidth - 100;
            height = windowHeight - 100;
            spawnTimer = new Timer(spawnTime);
            rand = new Random(42);
            InitializeEntities(1);              // change here to create different drops initially
        }

        public void Spawn()
        {
            SpawnAt(new Vector2((float)rand.NextDouble() * width + 50, (float)rand.NextDouble() * height + 50));
        }

        public void SpawnAt(Vector2 source)
        {
            if (spawnTimer.IsFinished)
            {
                if (ActivateEntities(source, source))
                    spawnTimer.Reset();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                UpdateEntities(gameTime);
                spawnTimer.CountDown(gameTime);
            }
        }

        public override void Reset()
        {
            spawnTimer.Reset();
            ResetEntities();
        }

        protected override AIEntity CreateEntity(int type)
        {
            return EntityFactory.CreateDrop(Sprites[1], type);
        }
    }
}