using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private int currentMenu = MenuConstants.MAIN;        

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
                selectedIndex %= MenuConstants.MENUITEMS[currentMenu].Length;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = MenuConstants.MENUITEMS[currentMenu].Length - 1;
            }
            if (CheckKey(Keys.Enter))
            {
                int changedMenu = menues[currentMenu].HandleSelection(currentMenu, selectedIndex, handler);
                if(changedMenu >= 0) 
                {
                    selectedIndex = 0;
                    currentMenu = changedMenu;
                }
            }
            oldKeyboardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menues[currentMenu].Draw(spriteBatch, gameTime, selectedIndex);
        }
    }
}
