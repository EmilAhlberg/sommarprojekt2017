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

        public int HighScore { get; set; }
        public int GameState { get; set; } = MenuConstants.MAIN;
        public bool ActiveEvent { get; private set; }
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
            ChangeState(gameTime);
            HandleState(gameTime);            
        }

        public void ChangeState(GameTime gameTime)
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
                        ActiveEvent = true;
                        //GameState = NewGameState;                      
                        break;
                    case MENU_STATE:
                        GameState = NewGameState;
                        break;
                    case GAME_OVER_STATE:
                        ActiveEvent = true;                       
                        GameState = NewGameState;
                        break;
                   
                }
            }
        }

        private void HandleState(GameTime gameTime)
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
                    menu.currentMenu = MenuConstants.GAME_OVER;
                    menu.Update(gameTime, this);
                    break;
                //case GAME_STATE:
                //    throw new NotImplementedException();
            }
        }

        public void UpdateEventTimer(GameTime gameTime)
        {
            eventTimer.CountDown(gameTime);
            if (eventTimer.IsFinished)
            {
                ActiveEvent = false;
                GameState = NewGameState;
                eventTimer.Reset(); //?
            }                               
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {        
            if (ActiveEvent)
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
                        String score = "High Score: " + HighScore;
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
            float height = 0;
            if (size.X > width)
                width = size.X;
            height += font.LineSpacing + 5;
            return new Vector2((game.Window.ClientBounds.Width - width) / 2, (game.Window.ClientBounds.Height - height) / 2);
        }

        //super duper big-method
        public void IsMouseVisible(bool mouseVisibility)
        {
            game.IsMouseVisible = mouseVisibility;
        }
    }
}
