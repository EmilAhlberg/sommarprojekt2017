using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.bullets;
using SummerProject.collidables.parts;
using SummerProject.collidables.enemies;
using SummerProject.collidables.enemies.SemiBosses;
using SummerProject.wave;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;

        public static IActivatable CreateEnemy(Sprite sprite, Player player, int type)
        {
            switch (type)
            {
                case (int)IDs.ENEMYASTER:
                    return new Asteroid(FarAway(), player);
                default: return new RandomEnemy(FarAway(), player);
            }
        }

        public static IActivatable CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                #region Bullets
                case (int)IDs.DEFAULT_BULLET: return new Bullet(FarAway());
                case (int)IDs.HOMINGBULLET: return new HomingBullet(FarAway());
                case (int)IDs.SPRAYBULLET: return new SprayBullet(FarAway());
                case (int)IDs.MINEBULLET: return new MineBullet(FarAway());
                case (int)IDs.CHARGINGBULLET: return new ChargingBullet(FarAway());
                case (int)IDs.GRAVITYBULLET: return new GravityBullet(FarAway());
                #endregion
                #region Drops
                case (int)IDs.HEALTHDROP: return new HealthDrop(FarAway());
                case (int)IDs.EXPLOSIONDROP: return new ExplosionDrop(FarAway());
                case (int)IDs.ENERGYDROP: return new EnergyDrop(FarAway());
                case (int)IDs.HEALTHDROP_TIER2: return new HealthDrop(FarAway(), IDs.HEALTHDROP_TIER2);
                case (int)IDs.MONEYDROP: return new MoneyDrop(FarAway());
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
