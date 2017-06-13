﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SummerProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Wall wall;
        Enemies enemies;
        Projectiles projectiles;

        CollisionHandler colhandl;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D enemyTex = Content.Load<Texture2D>("enemy");
            Texture2D shipTex = Content.Load<Texture2D>("ship");
            Texture2D wallTex = Content.Load<Texture2D>("wall");
            player = new Player(new Vector2(100, 100), new Sprite(shipTex), new Sprite(shipTex));
            Texture2D shotTex = Content.Load<Texture2D>("lazor");            
            player = new Player(new Vector2(100, 100), new Sprite(shipTex));
            enemies = new Enemies(new Sprite(enemyTex), player, 100);
            wall = new Wall(new Vector2(300, 300), new Sprite(wallTex));
            colhandl = new CollisionHandler();           
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
            player.Update();
            enemies.Update();
            projectiles.Update();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                projectiles.Fire(player.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                enemies.Spawn(new Vector2(250, 250), gameTime);
            List<Enemy> enemyList = enemies.getEnemyList();
            foreach (Enemy e in enemyList)
            {
                colhandl.CheckCollisions(player, wall, e);
            }
            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();           
            player.Draw(spriteBatch);
            player.projectiles.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            enemies.Draw(spriteBatch);
            projectiles.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
