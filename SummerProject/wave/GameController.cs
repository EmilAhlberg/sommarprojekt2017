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
        public Enemies Enemies { get; private set; }
        public Drops Drops { get; private set; }
        public Projectiles Projectiles { get; private set; }
        public Player Player { get; private set; }

        private CollisionHandler colHandler;        
        private GameMode gameMode;
        private SpawnPointGenerator spawnPointGen;
        private SpawnTimer spawnTimer;       
        private DropSpawnPoints dropPoints;
        private int spawnsThisLevel;
        private bool finishedSpawning;
        private bool isActive;


        public GameController(Player player, Projectiles projectiles, Drops drops, GameMode gameMode)
        {
            colHandler = new CollisionHandler();
            this.Player = player;
            this.Projectiles = projectiles;
            Drops = drops;
            this.gameMode = gameMode;
            spawnPointGen = new SpawnPointGenerator(gameMode);
            spawnTimer = new SpawnTimer(gameMode);  
            Enemies = new Enemies(player); 
            dropPoints = new DropSpawnPoints();
        }

        public void Update(GameTime gameTime, bool cutScene, int newGameState)
        {
            Player.Update(gameTime, cutScene);
            CheckActive();
            if (!cutScene)
            {
                if (isActive && gameMode.BetweenLevelsTimer.IsFinished)
                    UpdateSpawnHandlers(gameTime);

                if (newGameState != EventOperator.CUT_SCENE_STATE)
                    gameMode.Update(gameTime);
                ProgressGame(gameTime);
            }
            Drops.Update(gameTime);
            Enemies.Update(gameTime);
            Projectiles.Update(gameTime);
            HandleAllCollisions();
        }

        private void ProgressGame(GameTime gameTime)
        {
            if (GameMode.Level % 10 == 1)
                Player.Health = Player.maxHealth;
            int numberOfSpawns = NumberOfSpawns();           
            if (spawnsThisLevel >= numberOfSpawns)
            {
                finishedSpawning = true;
                int kills = (int) Traits.KILLS.Counter;
                int spawns = (int)Traits.ENEMIESSPAWNED.Counter;
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
                    number = 3;
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
                    number = 12;
                    break;
                case 6:
                    number = 14;
                    break;
                case 7:
                    number = 16;
                    break;
                case 8:
                    number = 20;
                    break;
                case 9:
                    number = 15;
                    break;
                case 0:
                    number = 1;
                    break;                    
            }
            return number;
        }

        private void UpdateSpawnHandlers(GameTime gameTime)
        {
            if (SRandom.NextFloat() > 0.9980f) //! background asteroid chance    
                Enemies.SpawnAsteroid(spawnPointGen.GetAsteroidSpawnPoint());

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
                Enemies.Spawn(v);
                ++spawnsThisLevel;
                Traits.ENEMIESSPAWNED.Counter++;                                
            }            
        }    

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            Player.Draw(spriteBatch, gameTime);
            Enemies.Draw(spriteBatch, gameTime);
            Drops.Draw(spriteBatch, gameTime);
            Projectiles.Draw(spriteBatch, gameTime);
            if(isActive)
                gameMode.Draw(spriteBatch, gameTime, fullDraw);
        }

        public void Reset(bool fullReset)
        {
            spawnsThisLevel = 0; //!
            finishedSpawning = false;
            if (fullReset)
                Player.Activate(Player.StartPosition, Vector2.Zero);
            Enemies.Reset();
            gameMode.Reset(fullReset);
            Projectiles.Reset();
            Drops.Reset();
            dropPoints.Reset();
        }

        private void HandleAllCollisions()
        {
            List<Collidable> pParts = Player.Parts.ConvertAll(p => (Collidable)p);
            List<Collidable> eParts = Enemies.GetValues().Where(e => ((Enemy)e).IsActive).SelectMany(e => ((Enemy)e).Parts).ToList().ConvertAll(p => (Collidable)p);
            List<Collidable> pBullets = Projectiles.GetValues().Where(p => ((Projectile)p).Team != EntityConstants.GetStatsFromID(EntityConstants.TEAM, IDs.DEFAULT_ENEMY) && ((Projectile)p).IsActive).ToList().ConvertAll(p => (Collidable)p);
            List<Collidable> eBullets = Projectiles.GetValues().Where(p => ((Projectile)p).Team == EntityConstants.GetStatsFromID(EntityConstants.TEAM, IDs.DEFAULT_ENEMY) && ((Projectile)p).IsActive).ToList().ConvertAll(p => (Collidable)p);
            List<Collidable> eDrops = Drops.GetValues().Where(d => ((Drop)d).IsActive).ToList().ConvertAll(p => (Collidable)p);
            colHandler.CheckCollisions(pParts, eParts);
            colHandler.CheckCollisions(pParts, eBullets);
            colHandler.CheckCollisions(pParts, eDrops);
            colHandler.CheckCollisions(eParts, pBullets);
        }

        private void CheckActive()
        {
            if (!isActive)
            {
                if (Player.IsActive)
                    isActive = true;
            }
            else if (!Player.IsActive)
            {
                isActive = false;
            } 
        }        
    }
}
