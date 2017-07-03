using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class Speedy : Enemy
    {
        public Speedy(Vector2 position, ISprite sprite, Player player, int type) : base(position, sprite, player, type)
        {
            Thrust = 2.5f * EntityConstants.THRUST[EntityConstants.ENEMY];
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 17, angle, sprite.MColor); //Death animation
            base.Death();
        }
    }
}
