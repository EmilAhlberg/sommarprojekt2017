using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class EventOperator
    {
        public const int EXIT = 0;
        public const int MENU_STATE = 1;
        public const int GAME_STATE = 2;
        public const int GAME_OVER_STATE = 3;
        public static readonly string[] COUNTDOWN = { "GO!", "SET!", "READY!", ""};
        
        public int GameState { get; set; } = MenuConstants.MAIN;        
        public int NewGameState { get; set; }

        private bool activeEvent;
        private float eventTime = 3f;   //!!     
        private Menu menu;
        private Game1 game;
        private SpriteFont font;
       

        public EventOperator(SpriteFont font, Game1 game)
        {           
            this.font = font;
            GameState = MENU_STATE;
            NewGameState= GameState;            
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
                eventTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (eventTime < 0)
                {
                FinishEvent();     
                eventTime = 3f; //!
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
                        String word = COUNTDOWN[(int)eventTime];
                        Color color = Color.Gold;
                        if ((int)eventTime == 0)
                            color = Color.OrangeRed;
                        spriteBatch.DrawString(font, word, WordLayoutPosition(word), color);
                        break;
                    case GAME_OVER_STATE:
                        String score = "High Score: ";
                        spriteBatch.DrawString(font, score, WordLayoutPosition(score), Color.Gold);
                        break;
                }
            } else
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
