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
        public Drops(List<Sprite> sprites, int entityCap, int windowWidth, int windowHeight) : base(sprites, entityCap)
        {
            width = windowWidth - 100;
            height = windowHeight - 100;
            spawnTimer = new Timer(spawnTime);
            rand = new Random(42);
            InitializeEntities(0);
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
            UpdateEntities(gameTime);
            spawnTimer.CountDown(gameTime);
        }

        public override void Reset()
        {
            spawnTimer.Reset();
            ResetEntities();
        }

        protected override AIEntity CreateEntity(int type)
        {
            return EntityFactory.CreateDrop(Sprites[0], type);
        }
    }
}
