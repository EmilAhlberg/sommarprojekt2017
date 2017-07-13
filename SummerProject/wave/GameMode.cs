﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.achievements;
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
        public const int CONSTANT_TIME = 1;
        public const int DECREASING_TIME = 2;
        public const int BURST_TIME = 3;

        //Modes for the point generator
        public const int RANDOM_SINGLE = 10;
        public const int RANDOM_WAVE = 11;
        public const int BURST_WAVE = 12;        

        public int TimeMode { get; set; }
        public int SpawnMode { get; set; }
        public bool IsChanged { get; set; }
        public static int Level { get; private set; }
        public bool LevelFinished { get; internal set; }
        private bool progressNow;
        private Difficulty difficulty;
        private SpriteFont font;
        public Timer BetweenLevelsTimer;
      

        public GameMode(SpriteFont font)
        {
            this.font = font;
            TimeMode = DECREASING_TIME; //Default game mode. Change pressedIndex in ModeSelectionMenu if changed
            SpawnMode = RANDOM_SINGLE;
            difficulty = new Difficulty();
            BetweenLevelsTimer = new Timer(3); //!         
            //BetweenLevelsTimer.CountDown(new GameTime());
        }

        public void Reset(bool fullReset)
        {
            Level = 0;
            //UpdateLevelSettings();
            //IsChanged = true;
            //progressNow = false;
            LevelFinished = true;           

            //if (fullReset)
            //{
            //    BetweenLevelsTimer.Reset();
            //}
            //Level = 1;
            //UpdateLevelSettings();
            //IsChanged = true;
            //progressNow = false;
            //if (fullReset)
            //{
            //    BetweenLevelsTimer.Reset();
            //}
        }

        private void ProgressGame()
        {         
            if (LevelFinished && !progressNow && BetweenLevelsTimer.IsFinished)
            {
                Level += 1;
                BetweenLevelsTimer.Reset();
                progressNow = true;
            } else if (LevelFinished && progressNow && BetweenLevelsTimer.IsFinished)
            {
                progressNow = false;
                LevelFinished = false;
                
                Traits.LEVEL.Counter++;
                IsChanged = true;
                UpdateLevelSettings();                
            }
        }
        //level 0??
        private void UpdateLevelSettings()
        {
            switch (Level%10)
            {
                case 1:
                    TimeMode = CONSTANT_TIME;
                    SpawnMode = RANDOM_SINGLE;
                    break;
                case 2:
                    TimeMode = DECREASING_TIME;
                    SpawnMode = RANDOM_SINGLE;
                    break;
                case 3:
                    TimeMode = DECREASING_TIME;
                    SpawnMode = RANDOM_SINGLE;
                    break;
                case 4:
                    TimeMode = CONSTANT_TIME;
                    SpawnMode = RANDOM_WAVE;
                    break;
                case 5:
                    TimeMode = CONSTANT_TIME;
                    SpawnMode = RANDOM_WAVE;
                    break;
                case 6:
                    TimeMode = CONSTANT_TIME;
                    SpawnMode = RANDOM_WAVE;
                    break;
                case 7:
                    TimeMode = CONSTANT_TIME;
                    SpawnMode = RANDOM_WAVE;
                    break;
                case 8:
                    TimeMode = BURST_TIME;
                    SpawnMode = BURST_WAVE;
                    break;
                case 9:
                    TimeMode = BURST_TIME;
                    SpawnMode = BURST_WAVE;
                    break;
                case 0:
                    //boss!
                    break;
            }
        }

        public void ChangeDifficulty(int newDifficulty)
        {
            difficulty.ChangeDifficulty(newDifficulty);
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
                string s = "";
                if (Level == 0)
                    s = "It's a trap!"; //"hack"
                else
                    s = "Wave: " + Level;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32),font, s, DrawHelper.CenteredWordPosition(s, font), Color.Gold);
            }
        }      
    }
}
