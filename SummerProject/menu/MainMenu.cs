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
                    handler.NewGameState = EventOperator.GAME_STATE;
                    handler.ResetGame(true);
                    break;
                case 1:
                    return MenuConstants.SETTINGS;
                case 2:
                    handler.NewGameState = EventOperator.EXIT;
                    break;
            }
            return -1;
        }
    }
}
