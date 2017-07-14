using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.achievements;
using SummerProject.collidables;
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

        public int GameState { get; set; } 
        public int NewGameState { get; set; }      
        public AchievementController achControl { get; private set; }
        public GameMode GameMode { get; private set; }
        private AnimatedEventHandler animatedHandler;
        private Menu menu;
        public UpgradeView upgradeView;
        private Game1 game;

        public EventOperator(SpriteFont font,SpriteFont upgradeFont, Game1 game, Texture2DPlus upgradeViewText, GameMode gameMode, AchievementController achControl, Player player, List<IDs> upgradePartsIDs)
        {            
            GameState = SPLASH_SCREEN_STATE;    //!!!!!
            NewGameState = MENU_STATE;
            GameMode = gameMode;
            animatedHandler = new AnimatedEventHandler(game, this, font, gameMode);
            upgradeView = new UpgradeView(upgradeViewText.Texture, upgradeFont, player, upgradePartsIDs);            
            menu = new Menu(new Vector2(WindowSize.Width / 2,
                    WindowSize.Height / 2), font);
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
                //animatedHandler.Reset(); //May want to set this differently in different cases.
                switch (NewGameState)
                {
                    case EXIT:
                        GameState = NewGameState;
                        break;
                    case GAME_STATE:
                        eventTime = AnimatedEventHandler.EVENTTIME;
                        animatedHandler.AnimatedEvent = true;                                                
                        break;
                    case MENU_STATE:
                        if (GameState == SPLASH_SCREEN_STATE)
                        {
                            eventTime = AnimatedEventHandler.SPLASHTIME;
                            animatedHandler.AnimatedEvent = true;
                        }
                        else
                        {
                            eventTime = AnimatedEventHandler.EVENTTIME;
                            animatedHandler.AnimatedEvent = true;
                        }
                        break;
                    case GAME_OVER_STATE:
                        eventTime = AnimatedEventHandler.STATSTIME;
                        animatedHandler.AnimatedEvent = true;
                        GameState = NewGameState;
                        break;
                    case PAUSE_STATE:
                        GameState = NewGameState;
                        break;
                    case UPGRADE_STATE:                        
                        GameState = NewGameState;
                        break;
                    case CUT_SCENE_STATE:
                        eventTime = AnimatedEventHandler.CUTSCENE;
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
                    upgradeView.Update(gameTime);
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
                    if (GameState == EventOperator.GAME_OVER_STATE)
                        game.ResetGame(true);
                    menu.CurrentMenu = MenuConstants.MAIN;
                    break;
                case EventOperator.GAME_STATE:
                    if (GameState == EventOperator.MENU_STATE)
                        game.ResetGame(true);
                    break;
                case EventOperator.CUT_SCENE_STATE:
                    NewGameState = UPGRADE_STATE;
                    game.Player.Position = new Vector2(WindowSize.Width / 2, WindowSize.Height / 2);
                    break;

            }
            animatedHandler.AnimatedEvent = false;
            GameState = NewGameState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //refactor whole draw?
            if (animatedHandler.AnimatedEvent)            
                animatedHandler.Draw(spriteBatch, gameTime);            
            else
            {
                if(GameState == PAUSE_STATE || GameState == UPGRADE_STATE)                    
                  game.DrawGame(spriteBatch, gameTime, false);

                if (GameState == UPGRADE_STATE)
                   upgradeView.Draw(spriteBatch, gameTime);

                if (!animatedHandler.AnimatedEvent && NewGameState != CUT_SCENE_STATE)             
                  menu.Draw(spriteBatch, gameTime);                      
            }
        }     
         
        public void ResetGame(bool fullReset)
        {
            game.ResetGame(fullReset);
            if (fullReset)
                upgradeView.Reset();
            //animatedHandler.Reset();
        }
    }
}
