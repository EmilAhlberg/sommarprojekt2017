using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            handler.NewGameState = EventOperator.GAME_STATE;
            //switch (selectedIndex)
            //{
            //    case 0:
            //        handler.IsMouseVisible(false);
            //        break;
            //    case 1:
            //        handler.IsMouseVisible(true);
            //        break;
            //    case 2:
            //        return MenuConstants.MAIN;
            //}
            return -1;
        }
    }
}
