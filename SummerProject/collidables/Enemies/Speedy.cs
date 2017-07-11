using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.enemies;

namespace SummerProject.collidables.Enemies
{
    class Speedy : Attacker
    {

        public Speedy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        protected override void Attack(GameTime gameTime)
        {
            Move();
        }

        protected override void Wait(GameTime gameTime)
        {
            CalculateAngle();
            ThrusterAngle = Angle;
        }
    }
}
