﻿using System;
using System.Collections.Generic;

namespace SummerProject.menu
{
    class MenuConstants
    {
        public const int MAIN = 0;
        public const int MODESELECTION = 1;
        public const int GAME_OVER = 2;
        public const int PAUSE = 3;
        public const int UPGRADE = 4;
        public const int DIFFICULTY = 5;

        public static readonly Dictionary<int, String[]> MENUITEMS =
            new Dictionary<int, String[]>
            {
                {MAIN, new string[] { "Start Game", "End Game" }},
                {MODESELECTION, new string[] { "Time Mode", "Spawn Mode","Burst Mode", "Difficulty", "PLAY!"}},
                {GAME_OVER, new string[] { "Play Again", "Main Menu" }},
                {PAUSE, new string[] {"Resume", "GIVE UP!" }},
                {UPGRADE, new string[] {"Resume"}},
                {DIFFICULTY, new string[] {"Easy","Normal","Hard","Back"} }
            };
              
    }
}
