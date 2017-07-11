using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.enemies
{
    class RandomEnemy : Enemy
    {
        public RandomEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            fillParts(Hull);
        }

        public void fillParts(CompositePart p)
        {
            Random random = new Random();
            int randomNumber;
            for (int i = 0; i < 4; i++)
            {
                randomNumber = random.Next(1, 5);
                switch (randomNumber)
                {
                    case 1: p.AddPart(new MineGunPart(), i); break;
                    case 2: p.AddPart(new SprayGunPart(), i); break;
                    case 3: p.AddPart(new EnginePart(), i); break;
                    default: p.AddPart(new EnginePart(), i); break;
                }
            }
            p.AddPart(new EnginePart(), 3);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            fillParts(Hull);
        }
    }
}
