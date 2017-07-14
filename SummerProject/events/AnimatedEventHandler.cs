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

namespace SummerProject.framework
{
    class AnimatedEventHandler
    {
        public bool AnimatedEvent { get; set; }
        

        public static readonly string[] COUNTDOWN = { "GO!", "READY!", "" };    
        public const float EVENTTIME = 2f;
        public const float STATSTIME = 100f;
        public const float SPLASHTIME = 3f;
        public Timer eventTimer;
        private Game1 game;
        private EventOperator op;
        private SpriteFont font;
        private GameMode gameMode;

        public AnimatedEventHandler(Game1 game, EventOperator op, SpriteFont font, GameMode gameMode)
        {
            this.gameMode = gameMode;
            this.game = game;
            this.op = op;
            this.font = font;
            eventTimer = new Timer(EVENTTIME);
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
                    if (op.GameState == EventOperator.SPLASH_SCREEN_STATE)
                    {
                        string s = "Dogs Don't Judge presents..."; //!
                        Sprite logo = SpriteHandler.GetSprite((int)IDs.LOGO);
                        logo.Scale *= 0.5f;
                        logo.Position = new Vector2(WindowSize.Width / 2, WindowSize.Height / 2);
                        logo.Origin = new Vector2(logo.SpriteRect.Width / 2, logo.SpriteRect.Height / 2);
                        logo.Draw(spriteBatch, gameTime);
                        spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, s, DrawHelper.CenteredWordPosition(s, font), Color.Gold);
                    }
                    else
                    {
                        op.ResetGame(false);
                        game.UpdateGame(gameTime);
                        game.DrawGame(spriteBatch, gameTime, false);
                        string s = "Mediocre!"; //!
                        spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, s, DrawHelper.CenteredWordPosition(s, font), Color.Gold);
                    }                              
                    break;
                case EventOperator.GAME_STATE:
                    //op.ResetGame(true);
                    game.DrawGame(spriteBatch, gameTime, false);
                    DrawCountDown(spriteBatch, gameTime);
                    break;
                case EventOperator.GAME_OVER_STATE:
                    DrawStats(spriteBatch, gameTime);
                    if (InputHandler.isJustPressed(MouseButton.LEFT))
                        eventTimer = new Timer(0);
                    break;
            }
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
            Vector2 location = new Vector2(WindowSize.Width/2, height / 2 - 200); //!

            for (int i = 0; i < STATS.Length; i++)
            {
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, STATS[i], location, Color.Gold, 0, font.MeasureString(STATS[i]) / 2, 1);
                location.Y += font.LineSpacing;
            }
        }

        private void DrawCountDown(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string word = "";
            //if (GameMode.Level == 0)
            //{
                word = COUNTDOWN[(int)eventTimer.currentTime];
            //}
            //else
            //{
            //    if ((int)eventTimer.currentTime == 1)
            //        word = "Wave: " + GameMode.Level;
            //    else
            //        word = "GO!";
            //}

            //string word = COUNTDOWN[(int)eventTimer.currentTime];
            Color color = Color.Gold;
            if ((int)eventTimer.currentTime == 0)
                color = Color.OrangeRed;
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, word, DrawHelper.CenteredWordPosition(word, font), color);
        }

        internal void Reset()
        {
            eventTimer.Reset();
        }
    }
}
