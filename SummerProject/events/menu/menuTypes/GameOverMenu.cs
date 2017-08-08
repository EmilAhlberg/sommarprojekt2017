using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.menu
{
    class GameOverMenu : MenuComponent
    {
        public GameOverMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.GAME_OVER])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {

            switch (selectedIndex)
            {
                case 0:
                    handler.NewGameState = EventOperator.UPGRADE_STATE;
                    handler.ResetGame(true);
                    break;
                case 1:
                    handler.NewGameState = EventOperator.MENU_STATE; 
                    handler.ResetGame(false);
                    break;
            }
            return -1;
        }

        public override void UpdateUnlocks(EventOperator handler)
        {           
        }

        protected override void SetLockedItems()
        {
            isLocked = new bool[menuItems.Length];
        }


    }
}
