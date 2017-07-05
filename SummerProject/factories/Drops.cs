using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SummerProject.factories
{
    public class Drops : Entities
    {
        //private Timer spawnTimer;
        //private const float spawnTime = 1f;
        private int width;
        private int height;
        private const float healthSpawnChance_tier2 = 0.02f;
        public Drops(int entityCap, int windowWidth, int windowHeight) : base(entityCap)
        {
            width = windowWidth - 100;
            height = windowHeight - 100;
            //spawnTimer = new Timer(spawnTime);
            InitializeEntities(EntityTypes.HEALTHDROP);
            InitializeEntities(EntityTypes.EXPLOSIONDROP);
            InitializeEntities(EntityTypes.ENERGYDROP);
            InitializeEntities(EntityTypes.HEALTHDROP_TIER2);
        }

        //public void Spawn()
        //{
        //    SpawnAt(new Vector2((float)SRandom.NextDouble() * width + 50, (float)SRandom.NextDouble() * height + 50));
        //}

        //public void SpawnAt(Vector2 source)
        //{
        //    if (spawnTimer.IsFinished)
        //    {
        //        if (ActivateEntities(source, source, SRandom.Next(EntityTypes.HEALTHDROP,EntityTypes.ENERGYDROP + 1)))
        //            spawnTimer.Reset();
        //    }
        //}

        public void SpawnAt(Vector2[] source)
        {
            foreach (Vector2 v in source)
            {
                int dropType = SRandom.Next(EntityTypes.HEALTHDROP, EntityTypes.ENERGYDROP + 1);
                if (dropType == EntityTypes.HEALTHDROP && SRandom.NextFloat() < healthSpawnChance_tier2)
                    dropType = EntityTypes.HEALTHDROP_TIER2;
                ActivateEntities(v, v, dropType);                    
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
            //spawnTimer.CountDown(gameTime);         
        }

        public override void Reset()
        {
            //spawnTimer.Reset();
            ResetEntities();
        }

        public override ActivatableEntity CreateEntity(int type) 
        {
            return EntityFactory.CreateEntity(Sprites[EntityTypes.SPRITE[type]], type);
        }
    }
}