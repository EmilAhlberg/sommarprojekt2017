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
        public ChargingBullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
        }

        protected override void TrailParticles()
        {
            Particles.GenerateAfterImageParticles(Sprite, Position, 6, Angle, Sprite.Scale);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
            base.CalculateAngle(dX, dY);
            Stop();
            AddSpeed(3*(10-Sprite.Scale.Length()), Angle); //!
            ResetSpawnTime();
        }
    }
}
