using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SummerProject.collidables
{
    class Bullet : Projectile
    {
        public Bullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
            Damage = EntityConstants.DAMAGE[(int)IDs.DEFAULT_BULLET];
            Health = EntityConstants.HEALTH[(int)IDs.DEFAULT_BULLET];
            Mass = EntityConstants.MASS[(int)IDs.DEFAULT_BULLET];
            friction = EntityConstants.FRICTION[(int)IDs.DEFAULT_BULLET];
            if (isEvil)
            {
                sprite.MColor = Color.Red; //LOL
            }
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(Position, 6, Angle, Sprite.PrimaryColor);
            UpdateTimer(gameTime);
            Move();
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

        public override void Collision(Collidable c2)
        {
            if (c2 is Enemy && !IsEvil || c2 is Player && IsEvil || c2 is Wall)
            {
                Particles.GenerateDeathParticles(Sprite, Position, 2, Angle, false);
                Death();
            }
        }
    }
}
