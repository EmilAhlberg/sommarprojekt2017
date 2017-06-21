using Microsoft.Xna.Framework.Graphics;
using SummerProject.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SummerProject.collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SummerProject
{
    public class WaveGenerator
    {
        public const int DEBUG_MODE = 0;
        public const int INCREASING_PRESSURE = 1;
        public const int WAVESPAWN_MODE = 2;

        private int mode;
        

        private List<Sprite> enemySprites;
        private Enemies enemies;
        private Player player;      
       
        private bool isActive;
        private SpawnCalculator spawnCalc;

        public WaveGenerator(List<Sprite> enemySprites, Player player, int windowWidth, int windowHeight)
        {
            this.enemySprites = enemySprites;
            this.player = player;
            mode = INCREASING_PRESSURE;       
            spawnCalc = new SpawnCalculator(mode, windowWidth, windowHeight);    
            enemies = new Enemies(enemySprites, player, 30); //!
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)            
                UpdateWave(gameTime);                
            
            enemies.Update(gameTime);
            UpdateMode();                       
        }

        private void UpdateMode()
        {
            if (InputHandler.isJustPressed(Keys.F1))
            {
                mode = INCREASING_PRESSURE;
                spawnCalc.SetGameMode(mode);
            }
              
            if (InputHandler.isJustPressed(Keys.F2))
            {
                mode = WAVESPAWN_MODE;
                spawnCalc.SetGameMode(mode);
            }


            if (InputHandler.isJustPressed(Keys.F3))
            {
                mode = DEBUG_MODE;
                spawnCalc.SetGameMode(mode);
            }
        }

        private void UpdateWave(GameTime gameTime)
        {
            spawnCalc.Update(gameTime);            
            if (spawnCalc.SpawnIsReady)
                SpawnWave();                
            }

        private void SpawnWave()
        {
            Vector2[] spawnPoints = spawnCalc.GetSpawnPoints();
            foreach (Vector2 v in spawnPoints)
            {
                enemies.Spawn(v);
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
            mode = INCREASING_PRESSURE; //DEFAULT
            spawnCalc.SetGameMode(mode);
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
