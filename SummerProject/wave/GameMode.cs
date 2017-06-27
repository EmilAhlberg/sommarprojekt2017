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
        public const int DEBUG_MODE = 0;        
        public const int CONSTANT_TIME = 1;
        public const int DECREASING_TIME = 2;

        //Modes for the point generator
        public const int RANDOM_SINGLESPAWN = 10;
        public const int RANDOM_WAVESPAWN = 11;       

        public int TimeMode { get; set; }
        public int SpawnMode { get; set; }

        private int level = 1;
        private SpriteFont font;
        private int windowWidth;
        private int windowHeight;
        private Timer timer;
      

        public GameMode(SpriteFont font)
        {
            this.font = font;
            this.windowWidth = WindowSize.Width;
            this.windowHeight = WindowSize.Height;

            timer = new Timer(3); //!
            
        }

        public void Reset()
        {
            TimeMode = CONSTANT_TIME;      //DEFAULT GAME MODE
            SpawnMode = RANDOM_WAVESPAWN;
        }

        public void ProgressGame(int level)
        {
            this.level = level;
            timer.Reset();
        }

        public void Update(GameTime gameTime)
        {
            timer.CountDown(gameTime);
            UpdateMode();
            
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!timer.IsFinished)
            {
                String s = "Wave: " + level;
                spriteBatch.DrawString(font, s, WordLayoutPosition(s), Color.Gold);
            }
        }

        private void UpdateMode()
        {
            if (InputHandler.isJustPressed(Keys.F1))
            {
                TimeMode = GameMode.DECREASING_TIME;
                SpawnMode = GameMode.RANDOM_SINGLESPAWN;        
            }

            if (InputHandler.isJustPressed(Keys.F2))
            {
                TimeMode = GameMode.RANDOM_WAVESPAWN;
                SpawnMode = GameMode.RANDOM_WAVESPAWN;         
            }

            if (InputHandler.isJustPressed(Keys.F3))
            {
                TimeMode = GameMode.DEBUG_MODE;              
            }
        }


        //duplicated in AnimatedEventHandler
        private Vector2 WordLayoutPosition(String s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            if (size.X > width)
                width = size.X;
            return new Vector2((windowWidth - width) / 2, windowHeight / 2);
        }
    }
}
