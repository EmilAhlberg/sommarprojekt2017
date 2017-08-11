using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.menu
{
    public class UpgradeMenu : MenuComponent
    {
        public UpgradeMenu(Vector2 position) : base(position, MenuConstants.MENUITEMS[MenuConstants.UPGRADE])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    if (!isLocked[0])
                    {
                        handler.NewGameState = EventOperator.GAME_STATE;
                        handler.UpgradeView.UpgradeBar.RemoveSelection();
                    }
                    break;
                case 1:
                    if (!isLocked[1])
                        handler.UpgradeView.Reset();
                    break;
            }
            return -1;
        }

        public override void UpdateUnlocks(EventOperator handler)
        {
            if (handler.UpgradeView.IsModified)
            {
                isLocked[0] = false;
                isLocked[1] = false;
            } else
            {
                SetLockedItems();
            }              
        }

        protected override void SetLockedItems()
        {
            isLocked = new bool[menuItems.Length];
            isLocked[0] = true;
            isLocked[1] = true;
        }
    }

}