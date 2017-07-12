using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.Enemies
{
    class StandardEnemy : Enemy
    {
        public StandardEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public override void Move()
        {
            Hull.TakeAction(typeof(EnginePart));
        }
    }
}
