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
        public static readonly string[] COUNTDOWN = { "GO!", "SET!", "READY!", "" };
                
        public int GameState { get; set; } = MenuConstants.MAIN;
        private bool activeEvent;
        public int NewGameState { get; set; }
        private const float eventTime = 3f;
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
                        activeEvent = true;   //set event times here?                                    
                        //GameState = NewGameState;                      
                        break;
                    case MENU_STATE:
                        GameState = NewGameState;
                        break;
                    case GAME_OVER_STATE:                        
                        activeEvent = true;  //set event times here?
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
                    menu.Update(gameTime, this);
                    break;
                case GAME_OVER_STATE:
                    menu.CurrentMenu = MenuConstants.GAME_OVER;
                    menu.Update(gameTime, this);
                    break;
                case GAME_STATE:
                    game.ResetGame();
                    break;
            }
        }

        public void UpdateEventTimer(GameTime gameTime)
        {  
            eventTimer.CountDown(gameTime);
            if (eventTimer.IsFinished)
            {
                FinishEvent();
                eventTimer.Reset(); //?
            }
        }

        // handles end-of-event functionality
        private void FinishEvent()
        {
            switch (NewGameState)
            {
                case EventOperator.GAME_STATE:
                    game.ResetGame();
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
                    case GAME_STATE:
                        String word = COUNTDOWN[(int)eventTimer.currentTime];
                        Color color = Color.Gold;
                        if ((int)eventTimer.currentTime == 0)
                            color = Color.OrangeRed;
                        spriteBatch.DrawString(font, word, WordLayoutPosition(word), color);
                        break;
                    case GAME_OVER_STATE:
                        String score = "Score: " + ScoreHandler.Score;
                        spriteBatch.DrawString(font, score, WordLayoutPosition(score), Color.Gold);
                        break;
                }
            }
            else
            {
                menu.Draw(spriteBatch, gameTime);
            }
        }

        private Vector2 WordLayoutPosition(String s)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            //float height = 0;
            if (size.X > width)
                width = size.X;
            //height += font.LineSpacing + 5;
            return new Vector2((game.Window.ClientBounds.Width - width) / 2, (game.Window.ClientBounds.Height - 0) / 2);
        }

        //super duper big-method
        public void IsMouseVisible(bool mouseVisibility)
        {
            game.IsMouseVisible = mouseVisibility;
        }
    }
}
