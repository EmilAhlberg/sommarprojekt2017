using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject.menu
{
    class Menu
    {
        private List<MenuComponent> menues;
        private int selectedIndex;
        public int CurrentMenu { private get; set; } = MenuConstants.MAIN;

        public Menu(Vector2 position)
        {
            InitializeMenues(position);
        }

        private void InitializeMenues(Vector2 position)
        {
            menues = new List<MenuComponent>();
            //order of added menues is important
            menues.Add(new MainMenu(position));
            menues.Add(new DifficultyMenu(position));            
            menues.Add(new GameOverMenu(position));
            menues.Add(new PauseMenu(position));
            menues.Add(new UpgradeMenu(position));
            menues.Add(new WaveMenu(position));


        }

        private bool CheckKey(Keys theKey)
        {
            return InputHandler.isJustPressed(theKey);
        }

        public void Update(GameTime gameTime, EventOperator handler)
        {
            menues[CurrentMenu].UpdateUnlocks(handler);
            int selection = -1;
            for(int i = 0; i < MenuConstants.MENUITEMS[CurrentMenu].Length; i++)
            {
                Vector2 measuredString = menues[CurrentMenu].Font.MeasureString(MenuConstants.MENUITEMS[CurrentMenu][i]);
                Point indexPosition = new Point((int)(menues[CurrentMenu].Position.X), (int)(menues[CurrentMenu].Position.Y + i * measuredString.Y)); //Might be inprecise for a large number of menuitems DUNNO
                Point origin = new Point((int)measuredString.X / 2, (int)measuredString.Y/2);             
                Rectangle boundBox = new Rectangle((int)menues[CurrentMenu].Position.X - origin.X, indexPosition.Y - origin.Y, (int)measuredString.X, (int)measuredString.Y);
                if (boundBox.Contains(InputHandler.mPosition))
                    selection = i;               
            }
            selectedIndex = selection;
            if (InputHandler.isJustPressed(MouseButton.LEFT))
            {
                if(selection != -1)
                    SoundHandler.PlaySoundEffect((int)IDs.MENUCLICK);
                int changedMenu = menues[CurrentMenu].HandleSelection(CurrentMenu, selectedIndex, handler);
                if (changedMenu >= 0)
                {
                    selectedIndex = 0;
                    CurrentMenu = changedMenu;
                }
            }       
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menues[CurrentMenu].Draw(spriteBatch, gameTime, selectedIndex);
        }
    }
}
