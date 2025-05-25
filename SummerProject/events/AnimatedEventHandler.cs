using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.wave;
using SummerProject.factories;
using Microsoft.Xna.Framework.Input;
using SummerProject.events;

namespace SummerProject.framework
{
    class AnimatedEventHandler
    {
        public bool AnimatedEvent { get; set; }
        public const int BOSSFINISHED_TYPE = 1;
        public const int BOSSAPPEARANCE_TYPE = 2;
        

        public static readonly string[] COUNTDOWN = { "GO!", "READY!", "" };    
        public const float COUNTDOWNTIME = 2f;
        public const float STATSTIME = 1000f;
        public const float SPLASHTIME = 9.665f; //Length of intro theme
        public const float BOSSFINISHED_TIME = 10.1f; //Length of victory theme
        public const float BOSSAPPEARANCE_TIME = 7;
        public const float DEATHTIME = 3f;       
        public Timer eventTimer;

        private BossAppearance boss;
        private Game1 game;
        private EventOperator op;
        private SpriteFont font;
        private GameMode gameMode;
        private Sprite logo;

        public AnimatedEventHandler(Game1 game, EventOperator op, GameMode gameMode)
        {
            this.gameMode = gameMode;
            this.game = game;
            this.op = op;
            this.font = DrawHelper.BIGFONT;
            boss = new BossAppearance();
            eventTimer = new Timer(COUNTDOWNTIME);
            logo = SpriteHandler.GetSprite((int)IDs.LOGO);
            logo.Position = new Vector2(WindowSize.Width / 2, WindowSize.Height / 2);
            logo.Origin = new Vector2(logo.SpriteRect.Width / 2, logo.SpriteRect.Height / 2);

        }

