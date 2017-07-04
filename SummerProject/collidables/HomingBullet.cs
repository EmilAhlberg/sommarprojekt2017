using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.collidables
{
    class HomingBullet : Projectile, ITargeting
    {
        public DetectorPart Detector { get; private set; }

        public HomingBullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
            Damage = EntityConstants.DAMAGE[EntityConstants.BULLET];
            Health = EntityConstants.HEALTH[EntityConstants.BULLET];
            Mass = EntityConstants.MASS[EntityConstants.BULLET];
            Thrust = EntityConstants.THRUST[EntityConstants.BULLET];
            friction = EntityConstants.FRICTION[EntityConstants.BULLET];
            Detector = new DetectorPart(750, 750, typeof(Enemy), this);
        }


        public override void Collision(Collidable c2)
        {
            if (c2 is Enemy || c2 is Wall)
            {
                Particles.GenerateParticles(Position, 5, 0, sprite.MColor);
                Death();
            }
        }

        public override void Update(GameTime gameTime)
        {
            Detector.Update(gameTime);
            UpdateTimer(gameTime);  
            base.Move();
            Detector.Position = Position;
        }

        public void UpdateTarget(Entity target)
        {
            float dX = -target.Position.X;
            float dY = -target.Position.Y;
            base.CalculateAngle(dX, dY);
        //    AddForce(angle, 1);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
          base.CalculateAngle(dX, dY);
            Stop();
            AddSpeed(6, angle);
            ResetSpawnTime();
            Detector.Position = Position;
        }
    }
}
