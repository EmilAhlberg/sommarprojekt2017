using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.Enemies;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;
        public static ActivatableEntity CreateEnemy(Sprite sprite, Player player, int type)
        {
            switch (type)
            {
                case 151: return new Shooter(FarAway(), new Sprite(sprite), player);
                case 152: return new Speedy(FarAway(), new Sprite(sprite), player);
                case 153: return new Asteroid(FarAway(), new Sprite(sprite), player);
            }
            return new StandardEnemy(FarAway(), new Sprite(sprite), player);
        }

        public static ActivatableEntity CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                #region Bullets
                case 0: return new Bullet(FarAway(), new Sprite(sprite), false);
                case 1: return new HomingBullet(FarAway(), new Sprite(sprite), false);
                case 2: return new SprayBullet(FarAway(), new Sprite(sprite), false);
                case 4: return new Bullet(FarAway(), new Sprite(sprite), false);
                #endregion
                #region Drops
                case 50: return new HealthDrop(FarAway(), new Sprite(sprite),1);
                case 51: return new ExplosionDrop(FarAway(), new Sprite(sprite));
                case 52: return new EnergyDrop(FarAway(), new Sprite(sprite));
                case 53: return new HealthDrop(FarAway(), new Sprite(sprite), 2);
                #endregion
                #region Enemy Bullets
                case 100: return new Bullet(FarAway(), new Sprite(sprite), true);
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
