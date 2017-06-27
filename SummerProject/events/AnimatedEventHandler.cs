using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.framework
{
    class AnimatedEventHandler
    {
        public bool AnimatedEvent { get; set; }
        public static readonly string[] COUNTDOWN = { "GO!", "READY!", "" };

        private const float eventTime = 2f;
        private Timer eventTimer;
        private Game1 game;
        private EventOperator op;
        private SpriteFont font;

        public AnimatedEventHandler(Game1 game, EventOperator op, SpriteFont font)
        {
            this.game = game;
            this.op = op;
            this.font = font;
            eventTimer = new Timer(eventTime);
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

        internal void Reset()
        {
            eventTimer.Reset();
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (op.NewGameState)
            {
                case EventOperator.MENU_STATE:
                    op.ResetGame(false);
                    game.UpdateGame(gameTime);
                    game.DrawGame(spriteBatch, gameTime);
                    string s = "Mediocre!"; //!
                    spriteBatch.DrawString(font, s, WordLayoutPosition(s), Color.Gold);
                    System.Threading.Thread.Sleep(40); //! slow mo of doom                                          
                    break;
                case EventOperator.GAME_STATE:
                    op.ResetGame(true);
                    game.DrawGame(spriteBatch, gameTime); //WHY WONT PLAYER ALWAYS DRAAAW?1111111111
                    DrawCountDown(spriteBatch, gameTime);
                    break;
                case EventOperator.GAME_OVER_STATE:
                    string score = "Score: " + ScoreHandler.Score; //!
                    spriteBatch.DrawString(font, score, WordLayoutPosition(score), Color.Gold);
                    break;
            }
        }

        private void DrawCountDown(SpriteBatch spriteBatch, GameTime gameTime)
        {
            String word = COUNTDOWN[(int)eventTimer.currentTime];
            Color color = Color.Gold;
            if ((int)eventTimer.currentTime == 0)
                color = Color.OrangeRed;
            spriteBatch.DrawString(font, word, WordLayoutPosition(word), color);
        }

        private Vector2 WordLayoutPosition(string s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            if (size.X > width)
                width = size.X;
            return new Vector2((WindowSize.Width - width) / 2, (WindowSize.Height - 0) / 2);
        }
    }
}
