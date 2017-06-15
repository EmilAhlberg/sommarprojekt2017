using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;


    public class MenuComponent
    {

        private string[] MenuItems;  
        private SpriteFont spriteFont;
        private static readonly Color normal = Color.White;
        private static readonly Color hilite = Color.Yellow;
        private float width; 
        private float height;
        private Vector2 position;

        public MenuComponent(Vector2 position, SpriteFont spriteFont, string[] menuItems)
        {
            this.MenuItems = menuItems;
            this.spriteFont = spriteFont;
            MeasureMenu();
            this.position = position - (new Vector2(width, height))/2;        
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in MenuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }
        }     

        public int HandleSelection(int currentMenu, int selectedIndex, Game1 game)
        {
            switch(currentMenu)
            {
                case MenuConstants.MAIN:
                    return HandleMainMenu(selectedIndex, game);                    
                case MenuConstants.SETTINGS:
                    return HandleSettingsMenu(selectedIndex, game);                   
                default: throw new NotImplementedException();
            }           
        }

        private int HandleSettingsMenu(int selectedIndex, Game1 game)
        {
            switch (selectedIndex)
            {
                case 0: game.IsMouseVisible = false;           
                    break;
                case 1: game.IsMouseVisible = true;
                    break;
                case 2:
                    return MenuConstants.MAIN;                    
            }
            return -1;
        }

        private int HandleMainMenu(int selectedIndex, Game1 game)
        {
            switch (selectedIndex)
            {
                case 0:
                    game.GameState = Game1.GAME_STATE;
                    break;
                case 1:
                    return MenuConstants.SETTINGS;
                case 2:
                    game.Exit();
                    break;
            }
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, int selectedIndex)
        {          
            Vector2 location = position;
            Color tint;
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = hilite;
                else
                    tint = normal;
                spriteBatch.DrawString(spriteFont, MenuItems[i], location, tint);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}