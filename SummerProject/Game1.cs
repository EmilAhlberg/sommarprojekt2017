﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;
using SummerProject.factories;
using SummerProject.collidables;
using SummerProject.menu;

namespace SummerProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteFont debugFont;
        SpriteFont scoreFont;
        SpriteBatch spriteBatch;
        EventOperator eventOperator;
        Player player;
        //Wall wall;
        Enemies enemies;
        Projectiles projectiles;
        Sprite background;
        CollisionHandler colhandl;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
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
            base.Initialize();
        }

        internal void StartGame(GameTime gameTime)
        {
            while (eventOperator.ActiveEvent)
            {
                Draw(gameTime);
                eventOperator.UpdateEventTimer(gameTime);
            }
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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

            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugFont = Content.Load<SpriteFont>("debugfont");
            scoreFont = Content.Load<SpriteFont>("ScoreFont");
            SpriteFont bigFont = Content.Load<SpriteFont>("BigScoreFont");

            Texture2D backgroundTex = Content.Load<Texture2D>("background1");
            Texture2D enemyTex = Content.Load<Texture2D>("enemy");
            Texture2D shipTex = Content.Load<Texture2D>("ship");
            Texture2D wallTex = Content.Load<Texture2D>("wall");
            Texture2D shotTex = Content.Load<Texture2D>("lazor");
            Texture2D homingTex = Content.Load<Texture2D>("homing");
            Texture2D partTex1 = Content.Load<Texture2D>("shipPart1");
            Texture2D partTex2 = Content.Load<Texture2D>("shipPart2");
            Texture2D deadTex1 = Content.Load<Texture2D>("denemy1");
            Texture2D deadTex2 = Content.Load<Texture2D>("denemy2");
            Texture2D deadTex3 = Content.Load<Texture2D>("dship1");
            Texture2D deadTex4 = Content.Load<Texture2D>("dship2");

            List<Sprite> bulletSprites = new List<Sprite>();
            List<Sprite> enemySprites = new List<Sprite>();
            enemySprites.Add(new Sprite(enemyTex));        // order is important
            bulletSprites.Add(new Sprite(shotTex, 4));
            bulletSprites.Add(new Sprite(homingTex));

            CompositeSprite compSpr = new CompositeSprite();

            compSpr.addSprite(new Sprite(shipTex), new Vector2(0, 0));
            compSpr.addSprite(new Sprite(partTex1), new Vector2(0, -16));
            compSpr.addSprite(new Sprite(partTex2), new Vector2(0, 16));

            eventOperator = new EventOperator(new Vector2((Window.ClientBounds.Width) / 2, (Window.ClientBounds.Height) / 2), bigFont, this);
            
            background = new Sprite(backgroundTex);
            projectiles = new Projectiles(bulletSprites, 30);
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2), compSpr, projectiles);
            enemies = new Enemies(enemySprites, player, 10, 3);    
            //wall = new Wall(new Vector2(300, 300), new Sprite(wallTex));
            colhandl = new CollisionHandler();

            Particles.AddSprite(new Sprite(deadTex2));
            Particles.AddSprite(new Sprite(deadTex1));
            Particles.AddSprite(new Sprite(deadTex4));
            Particles.AddSprite(new Sprite(deadTex3));
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

            if (eventOperator.GameState == EventOperator.GAME_STATE)
            {
                player.Update(gameTime);
                enemies.Update(gameTime);
                projectiles.Update(gameTime);
                Particles.Update(gameTime);
                HandleAllCollisions();
                KeepPlayerInScreen();
            }                             
            else {
                eventOperator.Update(gameTime);
            }             
                           
            base.Update(gameTime);
        }

        private void HandleAllCollisions()
        {
            List<Collidable> collidableList = new List<Collidable>();
            foreach (Collidable c in enemies.entityList)
            {
                collidableList.Add(c);
            }
            foreach (Collidable c in projectiles.entityList)
            {
                collidableList.Add(c);
            }
            colhandl.CheckCollisions(collidableList.ToArray(), player /*,wall*/);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            background.Draw(spriteBatch, gameTime);
            if(eventOperator.GameState == EventOperator.GAME_STATE)
            {
                Particles.Draw(spriteBatch, gameTime);
                projectiles.Draw(spriteBatch, gameTime);
                player.Draw(spriteBatch, gameTime);
                //wall.Draw(spriteBatch, gameTime);
                enemies.Draw(spriteBatch, gameTime);
            } else
            {
                eventOperator.Draw(spriteBatch, gameTime);
            }            
            //
            DebugMode(spriteBatch);
            //
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void KeepPlayerInScreen()
        {
            float x = player.Position.X;
            float y = player.Position.Y;
            if (player.Position.X > graphics.PreferredBackBufferWidth)
                x = graphics.PreferredBackBufferWidth;
            if (player.Position.Y > graphics.PreferredBackBufferHeight)
                y = graphics.PreferredBackBufferHeight;
            if (player.Position.X < 0)
                x = 0;
            if (player.Position.Y < 0)
                y = 0;
            player.Position = new Vector2(x, y);
        }

        private void DebugMode(SpriteBatch spriteBatch)
        {
            int controlSheme = player.controlScheme;
            string usingControls = "";
            if (controlSheme <= 1)
                usingControls = "WASD + Follow mouse";
            if (controlSheme == 2)
                usingControls = "Absolute WASD";
            if (controlSheme == 3)
                usingControls = "Mouse only";
            if (controlSheme == 4)
                usingControls = "WASD : AD = Rotate";

            if (eventOperator.GameState == EventOperator.GAME_STATE)
            {
                //spriteBatch.DrawString(debugFont, "Player pos: " +player.Position, new Vector2(600, 100), Color.Yellow);
                spriteBatch.DrawString(scoreFont, "Score: " + player.score, new Vector2(1600, 50), Color.Gold);
                spriteBatch.DrawString(scoreFont, "Health: " + player.Health / 2, new Vector2(1600, 90), Color.OrangeRed);
                spriteBatch.DrawString(scoreFont, "High Score: " + player.highScore, new Vector2(graphics.PreferredBackBufferWidth / 2 - scoreFont.MeasureString("High Score: " + player.highScore).X / 2, 50), Color.Gold);
                spriteBatch.DrawString(scoreFont, "Controls: " + controlSheme + " - " + usingControls, new Vector2(1250, 1000), Color.Crimson);
            }
        }
    }   
}
