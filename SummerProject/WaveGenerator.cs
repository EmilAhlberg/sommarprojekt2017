using Microsoft.Xna.Framework.Graphics;
using SummerProject.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SummerProject.collidables;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public class WaveGenerator
    {      
        private List<Sprite> enemySprites;
        private Enemies enemies;
        private Player player;
      
        private Random rand;
        private Vector2[] spawnPoints;
        private bool isActive;
        SpawnCalculator spawnCalc;

        public WaveGenerator(List<Sprite> enemySprites, Player player, int windowWidth, int windowHeight)
        {
            this.enemySprites = enemySprites;
            this.player = player;
            spawnCalc = new SpawnCalculator();    

           
            rand = new Random();

            spawnPoints = new Vector2[8];
            spawnPoints[0] = new Vector2(-50, -50);     // Top left
            spawnPoints[1] = new Vector2(windowWidth + 50, -50);  // top right
            spawnPoints[2] = new Vector2(-50, windowHeight + 50); // bottom left
            spawnPoints[3] = new Vector2(windowWidth + 50, windowHeight + 50); // bottom right
            spawnPoints[4] = new Vector2(windowWidth + 50, windowHeight / 2); // right  (bugged)
            spawnPoints[5] = new Vector2(-50, windowHeight / 2); // left      (bugged)
            spawnPoints[6] = new Vector2(windowWidth / 2, windowHeight + 50); // bottom
            spawnPoints[7] = new Vector2(windowWidth / 2, -50); // top



            enemies = new Enemies(enemySprites, player, 30);
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)
            {
                UpdateWave(gameTime);                
            }
            enemies.Update(gameTime);
        }

        private void UpdateWave(GameTime gameTime)
        {
            spawnCalc.Update(gameTime);
            if(spawnCalc.SpawnIsReady)
            {
                enemies.Spawn(spawnPoints[(int)(rand.NextDouble() * 8)]); //!
                spawnCalc.JustSpawned();
            }     
               
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            enemies.Draw(spriteBatch, gameTime);
        }

        public void Reset()
        {
            enemies.Reset();
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

        public List<AIEntity> CollidableList()
        {
            return enemies.EntityList;
        }
    }

    



}
