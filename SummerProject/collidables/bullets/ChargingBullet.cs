using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.bullets
{
    class ChargingBullet : Bullet
    {
        public ChargingBullet(Vector2 position) : base(position)
        {
        }

        protected override void TrailParticles()
        {
            Particles.GenerateAfterImageParticles(Sprite, Position, 6, Angle, Sprite.Scale);
        }
        public override void Death()
        {
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), Sprite.SpriteRect.Width, Sprite.SpriteRect.Height), angle);
            BoundBox.Origin = Sprite.Origin;
            Sprite.Scale = Vector2.One;
            base.Death();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
            base.CalculateAngle(dX, dY);
            Stop();
            ResetSpawnTime();
        }
    }
}
