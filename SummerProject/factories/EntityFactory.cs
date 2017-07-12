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
                case (int)IDs.ENEMYSHOOT: e = new Shooter(FarAway(), player); e.AddPart(new SprayGunPart(), 1); e.AddPart(new EnginePart(), 3); return e;
                case (int)IDs.ENEMYSPEED: e = new Speedy(FarAway(), player); /*e.AddPart(new EnginePart(), 0);*/ e.AddPart(new EnginePart(), 1); e.AddPart(new EnginePart(), 2); e.AddPart(new EnginePart(), 0); e.AddPart(new EnginePart(), 3); return e;
                case (int)IDs.ENEMYASTER: return new Asteroid(FarAway(),  player);
            }
            e = new RandomEnemy(FarAway(), player); return e;
            //e = new StandardEnemy(FarAway(),  player); e.AddPart(new EnginePart(), 3); return e;
        }

        public static IActivatable CreateEnemyForLevel(Sprite sprite, Player player, int level)
        {
            Enemy e;
            switch (level%10)
            {
                case 1: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 2: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 3: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 4: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 5: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 6: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 7: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 8: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 9: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                case 10: e = new StandardEnemy(FarAway(), player); e.AddPart(new EnginePart(), 3); return e;
                default: throw new Exception();
            }
            e = new RandomEnemy(FarAway(), player); return e;
            //e = new StandardEnemy(FarAway(),  player); e.AddPart(new EnginePart(), 3); return e;
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
                case (int)IDs.HEALTHDROP: return new HealthDrop(FarAway(),1);
                case (int)IDs.EXPLOSIONDROP: return new ExplosionDrop(FarAway());
                case (int)IDs.ENERGYDROP: return new EnergyDrop(FarAway());
                case (int)IDs.HEALTHDROP_TIER2: return new HealthDrop(FarAway(), 2);
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
