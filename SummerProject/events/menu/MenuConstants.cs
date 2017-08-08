using System;
using System.Collections.Generic;

namespace SummerProject.menu
{
    class MenuConstants
    {
        public const int MAIN = 0;
        //public const int MODESELECTION = 1;
        public const int GAME_OVER = 2;
        public const int PAUSE = 3;
        public const int UPGRADE = 4;
        public const int DIFFICULTY = 1;
        public const int WAVE = 5;

        public static readonly Dictionary<int, string[]> MENUITEMS =
            new Dictionary<int, string[]>
            {
                {MAIN, new string[] { "Start Game", "End Game" }},
                //{MODESELECTION, new string[] { "Default Mode", "Wave Mode", "Zerg Mode", "Next"}},
                {GAME_OVER, new string[] { "Play Again", "Main Menu" }},
                {PAUSE, new string[] {"Resume", "GIVE UP!" }},
                {UPGRADE, new string[] {"Continue", "Reset Ship"}},
                {DIFFICULTY, new string[] {"Normal","Hard","Insane","Next"} },
                {WAVE, new string[] {"Wave 1","Wave 11","Wave 21", "Wave 31", "Wave 41", "PLAY!"} }
            };
              
    }
}
