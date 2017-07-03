using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.achievements;
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
                    //if(!isLocked[selectedIndex])
                    //{
                        handler.GameMode.TimeMode = GameMode.CONSTANT_TIME;
                        handler.GameMode.SpawnMode = GameMode.RANDOM_WAVE;
                        handler.GameMode.IsChanged = true;
                        pressedIndex = selectedIndex;
                    //}                                
                    break;
                case 2:
                    //if (!isLocked[selectedIndex])
                    //{
                        handler.GameMode.TimeMode = GameMode.BURST_TIME;
                        handler.GameMode.SpawnMode = GameMode.BURST_WAVE;
                        handler.GameMode.IsChanged = true;
                        pressedIndex = selectedIndex;
                    //}
                    break;
                case 3:
                    return MenuConstants.DIFFICULTY;
            }
            return -1;
        }

        public override void UpdateUnlocks(EventOperator handler)
        {
            if (handler.achControl.Achievements[Traits.WAVE_MODE].Unlocked)
                isLocked[1] = false;
            if (handler.achControl.Achievements[Traits.BURST_MODE].Unlocked)
                isLocked[2] = false;
        }

        protected override void SetLockedItems()
        {
            isLocked = new bool[menuItems.Length];
            //isLocked[1] = true;
            //isLocked[2] = true;
        }


    }
}
