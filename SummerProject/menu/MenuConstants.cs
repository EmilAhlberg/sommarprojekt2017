﻿using System;
using System.Collections.Generic;

namespace SummerProject.menu
{
    class MenuConstants
    {
        public const int MAIN = 0;
        public const int SETTINGS = 1;
        public const int GAME_OVER = 2;

        public static readonly Dictionary<int, String[]> MENUITEMS =
            new Dictionary<int, String[]>
            {
                {MAIN, new string[] { "Start Game", "Settings", "End Game" }},
                {SETTINGS, new string[] { "God Mode", "I am a Peasant", "Back" }},
                {GAME_OVER, new string[] { "Play Again", "Main Menu" }}
            };
    }
}
