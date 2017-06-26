using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class EnergyDrop : Drop
    {
        public const int charge = 50;
        public EnergyDrop(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }
        public override void Collision(Collidable c2)
        {
            if (c2 is Player)
            {
                Death();
            }
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 8); //Death animation
            base.Death();
        }
    }
}
