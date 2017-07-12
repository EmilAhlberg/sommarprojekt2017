using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.achievements;

namespace SummerProject.collidables
{
    class MoneyDrop : Drop
    {
        public const int value = 1000;
        public MoneyDrop(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 5); //Death animation
            base.Death();
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Player)
                Traits.CURRENCY.Counter += value;
            base.HandleCollision(c2);
        }
    }
}
