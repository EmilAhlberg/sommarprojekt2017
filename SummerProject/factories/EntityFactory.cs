using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.bullets;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;
        public static ActivatableEntity CreateEnemy(Sprite sprite, Player player, int type)
        {
            switch (type)
            {
                case (int)IDs.ENEMYSHOOT: return new Shooter(FarAway(), new Sprite(sprite), player);
                case (int)IDs.ENEMYSPEED: return new Speedy(FarAway(), new Sprite(sprite), player);
                case (int)IDs.ENEMYASTER: return new Asteroid(FarAway(), new Sprite(sprite), player);
            }
            return new StandardEnemy(FarAway(), new Sprite(sprite), player);
        }

        public static ActivatableEntity CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                #region Bullets
                case (int)IDs.DEFAULT_BULLET: return new Bullet(FarAway(), new Sprite(sprite), false);
                case (int)IDs.HOMINGBULLET: return new HomingBullet(FarAway(), new Sprite(sprite), false);
                case (int)IDs.SPRAYBULLET: return new SprayBullet(FarAway(), new Sprite(sprite), false);
                case (int)IDs.MINEBULLET: return new MineBullet(FarAway(), new Sprite(sprite), false);
                case (int)IDs.CHARGINGBULLET: return new ChargingBullet(FarAway(), new Sprite(sprite), false);
                #endregion
                #region Drops
                case (int)IDs.HEALTHDROP: return new HealthDrop(FarAway(), new Sprite(sprite),1);
                case (int)IDs.EXPLOSIONDROP: return new ExplosionDrop(FarAway(), new Sprite(sprite));
                case (int)IDs.ENERGYDROP: return new EnergyDrop(FarAway(), new Sprite(sprite));
                case (int)IDs.HEALTHDROP_TIER2: return new HealthDrop(FarAway(), new Sprite(sprite), 2);
                #endregion
                #region Enemy Bullets
                case (int)IDs.EVILBULLET: return new Bullet(FarAway(), new Sprite(sprite), true);
                #endregion
                default:
                    throw new NotImplementedException();
            }
        }

        public static Vector2 FarAway()
        {
            return new Vector2(standard, standard);
        }
    }
}
