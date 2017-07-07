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
        private const float maxScale = 5;
        private const float initialDamage = 0.5f;
        private Timer chargeTimer;
        public ChargingGunPart(ISprite sprite) : base(sprite)
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
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
            if (charging) { 
                chargeTimer.CountDown(gameTime);
            
                if (buttonReleased == true && bullet != null &&  reloadTimer.IsFinished)
                {
                    ScaleBoundBox();
                    projectiles.FireSpecificBullet(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), bullet);
                    charging = false;
                    chargeTimer.Reset();
                    ResetForNextShot();
                    reloadTimer.Reset();
                }
            }
            buttonReleased = true;
        }

        private void ScaleBoundBox()
        {
            float timeRatio = (1 - chargeTimer.currentTime / chargeTimer.maxTime);
            float scaleModifier = 1 + ((maxScale-1) * timeRatio);
            float damageModifier = 1 + ((maxDamageScale - 1) * timeRatio);
            bullet.Damage = initialDamage;
            bullet.BoundBox = new RotRectangle(new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, (int)(bullet.BoundBox.Width * scaleModifier), (int)(bullet.BoundBox.Height * scaleModifier)), bullet.Angle);
            bullet.Sprite.Scale *= scaleModifier;
            bullet.Damage *= damageModifier;
        }

        public void ResetForNextShot()
        {
            buttonReleased = false;
            charging = false;
            bullet = null;
        }
    }
}
