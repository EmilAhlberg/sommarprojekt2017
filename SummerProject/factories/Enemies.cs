using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class Enemies : Entities
    {             
        private Player player;
        private Vector2[] spawnPoints;
        private Random rand;
        private float minSpawnDelay = 0.3f;
        public Enemies (List<Sprite> sprites, Player player, int NbrOfEnemies, float eventTime) : base(sprites, NbrOfEnemies, eventTime)
        {
            this.eventTime = eventTime;
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
            //Vector2 spawnPoint = new Vector2(250, 250);
            Spawn(spawnPoints[(int) (rand.NextDouble() * 8)], player.Position); //!
            UpdateEntities(gameTime);
        }

        public void Spawn(Vector2 source, Vector2 target)
        {
            if (EventTimer < 0)
            {
                ActivateEntities(source, target);
                if (eventTime > minSpawnDelay)
                    eventTime *= 0.95f;
            }
        }


        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEntity(sprites[index], player);
        }
    }
}
