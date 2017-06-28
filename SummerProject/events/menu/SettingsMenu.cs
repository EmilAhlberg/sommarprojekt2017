﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;

namespace SummerProject.menu
{
    class SettingsMenu : MenuComponent
    {
        public SettingsMenu(Vector2 position, SpriteFont spriteFont) : base(position, spriteFont, MenuConstants.MENUITEMS[MenuConstants.SETTINGS])
        {
        }

        public override int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler)
        {
            switch (selectedIndex)
            {
                case 0:
                    handler.GameMode.TimeMode = GameMode.DECREASING_TIME;
                    handler.GameMode.SpawnMode = GameMode.RANDOM_SINGLE;
                    handler.GameMode.ChangeLevel = true;             
                    pressedIndex = selectedIndex;
                    break;
                case 1:
                    handler.GameMode.TimeMode = GameMode.CONSTANT_TIME;
                    handler.GameMode.SpawnMode = GameMode.RANDOM_WAVE;
                    handler.GameMode.ChangeLevel = true;
                    pressedIndex = selectedIndex;
                    break;
                case 2:
                    pressedIndex = int.MaxValue;
                    return MenuConstants.MAIN;   
            }
            return -1;
        }
    }
}
