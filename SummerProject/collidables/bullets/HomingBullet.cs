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

        public HomingBullet(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            Detector = new DetectorPart(750, 750, typeof(Enemy), this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Detector.Update(gameTime);
            UpdateTimer(gameTime);  
            Detector.Position = Position;
        }

        public void UpdateTarget(Entity target)
        {
            float dX = -target.Position.X;
            float dY = -target.Position.Y;
            base.CalculateAngle(dX, dY);
            AddForce(5, Angle);
        }
        public override void Death()
        {
            Detector.Death();
            base.Death();
        }
        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
          base.CalculateAngle(dX, dY);
            Stop();
            AddSpeed(6, Angle);
            ResetSpawnTime();
            Detector.Position = Position;
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Enemy || c2 is Wall)
            {
                Particles.GenerateParticles(Position, 5, 0, Sprite.MColor);
                Death();
            }
        }
    }
}
