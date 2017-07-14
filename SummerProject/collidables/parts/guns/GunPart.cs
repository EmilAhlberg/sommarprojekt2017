using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.factories;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using SummerProject.achievements;

namespace SummerProject.collidables.parts
{
    class GunPart : Part
    {
        public static Projectiles projectiles;
        protected float RELOADTIME = 0.3f; //!
        protected Timer reloadTimer;
        protected Projectile bullet;
        protected IDs bulletID;
        protected int BulletSpeed { get; set; } = 20;
        protected int EvilBulletSpeed { get; set; } = 10;

        protected int bulletCap = 5;

        public GunPart(IDs id) : this(id, IDs.DEFAULT_BULLET)
        {
 
        }
        public GunPart(IDs id = IDs.DEFAULT, IDs bulletID = IDs.DEFAULT_BULLET) : base(id)
        {
            reloadTimer = new Timer(RELOADTIME);
            reloadTimer.Finish();
            this.bulletID = bulletID;
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
            if(!IsEvil)
                Traits.SHOTSFIRED.Counter++;
            SoundHandler.PlaySoundEffect((int)id);
            EditBullet();
            bullet = (Projectile)projectiles.GetEntity((int)bulletID);
        }

        protected virtual void EditBullet()
        {
            bullet.IsEvil = IsEvil;
            if (IsEvil)
                bullet.AddSpeed(EvilBulletSpeed, Angle);
            else
                bullet.AddSpeed(BulletSpeed, Angle);
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
