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
        BOSS1,
        BOSS2,
        BOSS3,
        BOSS4,
        BOSS5,
        #endregion

        #region Bullets
        DEFAULT_BULLET,
        HOMINGBULLET,
        MINEBULLET,
        SPRAYBULLET,
        CHARGINGBULLET,
        GRAVITYBULLET,

        #endregion

        #region Drops
        DEFAULT_DROP,
        HEALTHDROP,
        HEALTHDROP_TIER2,
        MONEYDROP,
        ENERGYDROP, //ENERGYDROP has to be the last drop in this enum
        #endregion

        #region Particles
        DEFAULT_PARTICLE,
        WRENCH,
        BOLT,
        MONEY,
        AFTERIMAGE,
        ALERTPARTICLE,
        DEATH,
        #endregion

        #region Parts
        DEFAULT_PART,
        RECTHULLPART,
        GUNPART,
        GRAVITYGUNPART,
        ENGINEPART,
        SPRAYGUNPART,
        MINEGUNPART,
        CHARGINGGUNPART,
        EMPTYPART,
        #endregion

        UPGRADEBAR,
        MENUSCREENBKG,
        MENUCLICK,
        ROTATEPART,
        HAMMERPART,
        EXPLOSIONDROP, //NOT USED
        LOGO,
        POPUPTEXTBKG,
        EXPLOSIONDEATHSOUND,

        #region Music
        SONG1,
        SONG1INTRO,
        SONG3,    
        SONG2,
        VICTORY,
        GAMEOVER,
        PLAYERDEATHSOUND,
        SLOWMOSOUND,
        REVERSESLOWMOSOUND,
        PICKUPSOUND,
        TURBOENGINEPART,
        #endregion
    }
}
