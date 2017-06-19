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
                    handler.NewGameState = EventOperator.GAME_STATE;
                    break;
                case 1:
                    handler.NewGameState = EventOperator.MENU_STATE;
                    return MenuConstants.MAIN;
            }
            return -1;
        }
    }
}
