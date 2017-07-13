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
        private const int dropCap = 10;
        public Drops( int windowWidth, int windowHeight) : base()
        {
            width = windowWidth - 100;
            height = windowHeight - 100;
            //spawnTimer = new Timer(spawnTime);
            InitializeEntities((int)IDs.HEALTHDROP, dropCap);
            InitializeEntities((int)IDs.EXPLOSIONDROP, dropCap);
            InitializeEntities((int)IDs.ENERGYDROP, dropCap);
            InitializeEntities((int)IDs.HEALTHDROP_TIER2, dropCap);
            InitializeEntities((int)IDs.MONEYDROP, dropCap);
        }

        //public void Spawn()
        //{
        //    SpawnAt(new Vector2((float)SRandom.NextDouble() * width + 50, (float)SRandom.NextDouble() * height + 50));
        //}

        //public void SpawnAt(Vector2 source)
        //{
        //    if (spawnTimer.IsFinished)
        //    {
        //        if (ActivateEntities(source, source, SRandom.Next((int)IDs.HEALTHDROP,(int)IDs.ENERGYDROP + 1)))
        //            spawnTimer.Reset();
        //    }
        //}

        public void SpawnAt(Vector2[] source)
        {
            foreach (Vector2 v in source)
            {
                int dropType = SRandom.Next((int)IDs.HEALTHDROP, (int)IDs.ENERGYDROP + 1);
                if (dropType == (int)IDs.HEALTHDROP_TIER2 && SRandom.NextFloat() > healthSpawnChance_tier2)
                    dropType = (int)IDs.HEALTHDROP;
                ActivateEntities(v, v, dropType);                    
            }
        }

        public void SpawnMoneyAt(Vector2[] source)
        {
            foreach (Vector2 v in source)
            {              
                ActivateEntities(v, v, (int)IDs.MONEYDROP);
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

        public override IActivatable CreateEntity(int type)
        {
            return EntityFactory.CreateEntity(SpriteHandler.GetSprite(type), type);
        }
    }
}