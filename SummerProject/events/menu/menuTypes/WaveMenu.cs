using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;
using SummerProject.achievements;
using SummerProject.factories;

namespace SummerProject.menu
{
    public class WaveMenu : MenuComponent
    {

        public WaveMenu(Vector2 position) : base(position, MenuConstants.MENUITEMS[MenuConstants.WAVE])
        {
            pressedIndex = 0; //! Change iff default mode is changed
            GameMode.StartingLevel = 0;
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    GameMode.StartingLevel = 0;
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    if (!isLocked[selectedIndex])
                    {
                        GameMode.StartingLevel = 10;
                        pressedIndex = selectedIndex;
                    }                   
                    break;
                case 2:
                    if (!isLocked[selectedIndex])
                    {
                        GameMode.StartingLevel = 20;
                        pressedIndex = selectedIndex;
                    }
                    break;
                case 3:
                    if (!isLocked[selectedIndex])
                    {
                        GameMode.StartingLevel = 30;
                        pressedIndex = selectedIndex;
                    }
                    break;
                case 4:
                    if (!isLocked[selectedIndex])
                    {
                        GameMode.StartingLevel = 40;
                        pressedIndex = selectedIndex;
                    }
                    break;
                case 5:
                    handler.NewGameState = EventOperator.UPGRADE_STATE;
                    handler.ResetGame(true);
                    break;
            }
            return -1;
        }

        public override void UpdateUnlocks(EventOperator handler)
        {
            if (handler.achControl.Achievements[Traits.WAVE11].Unlocked)
                isLocked[1] = false;
            if (handler.achControl.Achievements[Traits.WAVE21].Unlocked)
                isLocked[2] = false;
            if (handler.achControl.Achievements[Traits.WAVE31].Unlocked)
                isLocked[2] = false;
            if (handler.achControl.Achievements[Traits.WAVE41].Unlocked)
                isLocked[2] = false;
        }

        protected override void SetLockedItems()
        {
            isLocked = new bool[menuItems.Length];
            isLocked[1] = true;
            isLocked[2] = true;
            isLocked[3] = true;
            isLocked[4] = true;
        }
    }
}