using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class StandardEnemy : Enemy
    {
        public StandardEnemy(Vector2 position, ISprite sprite, Player player, int type) : base(position, sprite, player, type)
        {
        }

        public override void Death()
        {
            base.Death();
            Particles.GenerateParticles(Position, 2, angle, sprite.MColor); //Death animation
        }
    }
}
