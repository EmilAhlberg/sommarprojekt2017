using Microsoft.Xna.Framework;
using SummerProject.collidables.bullets;
using SummerProject.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    class ChargingGunPart : GunPart
    {
        private bool charging;
        private bool buttonReleased;
        private Bullet bullet;
        private  float maxDamageScale = 6; // damage = maxDamageScale * initialDamage
        private const float maxScale = 5f;
        private const float initialDamage = 0.5f;
        private Timer chargeTimer;
        public ChargingGunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            chargeTimer = new Timer(3);
        }
        
        public override void TakeAction()
        {
            buttonReleased = false;
            if (!charging)
            {
                bullet = (ChargingBullet) projectiles.GetEntity((int) IDs.CHARGINGBULLET);
                charging = true;
            }
            Particles.GenerateParticles(Position, 3, angle, new Color(1, 0.15f + 0.85f * (1 - chargeTimer.currentTime / chargeTimer.maxTime), (1 - chargeTimer.currentTime / chargeTimer.maxTime), 0.5f + 0.5f*(1 - chargeTimer.currentTime / chargeTimer.maxTime)));
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
            if (charging) { 
                chargeTimer.CountDown(gameTime);
                if (buttonReleased && bullet != null &&  reloadTimer.IsFinished)
                {
                    projectiles.FireSpecificBullet(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), bullet);
                    ScaleBoundBox();
                    chargeTimer.Reset();
                    ResetForNextShot();
                    reloadTimer.Reset();
                }
            }
            buttonReleased = true;
        }

        private void ScaleBoundBox()
        {
           // bullet.Sprite.Scale = Vector2.One;
            float timeRatio = (1 - chargeTimer.currentTime / chargeTimer.maxTime);
            float scaleModifier = 1 + ((maxScale-1) * timeRatio);
            float damageModifier = 1 + ((maxDamageScale - 1) * timeRatio);
            bullet.Damage = initialDamage;
            bullet.BoundBox = new RotRectangle(new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, (int)(bullet.BoundBox.Width * scaleModifier), (int)(bullet.BoundBox.Height * scaleModifier)), bullet.Angle);
            bullet.Sprite.Scale *= scaleModifier;
            bullet.Damage *= damageModifier;
            bullet.AddSpeed(3 * (maxScale*2+1 - scaleModifier*2), Angle); //!
        }

        public void ResetForNextShot()
        {
            buttonReleased = false;
            charging = false;
            bullet = null;
        }
    }
}
