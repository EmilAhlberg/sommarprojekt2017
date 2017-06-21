﻿using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;
        public static AIEntity CreateEnemy(Sprite sprite, Player player)
        {
            return new Enemy(FarAway(), new Sprite(sprite), player);

        }

        public static AIEntity CreateProjectile(Sprite sprite, int type)
        {
            switch (type)
            {
                case 0: return new Bullet(FarAway(), new Sprite(sprite));
                case 1: return new HomingBullet(FarAway(), new Sprite(sprite));
                default:
                    throw new NotImplementedException();
            }
        }

        public static AIEntity CreateDrop(Sprite sprite, int type)
        {
            switch (type)
            {
                case 0: return new HealthDrop(FarAway(), new Sprite(sprite));
                case 1: return new ExplosionDrop(FarAway(), new Sprite(sprite));
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
