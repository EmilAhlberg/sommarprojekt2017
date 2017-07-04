using System.Collections.Generic;

namespace SummerProject.factories
{
    class EntityTypes
    {
        public const int ARRAYSIZE = 200;
        public const int BULLET = 0;            // Bullet range = 0-49
        public const int HOMINGBULLET = 1;
        public const int SPRAYBULLET = 2;
        public const int EVILBULLET = 100;        // Enemy Bullet Range = 100 - 149
        public const int HEALTHDROP = 50;        // Drop Range = 50-99
        public const int HEALTHDROP_TIER2 = 53;
        public const int EXPLOSIONDROP = 51;
        public const int ENERGYDROP = 52;
        public const int ENEMY = 150;          // Enemy Range = 150-199
        public const int ENEMYSHOOT = 151;
        public const int ENEMYSPEED = 152;
        public const int ENEMYASTER = 153;

        public static readonly Dictionary<int, int> SPRITE =
  new Dictionary<int, int>
  {
                {BULLET, BULLET},
                {HOMINGBULLET, HOMINGBULLET},
                {SPRAYBULLET,SPRAYBULLET },
                {EVILBULLET, BULLET},
                {ENEMY, ENEMY},
                {HEALTHDROP, 50},
                {EXPLOSIONDROP, 51},
                {ENERGYDROP, 52},
                {HEALTHDROP_TIER2, 53},
                {ENEMYSHOOT, ENEMYSHOOT},
                {ENEMYSPEED, ENEMYSPEED},
                {ENEMYASTER, ENEMYASTER},

  };

    }
}
