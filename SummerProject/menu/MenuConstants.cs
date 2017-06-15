using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.menu
{
    class MenuConstants
    {
        public const int MAIN = 0;      
        public const int SETTINGS = 1;
        
        public static readonly Dictionary<int, String[]> MENUITEMS =
            new Dictionary<int, String[]>
            {
                {MAIN, new string[] { "Start Game", "Settings", "End Game" }},
                {SETTINGS, new string[] { "Nerf Homing Bullet", "More lazors", "Back" }}
            };
    }
}
