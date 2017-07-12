using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.factories;
using Microsoft.Xna.Framework.Input;

namespace SummerProject.collidables.parts
{
    class GunPart : Part
    {
        public static Projectiles projectiles;
        protected float RELOADTIME = 0.3f; //!
        protected Timer reloadTimer;
        protected Projectile bullet;
        protected IDs bulletID;

        protected int bulletCap = 5;

        public GunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            reloadTimer = new Timer(RELOADTIME);
            bulletID = IDs.DEFAULT_BULLET;
            projectiles.AddExtraBullets((int)bulletID, bulletCap);
            bullet = (Projectile)projectiles.GetEntity((int)bulletID);
        }

        public override void TakeAction()
        {
            if (reloadTimer.IsFinished)
            {
                Fire();
                reloadTimer.Reset();
            }
        }
        protected virtual void Fire()
        {        
            projectiles.FireSpecificBullet(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), bullet);
            EditBullet();
            bullet = (Projectile)projectiles.GetEntity((int)bulletID);
        }

        protected virtual void EditBullet()
        {
            bullet.IsEvil = IsEvil;
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
        }

        protected override void HandleCollision(ICollidable c2)
        {
            base.HandleCollision(c2);
        }
    }
}
