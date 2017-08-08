using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class Asteroid : Enemy
    {
        private float spriteRotSpeed;
        private const float randomAngleOffsetMultiplier = .3f;

        public Asteroid(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        protected override void CalculateAngle()
        {
              Angle += spriteRotSpeed;
        }

        protected override void Enrage()
        {
            Death();
        }

        public override void Update(GameTime gameTime)
        {
            if (WindowSize.IsOutOfBounds(Position))
                Death();
            base.Update(gameTime);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            base.CalculateAngle();
            spriteRotSpeed = 0.05f * SRandom.NextFloat();
            Angle += randomAngleOffsetMultiplier * SRandom.NextFloat();
            friction = 0f;
            AddSpeed(5, Angle);
        }

        public override void Death()
        {
            //Particles.GenerateParticles(Position, 18, angle, sprite.MColor); //Death animation
            if (WindowSize.IsOutOfBounds(Position))
                OutOfBoundsDeath();
            else
            base.Death();
        }
        public override void Move()
        {
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Projectile)
            {
                Projectile b2 = c2 as Projectile;
                float speedPerHit;
                if (b2 is SprayBullet)
                    speedPerHit = 0.5f;
                else
                    speedPerHit = 2f;
                AddSpeed(speedPerHit, b2.Angle);
            }
            base.HandleCollision(c2);
        }
    }
}
