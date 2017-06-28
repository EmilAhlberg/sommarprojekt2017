using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.menu
{
    class SettingsMenu : MenuComponent
    {
        public SettingsMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.SETTINGS])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.ChangeGameMode(0);
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    handler.ChangeGameMode(1);
                    pressedIndex = selectedIndex;
                    break;
                case 2:
                    pressedIndex = int.MaxValue;
                    return MenuConstants.MAIN;   
            }
            return -1;
        }
    }
}
