using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    class HomingBullet : Projectile
    {
        private Entity target;
        private bool lockedOn;
        private const int homingDamage = 10;

        public HomingBullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
            InitDetection();
        }
        private void InitDetection()
        {
            AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, 300, 300), angle));
            Damage = 0; //!   big box must do 0 dmg bcuz detection
            lockedOn = false;
        }
        private void InitLockOn()
        {
            lockedOn = true;
            Damage = homingDamage;    
        }

        private void CalculateAngle()
        {
            float dX = Position.X - target.Position.X;
            float dY = Position.Y - target.Position.Y;
            base.CalculateAngle(dX, dY);
        }

        public override void Collision(Collidable c2)
        {
            if (!lockedOn)
            {
                if (c2 is Enemy && !IsEvil || c2 is Player && IsEvil)
                {
                    target = c2 as Entity;
                    InitLockOn();
                    RemoveBoundBox(1);
                }
                else if (c2 is Wall)
                    Death();
            }
            else if (lockedOn && (c2 is Enemy || c2 is Wall) )
            {
                Death();
                InitDetection();
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (lockedOn)
            {
                if (target.IsActive)
                    CalculateAngle();
               else
                    InitDetection();
            }
            UpdateTimer(gameTime);
            Move();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            IsActive = true;
            float dX = source.X - target.X;
            float dY = source.Y - target.Y;
            base.CalculateAngle(dX, dY);
            ResetSpawnTime();
        }
    }
}
