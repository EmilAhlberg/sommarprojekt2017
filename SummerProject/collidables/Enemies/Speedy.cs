using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.enemies;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.Enemies
{
    class Speedy : Attacker
    {

        public Speedy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public override void Move()
        {
            if(waitTimer.IsFinished && !attackTimer.IsFinished)
                Hull.TakeAction(typeof(EnginePart));
        }

        protected override void Attack(GameTime gameTime)
        {
        }

        protected override void Wait(GameTime gameTime)
        {
        }
    }
}
