using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class SprayBullet : Bullet
    {
        private const float randomAngleOffset = .3f;
        public SprayBullet(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite, isEvil)
        {
            Damage = 0.2f;
            
        }
        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = -target.X;
            float dY = -target.Y;
            CalculateAngle(dX, dY);
            Angle += (SRandom.NextFloat() - 0.5f) * randomAngleOffset;
            Stop();
            if (!IsEvil)
                AddSpeed(30, Angle); //!
            else
                AddSpeed(15, Angle);
            ResetSpawnTime();
        }
    }
}
