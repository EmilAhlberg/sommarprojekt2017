using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class Shooter : Enemy
    {
        public Shooter(Vector2 position, ISprite sprite, Player player, int type) : base(position, sprite, player, type)
        {
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 16, angle, sprite.MColor); //Death animation
            base.Death();
        }
    }
}
