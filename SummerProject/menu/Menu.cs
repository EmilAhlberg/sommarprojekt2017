using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SummerProject.menu
{
    class Menu
    {
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;
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
            menues.Add(new PauseMenu(position, spriteFont));
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
        }

        public void Update(GameTime gameTime, EventOperator handler)
        {
            keyboardState = Keyboard.GetState();
            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                selectedIndex %= MenuConstants.MENUITEMS[CurrentMenu].Length;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = MenuConstants.MENUITEMS[CurrentMenu].Length - 1;
            }
            if (CheckKey(Keys.Enter))
            {
                int changedMenu = menues[CurrentMenu].HandleSelection(CurrentMenu, selectedIndex, handler);
                if (changedMenu >= 0)
                {
                    selectedIndex = 0;
                    CurrentMenu = changedMenu;
                }
            }
            oldKeyboardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menues[CurrentMenu].Draw(spriteBatch, gameTime, selectedIndex);
        }
    }
}
