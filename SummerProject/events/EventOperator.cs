using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.framework;
using SummerProject.menu;
using System;

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
       
                
        public int GameState { get; set; } 
        public int NewGameState { get; set; }
        private AnimatedEventHandler animatedHandler;
        private Menu menu;
        private UpgradeView upgradeView;
        private Game1 game;      

        public EventOperator(SpriteFont font, Game1 game, Texture2D upgradeViewText)
        {            
            GameState = MENU_STATE;
            NewGameState = GameState;
            animatedHandler = new AnimatedEventHandler(game, this, font);
            upgradeView = new UpgradeView(upgradeViewText);            
            menu = new Menu(new Vector2(WindowSize.Width / 2,
                    WindowSize.Height / 2), font);
            this.game = game;
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
                animatedHandler.Reset(); //May want to set this differently in different cases.
                switch (NewGameState)
                {
                    case EXIT:
                        GameState = NewGameState;
                        break;
                    case GAME_STATE:
                        animatedHandler.AnimatedEvent = true;                                                
                        break;
                    case MENU_STATE:
                        animatedHandler.AnimatedEvent = true;
                        break;
                    case GAME_OVER_STATE:
                        animatedHandler.AnimatedEvent = true;
                        GameState = NewGameState;
                        break;
                    case PAUSE_STATE:
                        GameState = NewGameState;
                        break;
                    case UPGRADE_STATE:
                        GameState = NewGameState;
                        break;
                }
            }
        }
        
        private void UpdateState(GameTime gameTime)
        {
            switch (GameState)
            {
                case EXIT:
                    game.Exit();
                    break;
                case MENU_STATE:
                    break;
                case GAME_OVER_STATE:
                    menu.CurrentMenu = MenuConstants.GAME_OVER;                   
                    break;
                case GAME_STATE:     
                    menu.CurrentMenu = MenuConstants.MAIN;                
                    break;
                case PAUSE_STATE:
                    if (NewGameState != EventOperator.MENU_STATE)
                        menu.CurrentMenu = MenuConstants.PAUSE;             
                    break;
                case UPGRADE_STATE:
                    menu.CurrentMenu = MenuConstants.UPGRADE;
                    upgradeView.Update(gameTime);
                    break;
            }
            menu.Update(gameTime, this);
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
                  game.DrawGame(spriteBatch, gameTime);

                if (GameState == UPGRADE_STATE)
                    upgradeView.Draw(spriteBatch, gameTime);

                if (!animatedHandler.AnimatedEvent)             
                  menu.Draw(spriteBatch, gameTime);                      
            }
        }     

        //super duper big-method
        public void IsMouseVisible(bool mouseVisibility)
        {
            game.IsMouseVisible = mouseVisibility;
        }

        public void ResetGame(bool fullReset)
        {
            game.ResetGame(fullReset);
        }
    }
}
