using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;

namespace SummerProject.menu
{
    class ModeSelectionMenu : MenuComponent
    {
        public ModeSelectionMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.MODESELECTION])
        {
            pressedIndex = 0; //! Change iff default mode is changed
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.GameMode.TimeMode = GameMode.DECREASING_TIME;
                    handler.GameMode.SpawnMode = GameMode.RANDOM_SINGLE;
                    handler.GameMode.IsChanged = true;             
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    handler.GameMode.TimeMode = GameMode.CONSTANT_TIME;
                    handler.GameMode.SpawnMode = GameMode.RANDOM_WAVE;
                    handler.GameMode.IsChanged = true;
                    pressedIndex = selectedIndex;
                    break;
                case 2:
                    handler.GameMode.TimeMode = GameMode.BURST_TIME;
                    handler.GameMode.SpawnMode = GameMode.BURST_WAVE;
                    handler.GameMode.IsChanged = true;
                    pressedIndex = selectedIndex;
                    break;
                case 3:
                    pressedIndex = int.MaxValue;
                    handler.NewGameState = EventOperator.GAME_STATE;
                    handler.ResetGame(true);
                    break;
            }
            return -1;
        }
    }
}
