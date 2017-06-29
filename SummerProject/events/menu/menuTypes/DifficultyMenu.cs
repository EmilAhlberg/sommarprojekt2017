using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;

namespace SummerProject.menu
{
    public class DifficultyMenu : MenuComponent
    {

        public DifficultyMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.DIFFICULTY])
        {
            pressedIndex = 0; //! Change iff default mode is changed

        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.GameMode.ChangeDifficulty(Difficulty.EASY);                   
                    //handler.GameMode.IsChanged = true;
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    handler.GameMode.ChangeDifficulty(Difficulty.NORMAL);
                    //handler.GameMode.IsChanged = true;
                    pressedIndex = selectedIndex;
                    break;
                case 2:
                    handler.GameMode.ChangeDifficulty(Difficulty.HARD);
                    //handler.GameMode.IsChanged = true;
                    pressedIndex = selectedIndex;
                    break;
                case 3:
                    handler.NewGameState = EventOperator.GAME_STATE;
                    handler.ResetGame(true);
                    break;
            }
            return -1;
        }
    }
}