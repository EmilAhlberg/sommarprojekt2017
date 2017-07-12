using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public class Projectiles : Entities
    {
        private const int defaultBulletCap = 10;
        public Projectiles() : base()
        {
            InitializeEntities((int)IDs.DEFAULT_BULLET, defaultBulletCap);
            InitializeEntities((int)IDs.HOMINGBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.SPRAYBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.EVILBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.MINEBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.CHARGINGBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.GRAVITY_BULLET, defaultBulletCap);
            //Enemy.projectiles = this; //! Hmmmmm
        }

        public void AddExtraBullets(int bulletId, int amount)
        {
            AddExtraEntities(bulletId, amount);
        }

        public bool Fire(Vector2 source, Vector2 target, int bulletType)
        {

                if (ActivateEntities(source, target, bulletType))
                {
                    return true; //bool type: only because of players shotsFiredTrait
                }
            return false;

        }

        public void FireSpecificBullet (Vector2 source, Vector2 target, Projectile bullet)
        {
            bullet.Activate(source, target);
        }


        public void EvilFire(Vector2 source, Vector2 target)
        {
            ActivateEntities(source, target, (int)IDs.EVILBULLET);
        }

        public override void Reset()
        {
            ResetEntities();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
        }
        public override IActivatable CreateEntity(int type)
        {
            return EntityFactory.CreateEntity(SpriteHandler.GetSprite(type), type); //! LOL
        }
    }
}
