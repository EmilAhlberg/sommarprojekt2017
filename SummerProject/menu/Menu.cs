using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SummerProject.menu
{
    class Menu
    {
        private List<MenuComponent> menues;
        private int selectedIndex;
        public int CurrentMenu { private get; set; } = MenuConstants.MAIN;

        public Menu(Vector2 position, SpriteFont spriteFont)
        {
            InitializeMenues(spriteFont, position);
        }

        private void InitializeMenues(SpriteFont spriteFont, Vector2 position)
        {
            menues = new List<MenuComponent>();
            //order of added menues is important
            menues.Add(new MainMenu(position, spriteFont));
            menues.Add(new SettingsMenu(position, spriteFont));
            menues.Add(new GameOverMenu(position, spriteFont));
        }

        private bool CheckKey(Keys theKey)
        {
            return InputHandler.isJustPressed(theKey);
        }

        public void Update(GameTime gameTime, EventOperator handler)
        {
            
            for(int i = 0; i < MenuConstants.MENUITEMS[CurrentMenu].Length; i++)
            {
                Vector2 measuredString = menues[CurrentMenu].Font.MeasureString(MenuConstants.MENUITEMS[CurrentMenu][i]);
                Point indexPosition = new Point((int)(menues[CurrentMenu].Position.X), (int)(menues[CurrentMenu].Position.Y + i * measuredString.Y)); //Might be inprecise for a large number of menuitems DUNNO
                Rectangle boundBox = new Rectangle(indexPosition.X, indexPosition.Y, (int)measuredString.X, (int)measuredString.Y);
                if (boundBox.Contains(InputHandler.mPosition))
                    selectedIndex = i;
            }

            if (InputHandler.isJustPressed(MouseButton.LEFT))
            {
                int changedMenu = menues[CurrentMenu].HandleSelection(CurrentMenu, selectedIndex, handler);
                if (changedMenu >= 0)
                {
                    selectedIndex = 0;
                    CurrentMenu = changedMenu;
                }
            }
            //keyboardState = Keyboard.GetState();        
            //if (CheckKey(Keys.Down))
            //{
            //    selectedIndex++;
            //    selectedIndex %= MenuConstants.MENUITEMS[CurrentMenu].Length;
            //}
            //if (CheckKey(Keys.Up))
            //{
            //    selectedIndex--;
            //    if (selectedIndex < 0)
            //        selectedIndex = MenuConstants.MENUITEMS[CurrentMenu].Length - 1;
            //}
            //if (CheckKey(Keys.Enter))
            //{
            //    int changedMenu = menues[CurrentMenu].HandleSelection(CurrentMenu, selectedIndex, handler);
            //    if (changedMenu >= 0)
            //    {
            //        selectedIndex = 0;
            //        CurrentMenu = changedMenu;
            //    }
            //}
            //oldKeyboardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menues[CurrentMenu].Draw(spriteBatch, gameTime, selectedIndex);
        }
    }
}
