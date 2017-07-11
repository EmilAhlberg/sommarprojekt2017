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
        public int Heal { get; private set; }
        public HealthDrop(Vector2 position, int tier) : base(position)
        {
            if (tier == 2)
                Heal = 5;
            else
                Heal = 1;
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 8); //Death animation
            base.Death();
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Player)
            {
                Player p = c2 as Player;
                p.Health += Heal;
            }
            base.HandleCollision(c2);
        }
    }
}
