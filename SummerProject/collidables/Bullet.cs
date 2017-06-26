using System;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class Bullet : Projectile
    {

        public Bullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
            Damage = EntityConstants.DAMAGE[EntityConstants.BULLET];
            Health = EntityConstants.HEALTH[EntityConstants.BULLET];
            Mass = EntityConstants.MASS[EntityConstants.BULLET];
            Thrust = EntityConstants.THRUST[EntityConstants.BULLET];
            if (isEvil)
            {
                sprite.MColor = Color.Red; //LOL
            }
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(Position, 6, angle);
            UpdateTimer(gameTime);
            Move();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = source.X - target.X;
            float dY = source.Y - target.Y;
            base.CalculateAngle(dX, dY);
            Stop();
            AddSpeed(30); //!
            ResetSpawnTime(); 
        }

        public override void Collision(Collidable c2)
        {
            if(c2 is Enemy && !IsEvil || c2 is Player && IsEvil || c2 is Wall)
            {
                Particles.GenerateParticles(Position, 5);
                Death();
            }
        }
    }
}
