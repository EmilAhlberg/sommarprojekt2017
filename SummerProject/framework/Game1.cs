﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using SummerProject.factories;
using SummerProject.collidables;
using System;
using SummerProject.wave;

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
        WaveGenerator waveGenerator;    
        Projectiles projectiles;
        Sprite background;
        CollisionHandler colhandl;    
        const bool SPAWN_ENEMIES = true;

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
            Texture2D enemyTex = Content.Load<Texture2D>("textures/enemyShip");
            Texture2D shipTex = Content.Load<Texture2D>("textures/ship");
            Texture2D wallTex = Content.Load<Texture2D>("textures/wall");
            Texture2D shotTex = Content.Load<Texture2D>("textures/lazor");
            Texture2D homingTex = Content.Load<Texture2D>("textures/homing");
            Texture2D partTex1 = Content.Load<Texture2D>("textures/shipPart1");
            Texture2D partTex2 = Content.Load<Texture2D>("textures/shipPart2");
            Texture2D deadTex1 = Content.Load<Texture2D>("textures/denemy1");
            Texture2D deadTex2 = Content.Load<Texture2D>("textures/denemy2");
            Texture2D deadTex3 = Content.Load<Texture2D>("textures/dship1");
            Texture2D deadTex4 = Content.Load<Texture2D>("textures/dship2");
            Texture2D plusTex = Content.Load<Texture2D>("textures/plus");
            Texture2D healthDropTex = Content.Load<Texture2D>("textures/healthPack");
            Texture2D wrenchTex = Content.Load<Texture2D>("textures/wrench");
            Texture2D explosionDropTex = Content.Load<Texture2D>("textures/explosionDrop");
            Texture2D boltTex = Content.Load<Texture2D>("textures/bolt");
            Texture2D energyDropTex = Content.Load<Texture2D>("textures/energyDrop");
            #endregion

            #region Adding entity-sprites to list
            Entities.Sprites[EntityTypes.ENEMY] = new Sprite(enemyTex, 2, 4);
            Entities.Sprites[EntityTypes.BULLET] = new Sprite(shotTex,4);
            Entities.Sprites[EntityTypes.HOMINGBULLET] = new Sprite(homingTex);
            Entities.Sprites[EntityTypes.HEALTHDROP] = new Sprite(healthDropTex,4,6);
            Entities.Sprites[EntityTypes.EXPLOSIONDROP] = new Sprite(explosionDropTex,8,6);
            Entities.Sprites[EntityTypes.ENERGYDROP] = new Sprite(energyDropTex,8,6);
            #endregion

            #region Testing composite sprite
            CompositeSprite compSpr = new CompositeSprite();

            compSpr.addSprite(new Sprite(shipTex), new Vector2(0, 0));
            //compSpr.addSprite(new Sprite(partTex1), new Vector2(0, -16));
            //compSpr.addSprite(new Sprite(partTex2), new Vector2(0, 16));

            #endregion

            #region Initializing game objects etc.
            GameMode gameMode = new GameMode(scoreFont);
            eventOperator = new EventOperator(bigFont, this, homingTex, gameMode); // fix new texture2d's!!
            background = new Sprite(backgroundTex);
            projectiles = new Projectiles(30); //! bulletCap hardcoded
            player = new Player(new Vector2(WindowSize.Width / 2, WindowSize.Height / 2), compSpr, projectiles);
            Drops drops = new Drops(10, WindowSize.Width, WindowSize.Height); //!! dropCap
            waveGenerator = new WaveGenerator(player, drops, gameMode);
            colhandl = new CollisionHandler();
            wall = new Wall(new Vector2(300, 300), new Sprite(wallTex)); //! wall location
           
            #endregion

            #region Adding sprites to particles
            Particles.AddSprite(new Sprite(deadTex2));
            Particles.AddSprite(new Sprite(deadTex1));
            Particles.AddSprite(new Sprite(deadTex4));
            Particles.AddSprite(new Sprite(deadTex3));
            Particles.AddSprite(new Sprite(plusTex));
            Particles.AddSprite(new Sprite(boltTex));
            Particles.AddSprite(new Sprite(wrenchTex));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (eventOperator.GameState == EventOperator.GAME_STATE && eventOperator.NewGameState == EventOperator.GAME_STATE)
                UpdateGame(gameTime);
            else
                eventOperator.Update(gameTime);

            CheckGameStatus(gameTime);

            InputHandler.UpdatePreviousState();
            base.Update(gameTime);
        }

        public void UpdateGame(GameTime gameTime)
        {
            #region Update for game state
            player.Update(gameTime);
            if (SPAWN_ENEMIES)
                waveGenerator.Update(gameTime);
            projectiles.Update(gameTime);
            Particles.Update(gameTime);
            HandleAllCollisions();
            KeepPlayerInScreen();        
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
            if (InputHandler.isPressed(Keys.P) && eventOperator.GameState == EventOperator.GAME_STATE)
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
            }
            projectiles.Reset();  
            waveGenerator.Reset(fullReset);
        }

        private void HandleAllCollisions()
        {
            List<Collidable> collidableList = new List<Collidable>();
            foreach (Collidable c in waveGenerator.CollidableList())
            {
                collidableList.Add(c);
            }
            foreach (Collidable c in projectiles.GetValues())
            {
                collidableList.Add(c);
            }
            foreach (Collidable c in waveGenerator.Drops.GetValues())
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
            background.Draw(spriteBatch, gameTime);
            if (eventOperator.GameState == EventOperator.GAME_STATE)
            {
                #region Draw for GameState
                DrawGame(spriteBatch, gameTime);


                #region DrawString
                spriteBatch.DrawString(scoreFont, "Score: " + ScoreHandler.Score, new Vector2(WindowSize.Width - 300, 50), Color.Gold);
                spriteBatch.DrawString(scoreFont, "Health: " + player.Health, new Vector2(WindowSize.Width - 300, 100), Color.OrangeRed);
                spriteBatch.DrawString(scoreFont, "Energy: " + (int)player.Energy, new Vector2(WindowSize.Width - 300, 150), Color.Gold);
                spriteBatch.DrawString(scoreFont, "High Score: " + ScoreHandler.HighScore, new Vector2(WindowSize.Width / 2 - scoreFont.MeasureString("High Score: " + ScoreHandler.HighScore).X / 2, 50), Color.Gold);
                Vector2 shitvect = new Vector2(WindowSize.Width / 2 - bigFont.MeasureString("GAME OVER").X / 2, WindowSize.Height / 2 - bigFont.MeasureString("GAME OVER").Y / 2);
                if (!player.IsActive)
                    spriteBatch.DrawString(bigFont, "GAME OVER", shitvect, Color.OrangeRed);
                #endregion

                #endregion
            }
            else
                eventOperator.Draw(spriteBatch, gameTime);

            DebugMode(spriteBatch, gameTime);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void DrawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            #region Draw Game
            Particles.Draw(spriteBatch, gameTime);
            projectiles.Draw(spriteBatch, gameTime);
            player.Draw(spriteBatch, gameTime);
            wall.Draw(spriteBatch, gameTime);
            waveGenerator.Draw(spriteBatch, gameTime);      
            #endregion
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

            //spriteBatch.DrawString(debugFont, "Player pos: " +player.Position, new Vector2(600, 100), Color.Yellow);
            spriteBatch.DrawString(scoreFont, "Controls: " + controlSheme + " - " + usingControls, new Vector2(WindowSize.Width - 700, WindowSize.Height - 100), Color.Crimson);
            spriteBatch.DrawString(scoreFont, "FPS: " + (int)Math.Round(1/gameTime.ElapsedGameTime.TotalSeconds), new Vector2(0, 0), Color.Gold);

        }
    }
}