using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;
using SummerProject.achievements;
using SummerProject.factories;

namespace SummerProject.menu
{
    public class DifficultyMenu : MenuComponent
    {

        public DifficultyMenu(Vector2 position) : base(position, MenuConstants.MENUITEMS[MenuConstants.DIFFICULTY])
        {
            pressedIndex = 0; //! Change iff default mode is changed

        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.GameMode.ChangeDifficulty(Difficulty.EASY);                   
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    if (!isLocked[selectedIndex])
                    {
                        handler.GameMode.ChangeDifficulty(Difficulty.NORMAL);                        
                        pressedIndex = selectedIndex;
                    }                   
                    break;
                case 2:
                    if (!isLocked[selectedIndex])
                    {
                        handler.GameMode.ChangeDifficulty(Difficulty.HARD);                       
                        pressedIndex = selectedIndex;
                    }
                    break;
                case 3:
                    GameMode.StartingLevel = 0; //as long as #wave comes after this  one
                    return MenuConstants.WAVE;
            }
            return -1;
        }

        public override void UpdateUnlocks(EventOperator handler)
        {
            if (handler.achControl.Achievements[Traits.NORMAL_DIFFICULTY].Unlocked)
                isLocked[1] = false;
            if (handler.achControl.Achievements[Traits.HARD_DIFFICULTY].Unlocked)
                isLocked[2] = false;
        }

        protected override void SetLockedItems()
        {
            isLocked = new bool[menuItems.Length];
            isLocked[1] = true;
            isLocked[2] = true;
        }
    }
}