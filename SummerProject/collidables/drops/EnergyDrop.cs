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
        public const int charge = 10;
        public EnergyDrop(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 12); //Death animation
            base.Death();
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Player)
                (c2 as Player).Energy += charge;
            base.HandleCollision(c2);
        }
    }
}
