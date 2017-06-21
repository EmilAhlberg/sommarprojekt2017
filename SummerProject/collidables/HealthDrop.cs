using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class HealthDrop : Drop
    {
        private const int heal = 1;
        public HealthDrop(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }
        public override void Collision(Collidable c2)
        {
            if (c2 is Player)
            {
                ((Player)c2).Health += heal; 
                Particles.GenerateParticles(Position, 8);
                Death();
            }
        }
    }
}
