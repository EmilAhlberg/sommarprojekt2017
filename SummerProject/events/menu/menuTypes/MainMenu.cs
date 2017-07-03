using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SummerProject.menu
{
    class MainMenu : MenuComponent
    {
        public MainMenu(Vector2 position, SpriteFont font) : base(position, font, MenuConstants.MENUITEMS[MenuConstants.MAIN])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:              
                    return MenuConstants.MODESELECTION;
                case 1:
                    handler.NewGameState = EventOperator.EXIT;
                    //achievements.SaveHandler s1 = new achievements.SaveHandler();
                    //achievements.SaveData d1 = new achievements.SaveData();
                    //s1.Save(d1, "save_file");
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
