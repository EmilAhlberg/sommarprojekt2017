using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.bullets;
using SummerProject.collidables.parts;
using SummerProject.collidables.enemies;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;
        public static IActivatable CreateEnemy(Sprite sprite, Player player, int type)
        {
            Enemy e;
            switch (type)
            {
                case (int)IDs.ENEMYSHOOT: e = new StandardEnemy(FarAway(), player); e.AddPart(new SprayGunPart(),1); e.AddPart(new EnginePart(), 3); return e;
                case (int)IDs.ENEMYSPEED: e = new StandardEnemy(FarAway(), player); /*e.AddPart(new EnginePart(), 0);*/ e.AddPart(new EnginePart(), 2); e.AddPart(new EnginePart(), 0); return e;
                case (int)IDs.ENEMYASTER: return new Asteroid(FarAway(),  player);
            }
            e = new RandomEnemy(FarAway(), player); return e;
            e = new StandardEnemy(FarAway(),  player); e.AddPart(new EnginePart(), 3); return e;
        }

        public static IActivatable CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                #region Bullets
                case (int)IDs.DEFAULT_BULLET: return new Bullet(FarAway(),  false);
                case (int)IDs.HOMINGBULLET: return new HomingBullet(FarAway(),  false);
                case (int)IDs.SPRAYBULLET: return new SprayBullet(FarAway(),  false);
                case (int)IDs.MINEBULLET: return new MineBullet(FarAway(),  false);
                case (int)IDs.CHARGINGBULLET: return new ChargingBullet(FarAway(),  false);
                #endregion
                #region Drops
                case (int)IDs.HEALTHDROP: return new HealthDrop(FarAway(),1);
                case (int)IDs.EXPLOSIONDROP: return new ExplosionDrop(FarAway());
                case (int)IDs.ENERGYDROP: return new EnergyDrop(FarAway());
                case (int)IDs.HEALTHDROP_TIER2: return new HealthDrop(FarAway(), 2);
                #endregion
                #region Enemy Bullets
                case (int)IDs.EVILBULLET: return new Bullet(FarAway(), true);
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
