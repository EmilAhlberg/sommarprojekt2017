using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class GameMode
    { 
        //Modes for the timer
        public const int DEBUG_TIME = 0;        
        public const int CONSTANT_TIME = 1;
        public const int DECREASING_TIME = 2;
        public const int BURST_TIME = 3;

        //Modes for the point generator
        public const int RANDOM_SINGLE = 10;
        public const int RANDOM_WAVE = 11;
        public const int BURST_WAVE = 12;

        public const int BURST_WAVE_INIT = 3;

        public int TimeMode { get; set; }
        public int SpawnMode { get; set; }

        public bool IsChanged { get; set; }
        public int Level { get; private set; } 
        private SpriteFont font;     
        public Timer BetweenLevelsTimer { get; private set; }
      

        public GameMode(SpriteFont font)
        {
            this.font = font;
            TimeMode = BURST_TIME;      //DEFAULT GAME MODE
            SpawnMode = BURST_WAVE;
            BetweenLevelsTimer = new Timer(3); //!         
        }

        public void Reset(bool fullReset)
        {           
            Level = 1; //!
            IsChanged = true;
            if (fullReset)
            {
                BetweenLevelsTimer.Reset();
            }
        }

        private void ProgressGame()
        {
            int newLevel = ScoreHandler.Score / (Level * 500); //!
            if (newLevel > Level)
            {
                Level = newLevel;
                IsChanged = true;
                BetweenLevelsTimer.Reset();           
            }                        
        }

        public void Update(GameTime gameTime)
        {
            IsChanged = false;
            BetweenLevelsTimer.CountDown(gameTime);
            ProgressGame();
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            if (!BetweenLevelsTimer.IsFinished && fullDraw)
            {
                String s = "Wave: " + Level;
                spriteBatch.DrawString(font, s, WordLayoutPosition(s), Color.Gold);
            }
        }

        //duplicated in AnimatedEventHandler
        private Vector2 WordLayoutPosition(String s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            if (size.X > width)
                width = size.X;
            return new Vector2((WindowSize.Width - width) / 2, WindowSize.Height / 2);
        }
    }
}
