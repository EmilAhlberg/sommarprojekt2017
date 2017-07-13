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
using SummerProject.achievements;

namespace SummerProject
{
    public class GameController
    {    
        private Enemies enemies;
        private Player player;
        private GameMode gameMode;
        private SpawnPointGenerator spawnPointGen;
        private SpawnTimer spawnTimer;
        public Drops Drops { get; private set; }
        private DropSpawnPoints dropPoints;
        private Timer levelTimer;
        private int spawnsThisLevel;
        private bool finishedSpawning;
        private bool isActive;


        public GameController(Player player, Drops drops, GameMode gameMode)
        {
            this.player = player;
            Drops = drops;
            this.gameMode = gameMode;
            spawnPointGen = new SpawnPointGenerator(gameMode);
            spawnTimer = new SpawnTimer(gameMode);  
            enemies = new Enemies(player); 
            dropPoints = new DropSpawnPoints();
            levelTimer = new Timer(20);
        }

        public void Update(GameTime gameTime)
        {
            CheckActive();
            if (isActive && gameMode.BetweenLevelsTimer.IsFinished)
                UpdateSpawnHandlers(gameTime);

            enemies.Update(gameTime);
            Drops.Update(gameTime);
            gameMode.Update(gameTime);
            ProgressGame(gameTime);      
        }

        // wave progression stuff
        private void ProgressGame(GameTime gameTime)
        {
            int numberOfSpawns = NumberOfSpawns();
            if (spawnsThisLevel >= numberOfSpawns)
            {
                finishedSpawning = true;
                if (Traits.KILLS.Counter == Traits.ENEMIESSPAWNED.Counter && spawnsThisLevel != 0)
                {
                    gameMode.LevelFinished = true;
                    spawnsThisLevel = 0;
                    finishedSpawning = false;
                }              
            }
        }

        private int NumberOfSpawns()
        {
            int number = 0;
            switch (GameMode.Level % 10)
            {
                case 1:
                    number = 1;
                    break;
                case 2:
                    number = 6;
                    break;
                case 3:
                    number = 8;
                    break;
                case 4:
                    number = 10;
                    break;
                case 5:
                    number = 4;
                    break;
                case 6:
                    number = 6;
                    break;
                case 7:
                    number = 8;
                    break;
                case 8:
                    number = 2;
                    break;
                case 9:
                    number = 4;
                    break;
                case 0:
                    number = 1;
                    break;                    
            }
            return number;
        }

        private void UpdateSpawnHandlers(GameTime gameTime)
        {
            Drops.SpawnAt(dropPoints.SpawnPositions());
            Drops.SpawnMoneyAt(dropPoints.MoneySpawnPositions());
            spawnPointGen.Update(gameTime);
            if (!finishedSpawning && spawnTimer.Update(gameTime))
            {
                SpawnWave();                
            }                               
        }

        private void SpawnWave()
        {
            Vector2[] spawnPoints = spawnPointGen.GetSpawnPoints();
            foreach (Vector2 v in spawnPoints)
            {
                enemies.Spawn(v);
                ++spawnsThisLevel;
                Traits.ENEMIESSPAWNED.Counter++;                                
            }            
        }    

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            enemies.Draw(spriteBatch, gameTime);
            Drops.Draw(spriteBatch, gameTime);
            if(isActive)
                gameMode.Draw(spriteBatch, gameTime, fullDraw);
        }

        public void Reset(bool fullReset)
        {
            spawnsThisLevel = 0; //!
            finishedSpawning = false;
            levelTimer.Reset();
            enemies.Reset();
            gameMode.Reset(fullReset);
            Drops.Reset();
            dropPoints.Reset();
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
            } //else if (!gameMode.BetweenLevelsTimer.IsFinished)
            //{
            //    isActive = false;
            //}
        }        

        //duh
        public List<IActivatable> CollidableList()
        {
            return enemies.GetValues();
        }       
    }
}
