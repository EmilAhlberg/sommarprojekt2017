using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PhysicsSimulator;
using SummerProject.achievements;
using SummerProject.collidables;
using SummerProject.factories;
using SummerProject.framework;
using SummerProject.menu;
using SummerProject.wave;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public class EventOperator
    {
        public const int EXIT = 0;
        public const int MENU_STATE = 1;
        public const int GAME_STATE = 2;
        public const int GAME_OVER_STATE = 3;
        public const int PAUSE_STATE = 4;
        public const int UPGRADE_STATE = 5;
        public const int SPLASH_SCREEN_STATE = 6;
        public const int CUT_SCENE_STATE = 7;

        private int gameState;
        public int GameState { get { return gameState; } set
            {
                gameState = value;
                switch (value)
                {
                    case EXIT:
                        break;
                    case GAME_STATE:
                        SoundHandler.PlaySong((int)IDs.SONG2);
                        break;
                    case MENU_STATE:
                        SoundHandler.PlaySong((int)IDs.SONG1);
                        break;
                    case GAME_OVER_STATE:
                        break;
                    case PAUSE_STATE:
                        break;
                    case UPGRADE_STATE:
                        break;
                    case CUT_SCENE_STATE:
                        break;
                    case SPLASH_SCREEN_STATE:
                        SoundHandler.PlaySong((int)IDs.SONG1INTRO);
                        break;

                }
            }
        }

        private int newGameState;
        public int NewGameState
        {
            get { return newGameState; }
            set
            {
                newGameState = value;
                switch (value)
                {
                    case EXIT:
                        break;
                    case GAME_STATE:
                        if(SoundHandler.CurrentSongID != IDs.SONG2)
                            SoundHandler.PauseSong();
                        break;
                    case MENU_STATE:
                        break;
                    case GAME_OVER_STATE:
                        break;
                    case PAUSE_STATE:
                        break;
                    case UPGRADE_STATE:
                        SoundHandler.PlaySong((int)IDs.SONG2);
                        break;
                    case CUT_SCENE_STATE:
                        if (CutSceneType == AnimatedEventHandler.BOSSFINISHED_TYPE)
                            SoundHandler.PlaySong((int)IDs.VICTORY);
                        break;
                    case SPLASH_SCREEN_STATE:
                        break;
                }
            }
        }

        public AchievementController achControl { get; private set; }
        public GameMode GameMode { get; private set; }
        public int CutSceneType { get; internal set; }
        public static bool TriggeredCutScene { get; set; }

        private AnimatedEventHandler animatedHandler;
        private Menu menu;
        public UpgradeView UpgradeView;
        private Game1 game;

        public EventOperator(Game1 game, Texture2DPlus upgradeViewText, GameMode gameMode, AchievementController achControl, Player player, List<IDs> upgradePartsIDs)
        {            
            GameState = SPLASH_SCREEN_STATE;    //!! initial gamestate
            NewGameState = MENU_STATE;
            GameMode = gameMode;
            animatedHandler = new AnimatedEventHandler(game, this, gameMode);
            UpgradeView = new UpgradeView(upgradeViewText.Texture, player, upgradePartsIDs);            
            menu = new Menu(new Vector2(WindowSize.Width / 2,
                    WindowSize.Height / 2));
            this.game = game;
            this.achControl = achControl;
        }

        public void Update(GameTime gameTime)
        {
            if (animatedHandler.AnimatedEvent && animatedHandler.UpdateEventTimer(gameTime))
                FinishEvent();
            else if (!animatedHandler.AnimatedEvent)
            {
                ActivateEvent(gameTime);
                UpdateState(gameTime);
            }         
        }

        public void ActivateEvent(GameTime gameTime)
        {
            if (NewGameState != GameState)
            {
                float eventTime = 0;
                switch (NewGameState)
                {
                    case EXIT:
                        GameState = NewGameState;
                        break;
                    case GAME_STATE:
                        eventTime = AnimatedEventHandler.COUNTDOWNTIME;
                        animatedHandler.AnimatedEvent = true;                                                
                        break;
                    case MENU_STATE:
                        if (GameState == SPLASH_SCREEN_STATE)
                        {
                            eventTime = AnimatedEventHandler.SPLASHTIME;
                            game.IsMouseVisible = false;
                        }                        
                                                 
                        else                        
                            eventTime = AnimatedEventHandler.COUNTDOWNTIME;
                        animatedHandler.AnimatedEvent = true;                        
                        break;
                    case GAME_OVER_STATE:
                        eventTime = AnimatedEventHandler.DEATHTIME + AnimatedEventHandler.STATSTIME;                    
                        animatedHandler.AnimatedEvent = true;                      
                        break;
                    case PAUSE_STATE:
                        GameState = NewGameState;
                        break;
                    case UPGRADE_STATE:                        
                        GameState = NewGameState;
                        break;
                    case CUT_SCENE_STATE:
                        if (CutSceneType == AnimatedEventHandler.BOSSFINISHED_TYPE)
                        {
                            eventTime = AnimatedEventHandler.BOSSFINISHED_TIME;
                            
                        }
                            
                        else if (CutSceneType == AnimatedEventHandler.BOSSAPPEARANCE_TYPE)
                            eventTime = AnimatedEventHandler.BOSSAPPEARANCE_TIME;
                        animatedHandler.AnimatedEvent = true;
                        break;
                }
                animatedHandler.eventTimer = new Timer(eventTime);
            }
        }
        
        private void UpdateState(GameTime gameTime)
        {
            switch (GameState)
            {
                case EXIT:
                    ExitGame();                    
                    break;
                case MENU_STATE:
                    break;
                case GAME_OVER_STATE:
                    menu.CurrentMenu = MenuConstants.GAME_OVER;                   
                    break;
                case GAME_STATE:     
                    if (NewGameState != CUT_SCENE_STATE)
                        menu.CurrentMenu = MenuConstants.MAIN;                
                    break;
                case PAUSE_STATE:
                    if (NewGameState != EventOperator.MENU_STATE)
                        menu.CurrentMenu = MenuConstants.PAUSE;
                    if (InputHandler.isJustPressed(Keys.Escape))
                        NewGameState = EventOperator.GAME_STATE;
                    break;
                case UPGRADE_STATE:
                    menu.CurrentMenu = MenuConstants.UPGRADE;
                    UpgradeView.Update(gameTime);
                    break;
            }
            menu.Update(gameTime, this);
        }

        private void ExitGame()
        {
            SaveData data = new SaveData();
            data.SaveProgress(achControl);
            SaveHandler.Save(data, "save_file"); //!
            game.Exit();
        }

        private void FinishEvent()
        {
            switch (NewGameState)
            {
                case EventOperator.MENU_STATE:
                    if (gameState == SPLASH_SCREEN_STATE)
                        game.IsMouseVisible = true;
                    game.ResetGame(true);
                    menu.CurrentMenu = MenuConstants.MAIN;
                    break;             
                case EventOperator.CUT_SCENE_STATE:
                    if (CutSceneType == AnimatedEventHandler.BOSSFINISHED_TYPE)
                    {
                        NewGameState = UPGRADE_STATE;
                        game.gameController.Player.Position = new Vector2(10, WindowSize.Height / 2);
                        game.gameController.Drops.Reset();
                        //game.Player.Stop();
                    } else if (CutSceneType == AnimatedEventHandler.BOSSAPPEARANCE_TYPE)                    
                        NewGameState = GAME_STATE;                    
                    break;
            }
            animatedHandler.AnimatedEvent = false;
            GameState = NewGameState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //refactor much?
            if (animatedHandler.AnimatedEvent)
                animatedHandler.Draw(spriteBatch, gameTime);
            else
            {
                if (GameState == PAUSE_STATE || GameState == UPGRADE_STATE)
                    game.DrawGame(spriteBatch, gameTime, false);

                if (GameState == UPGRADE_STATE)
                    UpgradeView.Draw(spriteBatch, gameTime);             

                if (!animatedHandler.AnimatedEvent && NewGameState != CUT_SCENE_STATE && NewGameState != GAME_OVER_STATE || (NewGameState == GAME_OVER_STATE && NewGameState == GameState)) //removes all cases of 1 frame menu flimmer            
                    menu.Draw(spriteBatch, gameTime);                      
            }
        }

        public void ResetGame(bool fullReset)
        {
            game.ResetGame(fullReset);
            if (fullReset)
                UpgradeView.Reset();
        }
    }
}
