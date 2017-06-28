using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.util;
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
        //public const int DEBUG_TIME = 0;        
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
        public bool LevelFinished { get; internal set; }

        private SpriteFont font;
        private Timer betweenLevelsTimer;
      

        public GameMode(SpriteFont font)
        {
            this.font = font;
            TimeMode = DECREASING_TIME; //Default game mode. Change pressedIndex in ModeSelectionMenu if changed
            SpawnMode = RANDOM_SINGLE;
            BetweenLevelsTimer = new Timer(3); //!         
        }

        public void Reset(bool fullReset)
        {           
            Level = 1; //! ..
            IsChanged = true;
            if (fullReset)
            {
                betweenLevelsTimer.Reset();
            }
        }

        private void ProgressGame()
        {
            //int newLevel = ScoreHandler.Score / (Level * 500); //!
            //if (newLevel > Level)
            //{
            //    Level = newLevel;
            //    IsChanged = true;
            //    betweenLevelsTimer.Reset();           
            //}          
            if (LevelFinished)
            {
                Level += 1;
                IsChanged = true;
                betweenLevelsTimer.Reset();
                LevelFinished = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            IsChanged = false;
            betweenLevelsTimer.CountDown(gameTime);
            ProgressGame();
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool fullDraw)
        {
            if (!betweenLevelsTimer.IsFinished && fullDraw)
            {
                string s = "Wave: " + Level;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, s, WordLayoutPosition(s), Color.Gold);
            }
        }

        //duplicated in AnimatedEventHandler
        private Vector2 WordLayoutPosition(string s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            if (size.X > width)
                width = size.X;
            return new Vector2((WindowSize.Width - width) / 2, WindowSize.Height / 2);
        }
    }
}
