using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public enum IDs
    {
        DEFAULT = 1,
        PLAYER,
        WALL,

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
        CHARGINGBULLET,
        #endregion

        #region Drops
        DEFAULT_DROP,
        HEALTHDROP,
        HEALTHDROP_TIER2,
        EXPLOSIONDROP,
        ENERGYDROP,
        #endregion

        #region Particles
        DEFAULT_PARTICLE,
        WRENCH,
        BOLT,
        AFTERIMAGE,
        DEATH,
        #endregion 

        #region Parts
        DEFAULT_PART,
        RECTHULLPART,
        GUNPART,
        ENGINEPART,
        SPRAYGUNPART,
        MINEGUNPART,
        CHARGINGGUNPART,
        EMPTYPART,
        PART
        #endregion
    }
}
