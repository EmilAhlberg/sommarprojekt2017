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
using SummerProject.wave;

namespace SummerProject
{
    public class WaveGenerator
    {    
        private Enemies enemies;
        private Player player;
        private GameMode gameMode;
        private SpawnPointGenerator spawnPointGen;
        private SpawnTimer spawnTimer;

        private bool isActive;
  

        //enemies as param insted of sprites?
        public WaveGenerator(List<Sprite> enemySprites, Player player, int windowWidth, int windowHeight)
        {
            this.player = player;
            gameMode = new GameMode();
            spawnPointGen = new SpawnPointGenerator(gameMode, windowWidth, windowHeight);
            spawnTimer = new SpawnTimer(gameMode);  
            enemies = new Enemies(enemySprites, player, 30); //! nbrOfEnemies
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)            
                UpdateSpawnHandlers(gameTime);                
            
            enemies.Update(gameTime);
            UpdateMode(); 
        }

       

        private void UpdateSpawnHandlers(GameTime gameTime)
        {            
            spawnPointGen.Update(gameTime);            
            if (spawnTimer.Update(gameTime))
                SpawnWave();                
            }

        private void SpawnWave()
        {
            Vector2[] spawnPoints = spawnPointGen.GetSpawnPoints();
            foreach (Vector2 v in spawnPoints)
            {
                enemies.Spawn(v);
                spawnTimer.JustSpawned();
            }            
        }    

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            enemies.Draw(spriteBatch, gameTime);
        }

        public void Reset()
        {
            enemies.Reset();
            gameMode.Reset();
        }

        private void CheckActive()
        {
            if (!isActive)
            {
                if (!player.IsActive)
                    isActive = true;
            }
            else if (player.IsActive)
            {
                isActive = false;
                Reset();
            }
        }        


        public List<AIEntity> CollidableList()
        {
            return enemies.EntityList;
        }

        private void UpdateMode()
        {
            if (InputHandler.isJustPressed(Keys.F1))
            {
                gameMode.TimeMode = GameMode.DECREASING_TIME;
                gameMode.SpawnMode = GameMode.RANDOM_SINGLESPAWN;
            }

            if (InputHandler.isJustPressed(Keys.F2))
            {
                gameMode.TimeMode = GameMode.RANDOM_WAVESPAWN;
                gameMode.SpawnMode = GameMode.RANDOM_WAVESPAWN;
            }

            if (InputHandler.isJustPressed(Keys.F3))
            {
                gameMode.TimeMode = GameMode.DEBUG_MODE;
            }
        }
    }
}
