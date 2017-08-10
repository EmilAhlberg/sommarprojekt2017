using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SummerProject.menu
{
    class MainMenu : MenuComponent
    {
        public MainMenu(Vector2 position) : base(position, MenuConstants.MENUITEMS[MenuConstants.MAIN])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:              
                    return MenuConstants.DIFFICULTY;
                case 1:
                    handler.NewGameState = EventOperator.EXIT;                   
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
