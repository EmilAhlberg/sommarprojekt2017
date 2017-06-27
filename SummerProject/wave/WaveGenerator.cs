﻿using Microsoft.Xna.Framework.Graphics;
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
        private DropSpawnPoints dropPoints;

        private bool isActive;

        //enemies as param insted of sprites?
        public WaveGenerator(Player player, SpriteFont font, Drops drops)
        {
            this.player = player;
            Drops = drops;
            gameMode = new GameMode(font);
            spawnPointGen = new SpawnPointGenerator(gameMode);
            spawnTimer = new SpawnTimer(gameMode);  
            enemies = new Enemies(player, 30); //! nbrOfEnemies
            dropPoints = new DropSpawnPoints();
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive)            
                UpdateSpawnHandlers(gameTime);
            
            enemies.Update(gameTime);
            Drops.Update(gameTime);
            gameMode.Update(gameTime);          
        }       

        private void UpdateSpawnHandlers(GameTime gameTime)
        {
            Drops.SpawnAt(dropPoints.SpawnPositions());           

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

        public void Reset(bool fullReset)
        {
            enemies.Reset();
            gameMode.Reset(fullReset);
            Drops.Reset();
        }

        private void CheckActive()
        {
            if (!isActive)
            {
                if (player.IsActive)
                    isActive = true;
            }
            else if (!player.IsActive)
            {
                isActive = false;
            }
        }        

        public List<AIEntity> CollidableList()
        {
            return enemies.GetValues();
        }       
    }
}
