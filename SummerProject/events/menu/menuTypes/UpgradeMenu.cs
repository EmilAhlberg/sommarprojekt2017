using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.menu
{
    public class UpgradeMenu : MenuComponent
    {
        public UpgradeMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.UPGRADE])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.NewGameState = EventOperator.GAME_STATE;
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