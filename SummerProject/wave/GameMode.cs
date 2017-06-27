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

        public int TimeMode { get; set; }
        public int SpawnMode { get; set; }
        private bool removeFrameFix; //hack solution to wave# + ready/go overlap

        public bool ChangeLevel { get; private set; }
        public int Level { get; private set; } 
        private SpriteFont font;     
        public Timer BetweenLevelsTimer { get; private set; }
      

        public GameMode(SpriteFont font)
        {
            this.font = font;
          

            BetweenLevelsTimer = new Timer(3); //!         
        }

        public void Reset(bool fullReset)
        {
            TimeMode = BURST_TIME;      //DEFAULT GAME MODE
            SpawnMode = RANDOM_WAVE;
            Level = 1; //!
            ChangeLevel = true;
            if (fullReset)
            {
                BetweenLevelsTimer.Reset();
                removeFrameFix = true;
            }
        }

        private void ProgressGame()
        {
            int newLevel = ScoreHandler.Score / (Level * 500);
            if (newLevel > Level)
            {
                Level = newLevel;
                ChangeLevel = true;
                BetweenLevelsTimer.Reset();           
            }        
            
        }

        public void Update(GameTime gameTime)
        {
            ChangeLevel = false;
            BetweenLevelsTimer.CountDown(gameTime);
            UpdateMode();
            ProgressGame();
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!BetweenLevelsTimer.IsFinished && !removeFrameFix)
            {
                String s = "Wave: " + Level;
                spriteBatch.DrawString(font, s, WordLayoutPosition(s), Color.Gold);
            }
            removeFrameFix = false;
        }

        private void UpdateMode()
        {
            if (InputHandler.isJustPressed(Keys.F1))
            {
                TimeMode = GameMode.DECREASING_TIME;
                SpawnMode = GameMode.RANDOM_SINGLE;        
            }

            if (InputHandler.isJustPressed(Keys.F2))
            {
                TimeMode = GameMode.RANDOM_WAVE;
                SpawnMode = GameMode.RANDOM_WAVE;         
            }

            if (InputHandler.isJustPressed(Keys.F3))
            {
                TimeMode = GameMode.DEBUG_TIME;              
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
