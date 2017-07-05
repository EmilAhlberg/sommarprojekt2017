using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using SummerProject.factories;
using SummerProject.collidables;
using System;
using SummerProject.wave;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.collidables.parts;

namespace SummerProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteFont debugFont;
        SpriteFont bigFont;
        SpriteFont scoreFont;
        SpriteBatch spriteBatch;
        EventOperator eventOperator;
        Player player;
        Wall wall;
        Timer deathTimer;
        GameController gameController;    
        Projectiles projectiles;
        Sprite background;
        CollisionHandler colhandl;
        UnitBar healthBar;
        UnitBar energyBar;
        AchievementController achController;
        const bool SPAWN_ENEMIES = true;
        bool slowmo = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            WindowSize.Height -= 100;           // temp fix for window debug
            WindowSize.Width -= 50;
            graphics.PreferredBackBufferWidth = WindowSize.Width;
            graphics.PreferredBackBufferHeight = WindowSize.Height;
            Content.RootDirectory = "Content";
            //this.IsFixedTimeStep = false; //use to make game rly fast :)
            //graphics.SynchronizeWithVerticalRetrace = false;
            //graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            //Mouse.SetCursor(MouseCursor.Crosshair);  
            deathTimer = new Timer(3); //!
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Adding base texture to Sprite

            Texture2D baseTex = new Texture2D(GraphicsDevice, 2, 2);
            Color[] c = new Color[4];
            c[0] = Color.White;
            c[1] = Color.White;
            c[2] = Color.White;
            c[3] = Color.White;
            baseTex.SetData(c);
            Sprite.addBaseTexture(baseTex);

            #endregion

            #region Loading fonts

            debugFont = Content.Load<SpriteFont>("fonts/debugfont");
            scoreFont = Content.Load<SpriteFont>("fonts/ScoreFont");
            bigFont = Content.Load<SpriteFont>("fonts/BigScoreFont");
            #endregion

            #region Loading textures
            Texture2D backgroundTex = Content.Load<Texture2D>("textures/background1");
            Texture2D enemyTex1 = Content.Load<Texture2D>("textures/enemyShip");
            Texture2D enemyTex2 = Content.Load<Texture2D>("textures/enemyShoot");
            Texture2D enemyTex3 = Content.Load<Texture2D>("textures/enemySpeed");
            Texture2D enemyTex4 = Content.Load<Texture2D>("textures/asteroid");
            Texture2D shipTex = Content.Load<Texture2D>("parts/Hull_3");
            Texture2D wallTex = Content.Load<Texture2D>("textures/wall");
            Texture2D shotTex = Content.Load<Texture2D>("textures/lazor");
            Texture2D sprayBulletTex = Content.Load<Texture2D>("textures/SprayBullet");
            Texture2D mineBulletTex = Content.Load<Texture2D>("textures/mineBullet_1");
            Texture2D homingTex = Content.Load<Texture2D>("textures/homing");
            Texture2D partTex1 = Content.Load<Texture2D>("textures/shipPart1");
            Texture2D partTex2 = Content.Load<Texture2D>("textures/shipPart2");
            Texture2D deadTex1 = Content.Load<Texture2D>("textures/denemy1");
            Texture2D deadTex2 = Content.Load<Texture2D>("textures/denemy2");
            Texture2D deadTex3 = Content.Load<Texture2D>("textures/dship1");
            Texture2D deadTex4 = Content.Load<Texture2D>("textures/dship2");
            Texture2D deadTex5 = Content.Load<Texture2D>("textures/denemyShoot1");
            Texture2D deadTex6 = Content.Load<Texture2D>("textures/denemyShoot2");
            Texture2D deadTex7 = Content.Load<Texture2D>("textures/denemySpeed1");
            Texture2D deadTex8 = Content.Load<Texture2D>("textures/denemySpeed2");
            Texture2D deadTex9 = Content.Load<Texture2D>("textures/dasteroid1");
            Texture2D deadTex10 = Content.Load<Texture2D>("textures/dasteroid2");
            Texture2D deadTex11 = Content.Load<Texture2D>("textures/dasteroid3");
            Texture2D plusTex = Content.Load<Texture2D>("textures/plus");
            Texture2D healthDropTex = Content.Load<Texture2D>("textures/healthPack");
            Texture2D healthDrop_TIER2_Tex = Content.Load<Texture2D>("textures/healthDrop_TIER2");
            Texture2D wrenchTex = Content.Load<Texture2D>("textures/wrench");
            Texture2D explosionDropTex = Content.Load<Texture2D>("textures/explosionDrop");
            Texture2D boltTex = Content.Load<Texture2D>("textures/bolt");
            Texture2D energyDropTex = Content.Load<Texture2D>("textures/energyDrop");
            Texture2D unitBarBorderTex = Content.Load<Texture2D>("textures/unitBarBorder");
            Texture2D cursorTex = Content.Load<Texture2D>("textures/cursor");
            Texture2D gunTex1 = Content.Load<Texture2D>("parts/Gun_1");
            Texture2D engineTex1 = Content.Load<Texture2D>("parts/Engine_1");
            List<Texture2D> allUpgradeParts = new List<Texture2D>();
            allUpgradeParts.Insert(PartTypes.RECTANGULARHULL, shipTex);         
            allUpgradeParts.Insert(PartTypes.ENGINEPART, engineTex1);
            allUpgradeParts.Insert(PartTypes.GUNPART, gunTex1);
            #endregion

            #region Adding entity-sprites to list
            Entities.Sprites[EntityTypes.ENEMY] = new Sprite(enemyTex1, 2, 4);
            Entities.Sprites[EntityTypes.ENEMYSHOOT] = new Sprite(enemyTex2, 2, 4);
            Entities.Sprites[EntityTypes.ENEMYSPEED] = new Sprite(enemyTex3, 2, 4);
            Entities.Sprites[EntityTypes.ENEMYASTER] = new Sprite(enemyTex4);
            Entities.Sprites[EntityTypes.BULLET] = new Sprite(shotTex,4);
            Entities.Sprites[EntityTypes.SPRAYBULLET] = new Sprite(sprayBulletTex, 2);
            Entities.Sprites[EntityTypes.HOMINGBULLET] = new Sprite(homingTex);
            Entities.Sprites[EntityTypes.HEALTHDROP] = new Sprite(healthDropTex,4,6);
            Entities.Sprites[EntityTypes.HEALTHDROP_TIER2] = new Sprite(healthDrop_TIER2_Tex, 4, 6);
            Entities.Sprites[EntityTypes.EXPLOSIONDROP] = new Sprite(explosionDropTex,8,6);
            Entities.Sprites[EntityTypes.ENERGYDROP] = new Sprite(energyDropTex,8,6);
            Entities.Sprites[EntityTypes.MINEBULLET] = new Sprite(mineBulletTex, 9, 6);
            #endregion

            #region Initializing game objects etc.
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), new Sprite(shipTex), projectiles);
            //Camera.Player = player; //Reintroduce if camera is to be used
            achController = new AchievementController(bigFont);
            SaveHandler.InitializeGame(achController);
            GameMode gameMode = new GameMode(scoreFont);          
           
            background = new Sprite(backgroundTex);
            projectiles = new Projectiles(30); //! bulletCap hardcoded
            GunPart.projectiles = projectiles;
            eventOperator = new EventOperator(bigFont, this, shipTex, gameMode, achController, player, allUpgradeParts); // fix new texture2d's!!
            RectangularHull rectHull1 = new RectangularHull(new Sprite(shipTex));
            RectangularHull rectHull2 = new RectangularHull(new Sprite(shipTex));
            GunPart gunPart1 = new GunPart(new Sprite(gunTex1));
            GunPart gunPart2 = new SprayGunPart(new Sprite(gunTex1));
            GunPart gunPart3 = new MineGunPart(new Sprite(gunTex1));
            EnginePart engine1 = new EnginePart(new Sprite(engineTex1));
            EnginePart engine2 = new EnginePart(new Sprite(engineTex1));
            EnginePart engine3 = new EnginePart(new Sprite(engineTex1));
            player.AddPart(rectHull1, 0);
            player.AddPart(rectHull2, 2);
            player.AddPart(engine1, 1);
            rectHull1.AddPart(engine2, 1);
            rectHull2.AddPart(engine3, 1);
            player.AddPart(gunPart1, 3);
            rectHull1.AddPart(gunPart2, 3);
            rectHull2.AddPart(gunPart3, 3);
            Drops drops = new Drops(10, WindowSize.Width, WindowSize.Height); //!! dropCap
            gameController = new GameController(player, drops, gameMode);
            colhandl = new CollisionHandler();
            wall = new Wall(new Vector2(-4000, -4000), new Sprite(wallTex)); //! wall location
            healthBar = new UnitBar(new Vector2(50, 50), new Sprite(unitBarBorderTex), Color.OrangeRed, player.maxHealth);
            energyBar = new UnitBar(new Vector2(50, 85), new Sprite(unitBarBorderTex), Color.Gold, player.maxEnergy);
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursorTex, cursorTex.Width/2, cursorTex.Height/2));


            

            #endregion

            #region Adding sprites to particles
            Particles.AddSprite(new Sprite(deadTex2));
            Particles.AddSprite(new Sprite(deadTex1));
            Particles.AddSprite(new Sprite(deadTex4));
            Particles.AddSprite(new Sprite(deadTex3));
            Particles.AddSprite(new Sprite(deadTex6));
            Particles.AddSprite(new Sprite(deadTex5));
            Particles.AddSprite(new Sprite(deadTex8));
            Particles.AddSprite(new Sprite(deadTex7));
            Particles.AddSprite(new Sprite(wrenchTex));
            Particles.AddSprite(new Sprite(boltTex));
            Particles.AddSprite(new Sprite(deadTex9));
            Particles.AddSprite(new Sprite(deadTex10));
            Particles.AddSprite(new Sprite(deadTex11));
            #endregion
            // TODO: use this.Content to load your game content here
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (eventOperator.GameState == EventOperator.GAME_STATE && eventOperator.NewGameState == EventOperator.GAME_STATE)
                UpdateGame(gameTime);
            else
                eventOperator.Update(gameTime);

            CheckGameStatus(gameTime);
            achController.Update(gameTime);
            InputHandler.UpdatePreviousState();
            base.Update(gameTime);
        }

        public void UpdateGame(GameTime gameTime)
        {
            #region Update for game state
            player.Update(gameTime);
            if (SPAWN_ENEMIES)
                gameController.Update(gameTime);
            projectiles.Update(gameTime);
            Particles.Update(gameTime);
            HandleAllCollisions();
            KeepPlayerInScreen();
            healthBar.Update(player.Health, player.maxHealth);
            energyBar.Update(player.Energy, player.maxEnergy);
            Traits.TIME.Counter +=(float) gameTime.ElapsedGameTime.TotalSeconds;
            float temp = Traits.TIME.Counter;
            #endregion
        }

        private void CheckGameStatus(GameTime gameTime)
        {
            #region Game Over
            if (!player.IsActive && eventOperator.GameState == EventOperator.GAME_STATE)
            {
                ResetGame(false);
                deathTimer.CountDown(gameTime);
                if (deathTimer.IsFinished)
                {
                    eventOperator.NewGameState = EventOperator.GAME_OVER_STATE;
                    deathTimer.Reset();
                }
            }
            #endregion
            #region Pause
            if (InputHandler.isJustPressed(Keys.Escape) && eventOperator.GameState == EventOperator.GAME_STATE)
            {
                eventOperator.NewGameState = EventOperator.PAUSE_STATE;
            }
            #endregion
            #region Upgrade Ship
            if (InputHandler.isJustPressed(Keys.M) && eventOperator.GameState == EventOperator.GAME_STATE)
            {
                eventOperator.NewGameState = EventOperator.UPGRADE_STATE;
            }
            #endregion
        }

        public void ResetGame(bool fullReset)
        {
            if (fullReset)
            {
                player.Reset();
                Particles.Reset();
                ScoreHandler.Reset();
                healthBar.Reset();
                energyBar.Reset();
            }
            projectiles.Reset();  
            gameController.Reset(fullReset);
        }

        private void HandleAllCollisions()
        {
            List<Collidable> collidableList = new List<Collidable>();
            foreach (Collidable c in gameController.CollidableList())
            {
                collidableList.Add(c);
            }
            foreach (Collidable c in projectiles.GetValues())
            {
                collidableList.Add(c);
            }
            foreach (Collidable c in gameController.Drops.GetValues())
            {
                collidableList.Add(c);
            }
            colhandl.CheckCollisions(collidableList.ToArray(), player, wall);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);
            background.Draw(spriteBatch, gameTime);
            if (eventOperator.GameState == EventOperator.GAME_STATE)
            {
                #region Draw for GameState
                DrawGame(spriteBatch, gameTime, true);


                #region DrawString
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),scoreFont, "Score: " + ScoreHandler.Score, new Vector2(WindowSize.Width - 300, 50), Color.Gold);
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),scoreFont, "High Score: " + ScoreHandler.HighScore, new Vector2(WindowSize.Width / 2 - scoreFont.MeasureString("High Score: " + ScoreHandler.HighScore).X / 2, 50), Color.Gold);
                Vector2 shitvect = new Vector2(WindowSize.Width / 2 - bigFont.MeasureString("GAME OVER").X / 2, WindowSize.Height / 2 - bigFont.MeasureString("GAME OVER").Y / 2);
                if (!player.IsActive)
                    spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),bigFont, "GAME OVER", shitvect, Color.OrangeRed);
                #endregion

                #endregion
            }
            else
                eventOperator.Draw(spriteBatch, gameTime);

            DebugMode(spriteBatch, gameTime);
            achController.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);
            if (eventOperator.GameState == EventOperator.GAME_STATE)
                DrawSpecialTransparency(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);

        }

        public void DrawGame(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            #region Draw Game
            Particles.Draw(spriteBatch, gameTime);
            projectiles.Draw(spriteBatch, gameTime);
            player.Draw(spriteBatch, gameTime);
            wall.Draw(spriteBatch, gameTime);
            gameController.Draw(spriteBatch, gameTime, fullDraw);
            #endregion
        }

        public void DrawSpecialTransparency(SpriteBatch spriteBatch, GameTime gameTime )
        {
            healthBar.Draw(spriteBatch, gameTime);
            energyBar.Draw(spriteBatch, gameTime);
        }

        private void KeepPlayerInScreen()
        {
            float x = player.Position.X;
            float y = player.Position.Y;
            if (player.Position.X > WindowSize.Width)
                x = WindowSize.Width;
            if (player.Position.Y > WindowSize.Height)
                y = WindowSize.Height;
            if (player.Position.X < 0)
                x = 0;
            if (player.Position.Y < 0)
                y = 0;
            player.Position = new Vector2(x, y);
        }

        private void DebugMode(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int controlSheme = player.ControlScheme;
            string usingControls = "";
            if (controlSheme <= 1)
                usingControls = "WASD + Follow mouse";
            if (controlSheme == 2)
                usingControls = "Absolute WASD";
            if (controlSheme == 3)
                usingControls = "Mouse only";
            if (controlSheme == 4) 
                usingControls = "WASD : AD = Rotate";            
            //spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),debugFont, "Player pos: " +player.Position, new Vector2(600, 100), Color.Yellow);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),scoreFont, "Controls: " + controlSheme + " - " + usingControls, new Vector2(WindowSize.Width - 700, WindowSize.Height - 50), Color.Crimson);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),scoreFont, "FPS: " + (int)Math.Round(1/gameTime.ElapsedGameTime.TotalSeconds), new Vector2(50, WindowSize.Height - 50), Color.Gold);

            //spriteBatch.DrawString(debugFont, "Player pos: " +player.Position, new Vector2(600, 100), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Part pos: " + player.Hull.Parts[0].BoundBoxes[0].Position, new Vector2(600, 200), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Player origin: " + player.Hull.BoundBoxes[0].Origin, new Vector2(600, 300), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Part origin: " + player.Hull.Parts[0].BoundBoxes[0].Origin, new Vector2(600, 400), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Player intersects wall: " + player.Hull.BoundBoxes[0].Intersects(wall.BoundBoxes[0]), new Vector2(600, 500), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Player part intersects wall: " + player.Hull.Parts[1].BoundBoxes[0].Intersects(wall.BoundBoxes[0]), new Vector2(600, 600), Color.Yellow);
            //spriteBatch.DrawString(debugFont, "Player intersects part: " + player.Hull.Parts[1].BoundBoxes[0].Intersects(player.Hull.BoundBoxes[0]), new Vector2(600, 700), Color.Yellow);
            //spriteBatch.DrawString(scoreFont, "Controls: " + controlSheme + " - " + usingControls, new Vector2(graphics.PreferredBackBufferWidth-700, graphics.PreferredBackBufferHeight -100), Color.Crimson);
            //Rectangle cR = new Rectangle((int)(player.Hull.BoundBoxes[0].Position.X), (int)(player.Hull.BoundBoxes[0].Position.Y), (int)(player.Hull.BoundBoxes[0].Width), (int)(player.Hull.BoundBoxes[0].Height));
            //Rectangle cR2 = new Rectangle((int)(player.Hull.Parts[0].BoundBoxes[0].Position.X), (int)(player.Hull.Parts[0].BoundBoxes[0].Position.Y), (int)(player.Hull.Parts[0].BoundBoxes[0].Width), (int)(player.Hull.Parts[0].BoundBoxes[0].Height));
            //spriteBatch.Draw(Content.Load<Texture2D>("textures/ship"),player.Hull.BoundBoxes[0].Position,cR, Color.Aqua, player.Hull.BoundBoxes[0].Angle, player.Hull.BoundBoxes[0].Origin, new Vector2(1,1),SpriteEffects.None,1);
            //spriteBatch.Draw(Content.Load<Texture2D>("textures/plus"), player.Hull.Parts[0].BoundBoxes[0].Position, cR2, Color.Red, player.Hull.Parts[0].BoundBoxes[0].Angle, player.Hull.Parts[0].BoundBoxes[0].Origin, new Vector2(1, 1), SpriteEffects.None, 1);

            //wall = new Wall(new Vector2(500, 500), new Sprite(Content.Load<Texture2D>("textures/wall")));
            //wall.Angle = (float)Math.PI / 3;
            //wall.Origin = new Vector2(-100, -100);
            //wall.Angle = (float)Math.PI*3/2;
            //wall.Draw(spriteBatch, gameTime);
            //spriteBatch.Draw(Content.Load<Texture2D>("textures/ship"), new Vector2(100,100), cR2, Color.Aqua, (float)Math.PI/2, new Vector2(cR2.Width/2,cR2.Height/2), new Vector2(1, 1), SpriteEffects.None, 1);
        }
    }
}
