﻿using Microsoft.Xna.Framework;
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
using System.IO;
using System.Linq;
using SummerProject.collidables.parts.guns;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SummerProject.framework;
using PhysicsSimulator;

namespace SummerProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GameController gameController;
        GraphicsDeviceManager graphics;        
        SpriteBatch spriteBatch;
        EventOperator eventOperator;        
        //Wall wall;         
        Background background;
        PlayerUI playerUI;       
        const bool slowmo = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
         //   WindowSize.Height -= 100;           // temp fix for window debug
           // WindowSize.Width -= 50;
            graphics.PreferredBackBufferWidth = WindowSize.Width;
            graphics.PreferredBackBufferHeight = WindowSize.Height;
            Content.RootDirectory = "Content";        
            //graphics.IsFullScreen = true;
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
            Texture2DPlus baseTexPlus = new Texture2DPlus(baseTex);
            Sprite.addBaseTexture(baseTexPlus);
            #endregion

            #region Loading fonts

            DrawHelper.DEBUGFONT = Content.Load<SpriteFont>("fonts/debugfont");
            DrawHelper.SCOREFONT = Content.Load<SpriteFont>("fonts/ScoreFont");
            DrawHelper.BIGFONT = Content.Load<SpriteFont>("fonts/BigScoreFont");
            DrawHelper.UPGRADEFONT = Content.Load<SpriteFont>("fonts/currencyFont");
            #endregion

            #region Loading textures

            Texture2DPlus backgroundTex = new Texture2DPlus(Content.Load<Texture2D>("background/Background"));
            Texture2DPlus bluePlanetTex = new Texture2DPlus(Content.Load<Texture2D>("background/BluePlanet"));
            Texture2DPlus smallRedTex = new Texture2DPlus(Content.Load<Texture2D>("background/LittleRedPlanet"));
            Texture2DPlus bigRedTex = new Texture2DPlus(Content.Load<Texture2D>("background/RedBigPlanet"));
            Texture2DPlus cursorTex = new Texture2DPlus(Content.Load<Texture2D>("textures/cursor"));

            Texture2DPlus errorTex = new Texture2DPlus(Content.Load<Texture2D>("textures/noTexture"));
            Texture2DPlus enemyTex1 = new Texture2DPlus(Content.Load<Texture2D>("textures/enemyShip"), 2);
            Texture2DPlus enemyTex2 = new Texture2DPlus(Content.Load<Texture2D>("textures/enemyShoot"), 2);
            Texture2DPlus enemyTex3 = new Texture2DPlus(Content.Load<Texture2D>("textures/enemySpeed"), 2);
            Texture2DPlus enemyTex4 = new Texture2DPlus(Content.Load<Texture2D>("textures/asteroid"));
            Texture2DPlus shipTex = new Texture2DPlus(Content.Load<Texture2D>("parts/Hull_3"));
            Texture2DPlus wallTex = new Texture2DPlus(Content.Load<Texture2D>("textures/wall"));
            Texture2DPlus shotTex = new Texture2DPlus(Content.Load<Texture2D>("textures/lazor"), 4);
            Texture2DPlus sprayBulletTex = new Texture2DPlus(Content.Load<Texture2D>("textures/SprayBullet"), 2);
            Texture2DPlus mineBulletTex = new Texture2DPlus(Content.Load<Texture2D>("textures/mineBullet_1"), 9);
            Texture2DPlus chargingBulletTex = new Texture2DPlus(Content.Load<Texture2D>("textures/ChargingBullet"), 6);
            Texture2DPlus gravityBulletTex = new Texture2DPlus(Content.Load<Texture2D>("textures/GravityBullet"), 6);
            Texture2DPlus homingTex = new Texture2DPlus(Content.Load<Texture2D>("textures/homing"));
            Texture2DPlus healthDropTex = new Texture2DPlus(Content.Load<Texture2D>("textures/healthPack"),4);
            Texture2DPlus moneyDropTex = new Texture2DPlus(Content.Load<Texture2D>("textures/MoneyDrop"), 7);
            Texture2DPlus healthDrop_TIER2_Tex = new Texture2DPlus(Content.Load<Texture2D>("textures/healthDrop_TIER2"),4);
            Texture2DPlus wrenchTex = new Texture2DPlus(Content.Load<Texture2D>("textures/wrench"));
            Texture2DPlus explosionDropTex = new Texture2DPlus(Content.Load<Texture2D>("textures/explosionDrop"),8);
            Texture2DPlus boltTex = new Texture2DPlus(Content.Load<Texture2D>("textures/bolt"));
            Texture2DPlus moneyTex = new Texture2DPlus(Content.Load<Texture2D>("textures/money"));
            Texture2DPlus energyDropTex = new Texture2DPlus(Content.Load<Texture2D>("textures/energyDrop"),8);
            Texture2DPlus unitBarBorderTex = new Texture2DPlus(Content.Load<Texture2D>("textures/unitBarBorder"));
            Texture2DPlus gunTex1 = new Texture2DPlus(Content.Load<Texture2D>("parts/Gun_1"));
            Texture2DPlus mineGunTex = new Texture2DPlus(Content.Load<Texture2D>("parts/MineGun"));
            Texture2DPlus sprayGunTex = new Texture2DPlus(Content.Load<Texture2D>("parts/SprayGun"));
            Texture2DPlus chargingGunTex = new Texture2DPlus(Content.Load<Texture2D>("parts/ChargingGun"));
            Texture2DPlus gravityGunTex = new Texture2DPlus(Content.Load<Texture2D>("parts/GravityGun"));
            Texture2DPlus engineTex1 = new Texture2DPlus(Content.Load<Texture2D>("parts/Engine_1"));
            Texture2DPlus selectionBoxTex = new Texture2DPlus(Content.Load<Texture2D>("parts/SelectionBox"));
            Texture2DPlus upgradeBkg = new Texture2DPlus(Content.Load<Texture2D>("parts/UpgradeBarBkg"));
            Texture2DPlus popupTextBkgTex = new Texture2DPlus(Content.Load<Texture2D>("textures/PopupTextBkg"));
            Texture2DPlus menuScreenBkg = new Texture2DPlus(Content.Load<Texture2D>("parts/MenuScreenBkg"));
            Texture2DPlus rotateItemTex = new Texture2DPlus(Content.Load<Texture2D>("textures/Rotate"));
            Texture2DPlus hammerItemTex = new Texture2DPlus(Content.Load<Texture2D>("textures/Hammer"));
            Texture2DPlus alertTex = new Texture2DPlus(Content.Load<Texture2D>("textures/AlertParticle"));
            Texture2DPlus logoTex = new Texture2DPlus(Content.Load<Texture2D>("textures/logo"));

            //allUpgradeParts.Insert(PartTypes.DETECTORPART, shotTex);
            #endregion

            #region Loading sounds and adding them to list
            SoundHandler.Sounds[(int)IDs.DEFAULT] = Content.Load<SoundEffect>("sounds/shotSnd");
            SoundHandler.Sounds[(int)IDs.GUNPART] = Content.Load<SoundEffect>("sounds/shotSnd");
            SoundHandler.Sounds[(int)IDs.GRAVITYBULLET] = Content.Load<SoundEffect>("sounds/blackholeSnd");
            SoundHandler.Sounds[(int)IDs.MENUCLICK] = Content.Load<SoundEffect>("sounds/menuclickSnd");
            SoundHandler.Sounds[(int)IDs.CHARGINGGUNPART] = Content.Load<SoundEffect>("sounds/chargeshotSnd");
            SoundHandler.Sounds[(int)IDs.EXPLOSIONDEATHSOUND] = Content.Load<SoundEffect>("sounds/ExplosionDeath");
            SoundHandler.Sounds[(int)IDs.PLAYERDEATHSOUND] = Content.Load<SoundEffect>("sounds/player_death");
            SoundHandler.Sounds[(int)IDs.SLOWMOSOUND] = Content.Load<SoundEffect>("sounds/slomo");
            SoundHandler.Sounds[(int)IDs.REVERSESLOWMOSOUND] = Content.Load<SoundEffect>("sounds/reverse_slomo");
            SoundHandler.Sounds[(int)IDs.PICKUPSOUND] = Content.Load<SoundEffect>("sounds/pickup");
            SoundHandler.Songs[(int)IDs.SONG1INTRO] = Content.Load<SoundEffect>("sounds/OdysseanIntro");
            SoundHandler.Songs[(int)IDs.SONG1] = Content.Load<SoundEffect>("sounds/Odyssean");
            SoundHandler.Songs[(int)IDs.SONG2] = Content.Load<SoundEffect>("sounds/Galaxian");
            SoundHandler.Songs[(int)IDs.VICTORY] = Content.Load<SoundEffect>("sounds/Victorian");
            SoundHandler.Songs[(int)IDs.GAMEOVER] = Content.Load<SoundEffect>("sounds/GameOver");
            //Console.WriteLine(SoundHandler.Songs[(int)IDs.SONG1INTRO].Duration);
            #endregion
            //Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Content\\textures");

            #region Adding sprites to list
            SpriteHandler.Sprites[(int)IDs.DEFAULT] = new Sprite(errorTex);
            //SpriteHandler.Sprites[(int)IDs.DEFAULT_ENEMY] = new Sprite(enemyTex1, 2, 4);
            //SpriteHandler.Sprites[(int)IDs.ENEMYSHOOT] = new Sprite(enemyTex2, 2, 4);
            //SpriteHandler.Sprites[(int)IDs.ENEMYSPEED] = new Sprite(enemyTex3, 2, 4);
            SpriteHandler.Sprites[(int)IDs.ENEMYASTER] = new Sprite(enemyTex4);
            SpriteHandler.Sprites[(int)IDs.DEFAULT_BULLET] = new Sprite(shotTex,4);
            SpriteHandler.Sprites[(int)IDs.CHARGINGBULLET] = new Sprite(chargingBulletTex, 24);
            SpriteHandler.Sprites[(int)IDs.GRAVITYBULLET] = new Sprite(gravityBulletTex, 24);
            SpriteHandler.Sprites[(int)IDs.SPRAYBULLET] = new Sprite(sprayBulletTex, 2);
            SpriteHandler.Sprites[(int)IDs.HOMINGBULLET] = new Sprite(homingTex);
            SpriteHandler.Sprites[(int)IDs.HEALTHDROP] = new Sprite(healthDropTex,6);
            SpriteHandler.Sprites[(int)IDs.HEALTHDROP_TIER2] = new Sprite(healthDrop_TIER2_Tex, 6);
            SpriteHandler.Sprites[(int)IDs.EXPLOSIONDROP] = new Sprite(explosionDropTex,6);
            SpriteHandler.Sprites[(int)IDs.ENERGYDROP] = new Sprite(energyDropTex,6);
            SpriteHandler.Sprites[(int)IDs.MINEBULLET] = new Sprite(mineBulletTex, 6);
            SpriteHandler.Sprites[(int)IDs.RECTHULLPART] = new Sprite(shipTex);
            SpriteHandler.Sprites[(int)IDs.GUNPART] = new Sprite(gunTex1);
            SpriteHandler.Sprites[(int)IDs.MINEGUNPART] = new Sprite(mineGunTex);
            SpriteHandler.Sprites[(int)IDs.GRAVITYGUNPART] = new Sprite(gravityGunTex);
            SpriteHandler.Sprites[(int)IDs.CHARGINGGUNPART] = new Sprite(chargingGunTex);
            SpriteHandler.Sprites[(int)IDs.SPRAYGUNPART] = new Sprite(sprayGunTex);
            SpriteHandler.Sprites[(int)IDs.ENGINEPART] = new Sprite(engineTex1);
            SpriteHandler.Sprites[(int)IDs.DEFAULT_PARTICLE] = new Sprite();
            SpriteHandler.Sprites[(int)IDs.WALL] = new Sprite(wallTex);
            SpriteHandler.Sprites[(int)IDs.WRENCH] = new Sprite(wrenchTex);
            SpriteHandler.Sprites[(int)IDs.BOLT] = new Sprite(boltTex);
            SpriteHandler.Sprites[(int)IDs.EMPTYPART] = new Sprite(selectionBoxTex);
            SpriteHandler.Sprites[(int)IDs.UPGRADEBAR] = new Sprite(upgradeBkg);
            SpriteHandler.Sprites[(int)IDs.POPUPTEXTBKG] = new Sprite(popupTextBkgTex);
            SpriteHandler.Sprites[(int)IDs.MENUSCREENBKG] = new Sprite(menuScreenBkg);
            SpriteHandler.Sprites[(int)IDs.MONEYDROP] = new Sprite(moneyDropTex, 6);
            SpriteHandler.Sprites[(int)IDs.MONEY] = new Sprite(moneyTex);
            SpriteHandler.Sprites[(int)IDs.ROTATEPART] = new Sprite(rotateItemTex);
            SpriteHandler.Sprites[(int)IDs.HAMMERPART] = new Sprite(hammerItemTex);
            SpriteHandler.Sprites[(int)IDs.ALERTPARTICLE] = new Sprite(alertTex);
            SpriteHandler.Sprites[(int)IDs.LOGO] = new Sprite(logoTex);
            #endregion

            #region Adding partIDs to list
            List<IDs> ids = new List<IDs>();
            for (int i = (int)IDs.DEFAULT_PART+1; i <= (int)IDs.EMPTYPART; i++)
                ids.Add((IDs)i);
            ids.Add(IDs.ROTATEPART); //!!
            #endregion

            #region Initializing game objects etc.
            background = new Background(new Sprite(backgroundTex), bluePlanetTex, bluePlanetTex, bigRedTex, smallRedTex);            
            //Camera.Player = player; //Reintroduce if camera is to be used
            AchievementController achController = new AchievementController();
            SaveHandler.InitializeGame(achController);
            GameMode gameMode = new GameMode();
            Projectiles projectiles = new Projectiles();
            GunPart.projectiles = projectiles;

            Player player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), projectiles); //ok, projectiles null before?
            eventOperator = new EventOperator(this, shipTex, gameMode, achController, player, ids);
            RectangularHull rectHull1 = new RectangularHull();
            RectangularHull rectHull2 = new RectangularHull();
            GunPart gunPart1 = new ChargingGunPart();
            GunPart gunPart2 = new ChargingGunPart();
            GunPart gunPart3 = new GunPart(IDs.DEFAULT);
            EnginePart engine1 = new EnginePart();
            EnginePart engine2 = new EnginePart();
            EnginePart engine3 = new EnginePart();
            Drops drops = new Drops(WindowSize.Width, WindowSize.Height);
            gameController = new GameController(player, projectiles, drops, gameMode);
            //wall = new Wall(new Vector2(-4000, -4000)); //! wall location
            UnitBar healthBar = new UnitBar(new Vector2(50, 50), new Sprite(unitBarBorderTex), Color.OrangeRed, 5); //! LOL
            UnitBar energyBar = new UnitBar(new Vector2(50, 85), new Sprite(unitBarBorderTex), Color.Gold, player.maxEnergy);
            playerUI = new PlayerUI(healthBar, energyBar, achController);
            Mouse.SetCursor(MouseCursor.FromTexture2D(cursorTex.Texture, cursorTex.Texture.Width/2, cursorTex.Texture.Height /2));
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
            {
                UpdateGame(gameTime, false);
                playerUI.Update(gameTime, gameController.Player);
            }
            else
                eventOperator.Update(gameTime);
            Particles.Update(gameTime);
            CheckGameStatus(gameTime);          
            InputHandler.UpdatePreviousState();
            Camera.Update(gameTime);
            base.Update(gameTime);
            background.Update(gameTime);
        }

        public void UpdateGame(GameTime gameTime, bool cutScene)
        {
            #region Update           
            gameController.Update(gameTime, cutScene, eventOperator.NewGameState);
            
            Traits.TIME.Counter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            #endregion
        }

        private void CheckGameStatus(GameTime gameTime)
        {
            #region Game Over
            if (!gameController.Player.IsActive && eventOperator.GameState == EventOperator.GAME_STATE)
            {
                ResetGame(false);
                eventOperator.NewGameState = EventOperator.GAME_OVER_STATE;
                SoundHandler.PauseSong(); //rätt?
            }
            #endregion
            #region Pause
            if (InputHandler.isJustPressed(Keys.Escape) && eventOperator.GameState == EventOperator.GAME_STATE && eventOperator.NewGameState != EventOperator.CUT_SCENE_STATE && eventOperator.NewGameState != EventOperator.GAME_OVER_STATE)
            {
                eventOperator.NewGameState = EventOperator.PAUSE_STATE;
            }
            #endregion
            #region Upgrade Ship
            //if (InputHandler.isJustPressed(Keys.M) && eventOperator.GameState == EventOperator.GAME_STATE)
            //{
            //    eventOperator.NewGameState = EventOperator.UPGRADE_STATE;               
            //}
            #endregion
            #region Cut Scene
            if (EventOperator.TriggeredCutScene)
            {
                eventOperator.CutSceneType = GameMode.Level % 10;
                eventOperator.NewGameState = EventOperator.CUT_SCENE_STATE;               
                EventOperator.TriggeredCutScene = false;
                //boss finished cutScene
                if (GameMode.Level % 10 == 1) // kill asteroids
                    gameController.Enemies.Reset();
            }

            #endregion
        }

        public void ResetGame(bool fullReset)
        {
            if (fullReset)
            {               
                Particles.Reset();
                ScoreHandler.Reset();
                playerUI.Reset();             
            }           
            gameController.Reset(fullReset);
        }     

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (eventOperator.GameState != EventOperator.SPLASH_SCREEN_STATE)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null, Camera.CameraMatrix);
                background.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);
            if (eventOperator.GameState == EventOperator.GAME_STATE && eventOperator.NewGameState != EventOperator.CUT_SCENE_STATE && eventOperator.NewGameState != EventOperator.GAME_OVER_STATE)
            {               
                DrawGame(spriteBatch, gameTime, true);
                playerUI.Draw(spriteBatch, gameTime);
            }
            else
                eventOperator.Draw(spriteBatch, gameTime);

            //DebugMode(spriteBatch, gameTime);            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);
            if (eventOperator.GameState == EventOperator.GAME_STATE && eventOperator.NewGameState != EventOperator.CUT_SCENE_STATE && eventOperator.NewGameState != EventOperator.GAME_OVER_STATE) //this should be identical to drawgame condiftions
                playerUI.DrawBars(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawGame(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            #region Draw Game
            Particles.Draw(spriteBatch, gameTime);            
            //wall.Draw(spriteBatch, gameTime);
            gameController.Draw(spriteBatch, gameTime, fullDraw);           
            #endregion
        }        

        private void DebugMode(SpriteBatch spriteBatch, GameTime gameTime)
        {
            #region Remove oldStuff?
            //int controlSheme = Player.ControlScheme;
            //string usingControls = "";
            //if (controlSheme <= 1)
            //    usingControls = "WASD + Follow mouse";
            //if (controlSheme == 2)
            //    usingControls = "Absolute WASD";
            //if (controlSheme == 3)
            //    usingControls = "Mouse only";
            //if (controlSheme == 4) 
            //    usingControls = "WASD : AD = Rotate";            
            //spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),debugFont, "Player pos: " +player.Position, new Vector2(600, 100), Color.Yellow);
            //spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),scoreFont, "Controls: " + controlSheme + " - " + usingControls, new Vector2(WindowSize.Width - 700, WindowSize.Height - 50), Color.Crimson);

            //if (eventOperator.GameState != EventOperator.SPLASH_SCREEN_STATE)
            //    spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "FPS: " + (int)Math.Round(1/gameTime.ElapsedGameTime.TotalSeconds), new Vector2(50, WindowSize.Height - 50), Color.Gold);

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
            //spriteBatch.Draw(new Texture2DPlus(Content.Load<Texture2D>("textures/ship"),player.Hull.BoundBoxes[0].Position,cR, Color.Aqua, player.Hull.BoundBoxes[0].Angle, player.Hull.BoundBoxes[0].Origin, new Vector2(1,1),SpriteEffects.None,1);
            //spriteBatch.Draw(new Texture2DPlus(Content.Load<Texture2D>("textures/plus"), player.Hull.Parts[0].BoundBoxes[0].Position, cR2, Color.Red, player.Hull.Parts[0].BoundBoxes[0].Angle, player.Hull.Parts[0].BoundBoxes[0].Origin, new Vector2(1, 1), SpriteEffects.None, 1);

            //wall = new Wall(new Vector2(500, 500), new Sprite(new Texture2DPlus(Content.Load<Texture2D>("textures/wall")));
            //wall.Angle = (float)Math.PI / 3;
            //wall.Origin = new Vector2(-100, -100);
            //wall.Angle = (float)Math.PI*3/2;
            //wall.Draw(spriteBatch, gameTime);
            //spriteBatch.Draw(new Texture2DPlus(Content.Load<Texture2D>("textures/ship"), new Vector2(100,100), cR2, Color.Aqua, (float)Math.PI/2, new Vector2(cR2.Width/2,cR2.Height/2), new Vector2(1, 1), SpriteEffects.None, 1);
            #endregion
        }
    }
}
