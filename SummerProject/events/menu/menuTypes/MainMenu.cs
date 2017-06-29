using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                    break;
            }
            return -1;
        }
    }
}
