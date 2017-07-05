using System.Collections.Generic;

namespace SummerProject.factories
{
    class SpriteHandler
    {
        private const int ARRAYSIZE = 200;
        public static Sprite[] Sprites = new Sprite[ARRAYSIZE];
        public static readonly Dictionary<int, int> SPRITE =
            new Dictionary<int, int>
            {
                {(int)IDs.DEFAULT_BULLET, (int)IDs.DEFAULT_BULLET},
                {(int)IDs.HOMINGBULLET, (int)IDs.HOMINGBULLET},
                {(int)IDs.SPRAYBULLET, (int)IDs.SPRAYBULLET},
                {(int)IDs.MINEBULLET, (int)IDs.MINEBULLET},
                {(int)IDs.CHARGINGBULLET, (int)IDs.DEFAULT_BULLET},
                {(int)IDs.EVILBULLET, (int)IDs.DEFAULT_BULLET},
                {(int)IDs.DEFAULT_ENEMY, (int)IDs.DEFAULT_ENEMY},
                {(int)IDs.HEALTHDROP, (int)IDs.HEALTHDROP},
                {(int)IDs.EXPLOSIONDROP, (int)IDs.EXPLOSIONDROP},
                {(int)IDs.ENERGYDROP, (int)IDs.ENERGYDROP},
                {(int)IDs.HEALTHDROP_TIER2, (int)IDs.HEALTHDROP_TIER2},
                {(int)IDs.ENEMYSHOOT, (int)IDs.ENEMYSHOOT},
                {(int)IDs.ENEMYSPEED, (int)IDs.ENEMYSPEED},
                {(int)IDs.ENEMYASTER, (int)IDs.ENEMYASTER},

            };


    }
}
