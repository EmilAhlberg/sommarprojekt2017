using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SummerProject.factories
{
    public class Drops : Entities
    {
        private Timer spawnTimer;
        private const float spawnTime = 1f;
        private int width;
        private int height;
        private Random rand;
        public Drops(int entityCap, int windowWidth, int windowHeight) : base(entityCap)
        {
            width = windowWidth - 100;
            height = windowHeight - 100;
            spawnTimer = new Timer(spawnTime);
            rand = new Random(42);
            InitializeEntities(EntityTypes.HEALTHDROP);             
            InitializeEntities(EntityTypes.EXPLOSIONDROP);
            InitializeEntities(EntityTypes.ENERGYDROP);
        }

        public void Spawn()
        {
            SpawnAt(new Vector2((float)rand.NextDouble() * width + 50, (float)rand.NextDouble() * height + 50));
        }

        public void SpawnAt(Vector2 source)
        {
            if (spawnTimer.IsFinished)
            {
                if (ActivateEntities(source, source, rand.Next(EntityTypes.HEALTHDROP,EntityTypes.ENERGYDROP + 1)))
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
            return EntityFactory.CreateEntity(Sprites[EntityTypes.SPRITE[type]], type);
        }
    }
}