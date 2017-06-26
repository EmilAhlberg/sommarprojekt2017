using System.Collections.Generic;

namespace SummerProject.factories
{
    class EntityTypes
    {
        public const int ARRAYSIZE = 200;
        public const int BULLET = 0;            // Bullet range = 0-49
        public const int HOMINGBULLET = 1;
        public const int EVILBULLET = 100;        // Enemy Bullet Range = 100 - 149
        public const int HEALTHDROP = 50;        // Drop Range = 50-99
        public const int EXPLOSIONDROP = 51;
        public const int ENERGYDROP = 52;
        public const int ENEMY = 150;          // Enemy Range = 150-199

        public static readonly Dictionary<int, int> SPRITE =
  new Dictionary<int, int>
  {
                {BULLET, 0},
                {HOMINGBULLET, 1},
                {2,2},
                {EVILBULLET, 0},
                {ENEMY, ENEMY},
                {HEALTHDROP, 50},
                {EXPLOSIONDROP, 51},
                {ENERGYDROP, 52},
  };
    }
}
