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

namespace SummerProject.framework
{
    class AnimatedEventHandler
    {
        public bool AnimatedEvent { get; set; }
        public static readonly string[] COUNTDOWN = { "GO!", "READY!", "" };    
        public const float EVENTTIME = 2f;
        public const float STATSTIME = 100f;
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
                    op.ResetGame(false);
                    game.UpdateGame(gameTime);
                    game.DrawGame(spriteBatch, gameTime, false);
                    string s = "Mediocre!"; //!
                    spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, s, DrawHelper.CenteredWordPosition(s, font), Color.Gold);
                    System.Threading.Thread.Sleep(40); //! slow mo of doom                                          
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
            string[] STATS = {  "You're dead!", "Score: " + ScoreHandler.Score, "Shots Fired: " + Traits.SHOTSFIRED.Counter,
                                "Shots Hit Ratio: " + Traits.SHOTSHIT.Counter,// / Traits.ShotsFiredTrait.Counter, <-- possible division by zero
                                "Time Elapsed " + Traits.TIME.Counter,"", "Left click to continue!"};
            float height = STATS.Length;
            height *= font.LineSpacing;
            Vector2 location = new Vector2(WindowSize.Width/2, height / 2);

            for (int i = 0; i < STATS.Length; i++)
            {
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, STATS[i], location, Color.Gold, 0, font.MeasureString(STATS[i]) / 2, 1);
                location.Y += font.LineSpacing;
            }
        }

        private void DrawCountDown(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string word = "";
            if (GameMode.Level == 0)
            {
                word = COUNTDOWN[(int)eventTimer.currentTime];
            }
            else
            {
                if ((int)eventTimer.currentTime == 1)
                    word = "Wave: " + GameMode.Level;
                else
                    word = "GO!";
            }

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
