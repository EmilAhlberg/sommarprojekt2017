using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public static readonly string[] COUNTDOWN = { "GO!", "READY!", "" };
                
        public int GameState { get; set; } = MenuConstants.MAIN;
        private bool activeEvent;        
        public int NewGameState { get; set; }
        private const float eventTime = 2f;
        private Timer eventTimer;    
        private Menu menu;
        private Game1 game;
        private SpriteFont font;

        public EventOperator(SpriteFont font, Game1 game)
        {
            this.font = font;
            GameState = MENU_STATE;
            NewGameState= GameState;
            eventTimer = new Timer(eventTime);
            menu = new Menu(new Vector2((game.Window.ClientBounds.Width) / 2, 
                    (game.Window.ClientBounds.Height) / 2), font);
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            if (activeEvent)
                UpdateEventTimer(gameTime);
            else
            {
                ActivateEvent(gameTime);
                UpdateState(gameTime);
            }         
        }

        public void ActivateEvent(GameTime gameTime)
        {
            if (NewGameState != GameState)
            {
                eventTimer.Reset(); //May want to set this differently in different cases.
                switch (NewGameState)
                {
                    case EXIT:
                        GameState = NewGameState;
                        break;
                    case GAME_STATE:
                        activeEvent = true;                                                
                        break;
                    case MENU_STATE:
                        activeEvent = true;
                        break;
                    case GAME_OVER_STATE:                        
                        activeEvent = true;  
                        GameState = NewGameState;
                        break;
                    case PAUSE_STATE:
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
            }
            menu.Update(gameTime, this);
        }

        public void UpdateEventTimer(GameTime gameTime)
        {  
            eventTimer.CountDown(gameTime);
            if (eventTimer.IsFinished)
            {
                FinishEvent();
                eventTimer.Reset(); 
            }
        }

        // handles end-of-event functionality
        private void FinishEvent()
        {
            switch (NewGameState)
            {
                case MENU_STATE:
                    menu.CurrentMenu = MenuConstants.MAIN;
                    break;
                case GAME_STATE:
                    if (!(GameState == EventOperator.PAUSE_STATE))             
                        game.ResetGame(true);        
                    break;
            }
            activeEvent = false;
            GameState = NewGameState;                                 
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (activeEvent)
            { 
                switch (NewGameState)
                {
                    case MENU_STATE:                      
                        ResetGame(false);
                        game.UpdateGame(gameTime);
                        game.DrawGame(spriteBatch, gameTime);
                        String s = "Mediocre!"; //!
                        spriteBatch.DrawString(font, s, WordLayoutPosition(s), Color.Gold);
                        System.Threading.Thread.Sleep(40); //! slow mo of doom                                          
                        break;
                    case GAME_STATE:                          
                        game.DrawGame(spriteBatch, gameTime);
                        DrawCountDown(spriteBatch, gameTime);                       
                        break;
                    case GAME_OVER_STATE:
                        String score = "Score: " + ScoreHandler.Score; //!
                        spriteBatch.DrawString(font, score, WordLayoutPosition(score), Color.Gold);
                        break;
                }
            }
            else
            {
                if(GameState == PAUSE_STATE)                    
                  game.DrawGame(spriteBatch, gameTime);


                if (!activeEvent)             
                  menu.Draw(spriteBatch, gameTime);                      
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

        private Vector2 WordLayoutPosition(String s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;         
            if (size.X > width)
                width = size.X;         
            return new Vector2((game.Window.ClientBounds.Width - width) / 2, (game.Window.ClientBounds.Height - 0) / 2);
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
