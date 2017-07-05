using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    enum IDs
    {
        DEFAULT,
        PLAYER,

        #region Enemies
        DEFAULT_ENEMY,
        ENEMYSHOOT,
        ENEMYSPEED,
        ENEMYASTER,
        #endregion

        #region Bullets
        DEFAULT_BULLET,
        HOMINGBULLET,
        MINEBULLET,
        SPRAYBULLET,
        EVILBULLET,
        #endregion

        #region Drops
        DEFAULT_DROP,
        HEALTHDROP,
        HEALTHDROP_TIER2,
        EXPLOSIONDROP,
        ENERGYDROP,
        #endregion
    }
}
