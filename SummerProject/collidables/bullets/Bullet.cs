using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SummerProject.collidables
{
    class Bullet : Projectile
    {
        public Bullet(Vector2 position, bool isEvil) : base(position, isEvil)
        {
            if (isEvil)
            {
                Sprite.MColor = Color.Red; //LOL
            }

        }

        public override void Update(GameTime gameTime)
        {
            TrailParticles();
            UpdateTimer(gameTime);
            Move();
        }

        protected virtual void TrailParticles()
        {
            Particles.GenerateParticles(Position, 6, Angle, Sprite.PrimaryColor);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
            base.CalculateAngle(dX, dY);
            Stop();
            if (!IsEvil)
                AddSpeed(30,Angle); //!
            else
                AddSpeed(10,Angle);
            ResetSpawnTime(); 
        }
        public override void Death()
        {
            Health = EntityConstants.HEALTH[(int)IDs.DEFAULT_BULLET];
            base.Death();
        }

        public override void Collision(Collidable c2)
        {
            if (c2 is Enemy && !IsEvil || c2 is Player && IsEvil || c2 is Wall)
            {
                Particles.GenerateDeathParticles(Sprite, Position, 2, Angle, false);
             //   Health -= 1;
               // if (Health <= 0)
                    Death();
            }
        }
    }
}
