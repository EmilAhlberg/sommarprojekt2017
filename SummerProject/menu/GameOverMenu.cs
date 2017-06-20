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
                    handler.ResetGame(true);
                    break;
                case 1:
                    handler.NewGameState = EventOperator.MENU_STATE;
                    handler.ResetGame(false);
                    //return MenuConstants.MAIN;
                    break;
            }
            return -1;
        }
    }
}
