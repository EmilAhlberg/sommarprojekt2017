using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public int GameState { get; set; } = MenuConstants.MAIN;
        public bool ActiveEvent { get; private set; }
        public int NewGameState { private get; set; }
        private float eventTime = 10f;        
        private Menu menu;
        private Game1 game;

        public EventOperator(Vector2 position, SpriteFont font, Game1 game)
        {         
            GameState = MENU_STATE;
             NewGameState= GameState;
            menu = new Menu(position, font);
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            //UpdateEventTime(gameTime);
            ChangeState(gameTime);
            HandleState(gameTime);            
        }

        public void ChangeState(GameTime gameTime)
        {
            if (NewGameState != GameState)
            {
                 switch (GameState)
                {
                    case MENU_STATE:
                        ActiveEvent = true;
                        game.StartGame(gameTime);
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
                //case GAME_STATE:
                //    throw new NotImplementedException();
            }
        }

        public void UpdateEventTimer(GameTime gameTime)
        {            
                eventTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (eventTime < 0)
                {
                    ActiveEvent = false;
                    eventTime = 5f; //!
                }                              
                 
        }
        
        

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //switch (GameState)
            //{
            //    case MENU_STATE:
                    menu.Draw(spriteBatch, gameTime);
            //        break;
            //    case GAME_STATE:
            //        throw new NotImplementedException();                                   
            //}
        }

        //super method
        public void IsMouseVisible(bool mouseVisibility)
        {
            game.IsMouseVisible = mouseVisibility;
        }
    }
}
