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
            InitializeEntities((int)IDs.MINEBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.CHARGINGBULLET, defaultBulletCap);
            InitializeEntities((int)IDs.GRAVITYBULLET, defaultBulletCap);
            //Enemy.projectiles = this; //! Hmmmmm
        }

        public void AddExtraBullets(int bulletId, int amount)
        {
            AddExtraEntities(bulletId, amount);
        }

        public bool Fire(Vector2 source, Vector2 target, int bulletType)
        {
            return ActivateEntities(source, target, bulletType);
        }

        public void FireSpecificBullet(Vector2 source, Vector2 target, Projectile bullet)
        {
            bullet.Activate(source, target);
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