        public bool UpdateEventTimer(GameTime gameTime)
        {
            eventTimer.CountDown(gameTime);
            if (eventTimer.IsFinished)
            {                
                eventTimer.Reset();
                return true;      
            }
            return false;
        }   

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (op.NewGameState)
            {
                case EventOperator.MENU_STATE:
                    #region SplashScreen 
                    if (op.GameState == EventOperator.SPLASH_SCREEN_STATE)
                    {
                        if (InputHandler.isJustPressed(MouseButton.LEFT))
                            eventTimer.Finish();
                        else
                        {
                            int alphaChannel = (int)(455 * (SPLASHTIME - eventTimer.currentTime) / SPLASHTIME) - 100;
                            logo.MColor = new Color(255, 255, 255, alphaChannel);
                            logo.Draw(spriteBatch, gameTime);
                        } 
                    }
                    #endregion
                    #region GiveUp
                    else
                    {
                        op.ResetGame(false);
                        game.UpdateGame(gameTime, false);
                        game.DrawGame(spriteBatch, gameTime, false);
                        string s = "Mediocre!"; //!
                        spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, s, DrawHelper.CenteredWordPosition(s, font), Color.Gold);
                    }
                    #endregion
                    break;
                case EventOperator.GAME_STATE:
                    #region CountDown
                    game.DrawGame(spriteBatch, gameTime, false);
                    DrawCountDown(spriteBatch, gameTime);
                       #endregion
                    break;
                case EventOperator.GAME_OVER_STATE:
                    #region GameOver Animation
                    if (eventTimer.currentTime > STATSTIME)
                    {
                        game.UpdateGame(gameTime, false);
                        game.DrawGame(spriteBatch, gameTime, false);
                        Vector2 shitvect = new Vector2(WindowSize.Width / 2 - font.MeasureString("GAME OVER").X / 2, WindowSize.Height / 2 - font.MeasureString("GAME OVER").Y / 2);      //previously bigFont in Game1        
                        spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, "GAME OVER", shitvect, Color.OrangeRed);
                    }
                    #endregion
                    #region DrawStats
                    else
                    {
                        SoundHandler.PlaySong((int)IDs.GAMEOVER);
                        DrawStats(spriteBatch, gameTime);
                        if (InputHandler.isJustPressed(MouseButton.LEFT))
                            eventTimer = new Timer(0);
                    }
                    #endregion
                    break;
                case EventOperator.CUT_SCENE_STATE:
                    #region Cutscenes
                    switch (op.CutSceneType)
                    {                       
                        case 1:

                            BossFinishedScene(spriteBatch, gameTime);
                            break;
                        case 2:
                            BossAppearanceScene(spriteBatch, gameTime);     
                            break;
                    }
                    #endregion
                    break;                  
            }
        }

        /*
         * Specific animation/text-drawing methods below.
         */

        private void BossAppearanceScene(SpriteBatch spriteBatch, GameTime gameTime)
        {            
            float dX = 0;
            float slideTime = 1.5f;
            float slideSpeed = 1.5f;

            if (eventTimer.currentTime > BOSSAPPEARANCE_TIME - slideTime)
                dX = slideSpeed;
            else if (eventTimer.currentTime < slideTime + 0.5f)
                dX = -slideSpeed;

            //temp
            if (dX == 0)
            {                
                Vector2 shitvect = new Vector2(WindowSize.Width / 2 - font.MeasureString("RABBLE RABBLE").X / 2, WindowSize.Height / 2 - font.MeasureString("RABBLE RABBLE").Y / 2);
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, "RABBLE RABBLE", shitvect, Color.OrangeRed);
            }        

            boss.Update(gameTime, dX);

            game.gameController.Player.Update(gameTime);
            game.UpdateGame(gameTime, true);
            game.DrawGame(spriteBatch, gameTime, false);
            boss.Draw(spriteBatch, gameTime); //happens after drawGame!
        }

        private void BossFinishedScene(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Vector2 playerTarget = new Vector2(WindowSize.Width / 2, WindowSize.Height / 2);
            bool cutScene;
            if (eventTimer.currentTime > BOSSFINISHED_TIME - 5) //!
            {
                cutScene = false;
                BossFinishedMessage(spriteBatch);
                //kills drops and asteroids here?
            }
            else
            {
                cutScene = true;
                if (eventTimer.currentTime < BOSSFINISHED_TIME - 6) //!
                { //!
                    playerTarget = new Vector2(10000, WindowSize.Height / 2);
                    game.gameController.Player.AddForce(10, 0); //!
                }
                game.gameController.Player.MoveTowardsPoint(playerTarget);
            }
            game.UpdateGame(gameTime, cutScene);
            game.DrawGame(spriteBatch, gameTime, false);          
        }

        private void BossFinishedMessage(SpriteBatch spriteBatch)
        {
            string word = "Good job!";F
            Vector2 location = new Vector2(WindowSize.Width / 2, WindowSize.Height / 2 - 200); //!
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word, location, Color.Gold, 0, font.MeasureString(word) / 2, 1);

        }

        private void DrawStats(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float divByZeroFix = Traits.SHOTSFIRED.Counter;
            if (Traits.SHOTSFIRED.Counter == 0)            
                divByZeroFix = 1;
            
            string[] STATS = {  "You're dead!", "", "Score: " + ScoreHandler.Score, "Shots Fired: " + Traits.SHOTSFIRED.Counter,
                                "Accuracy: " + Math.Round((Traits.SHOTSHIT.Counter / divByZeroFix)*100, 2) + "%",
                                "Cash Collected: " + Traits.CURRENCY.Counter + "$",
                                "Time Elapsed: " + Math.Round(Traits.TIME.Counter, 2) + "s","", "Left click to continue!"};
            float height = STATS.Length;
            height *= font.LineSpacing;
            Vector2 location = new Vector2(WindowSize.Width/2, WindowSize.Height / 2 - 300); //!

            for (int i = 0; i < STATS.Length; i++)
            {
                Color c = Color.Gold;
                float time = ((int)eventTimer.currentTime) % 2;
                if (i == 0)
                    c = Color.DarkRed;
                else if (i == STATS.Length -1 &&  time % 2 == 1)
                    c = Color.OrangeRed;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, STATS[i], location,c, 0, font.MeasureString(STATS[i]) / 2, 1);
                location.Y += font.LineSpacing;
            }
        }

        private void DrawCountDown(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string word = "";
                word = COUNTDOWN[(int)eventTimer.currentTime];
           
            Color color = Color.Gold;
            if ((int)eventTimer.currentTime == 0)
                color = Color.OrangeRed;
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, word, DrawHelper.CenteredWordPosition(word, font), color);
        }
    }
}
