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
        public Drops Drops { get; private set; }

        private bool isActive;
  

        //enemies as param insted of sprites?
        public WaveGenerator( Player player, int windowWidth, int windowHeight, SpriteFont font, Drops drops)
        {
            this.player = player;
            Drops = drops;
            gameMode = new GameMode(font, windowWidth, windowHeight);
            spawnPointGen = new SpawnPointGenerator(gameMode, windowWidth, windowHeight);
            spawnTimer = new SpawnTimer(gameMode);  
            enemies = new Enemies(player, 30); //! nbrOfEnemies
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)            
                UpdateSpawnHandlers(gameTime);            
            
            enemies.Update(gameTime);
            Drops.Update(gameTime);
            gameMode.Update(gameTime);
            UpdateMode(); 
        }       

        private void UpdateSpawnHandlers(GameTime gameTime)
        {
            Drops.Spawn();
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
            Drops.Draw(spriteBatch, gameTime);
            if(isActive)
                gameMode.Draw(spriteBatch, gameTime);
        }

        public void Reset()
        {
            enemies.Reset();
            gameMode.Reset();
            Drops.Reset();
            spawnTimer.ChangeMode();
            spawnPointGen.ChangeMode();
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
            return enemies.GetValues();
        }

        private void UpdateMode()
        {
            if (InputHandler.isJustPressed(Keys.F1))
            {
                gameMode.TimeMode = GameMode.DECREASING_TIME;
                gameMode.SpawnMode = GameMode.RANDOM_SINGLESPAWN;
                spawnTimer.ChangeMode();
                spawnPointGen.ChangeMode();
            }

            if (InputHandler.isJustPressed(Keys.F2))
            {
                gameMode.TimeMode = GameMode.RANDOM_WAVESPAWN;
                gameMode.SpawnMode = GameMode.RANDOM_WAVESPAWN;
                spawnTimer.ChangeMode();
                spawnPointGen.ChangeMode();
            }

            if (InputHandler.isJustPressed(Keys.F3))
            {
                gameMode.TimeMode = GameMode.DEBUG_MODE;
                spawnTimer.ChangeMode();
                spawnPointGen.ChangeMode();
            }
        }
    }
}
