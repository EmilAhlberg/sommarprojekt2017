using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;
using SummerProject.wave;

namespace SummerProject.collidables.enemies
{
    class RandomEnemy : Attacker
    {
        public RandomEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            FillParts(Hull);
        }

        public void FillParts(CompositePart p)
        {
            int randomNumber;
            int currentLevel = GameMode.Level; //hmm?
            for (int i = 0; i < 2; i++)
            {
                Part part1;
                Part part2;
                if (currentLevel / 10 != 0) //!!!temporary control of stuff
                {  
                    randomNumber = SRandom.Next(1, 4);
                    switch (randomNumber)
                    {
                        case 1: part1 = new MineGunPart(); part2 = new MineGunPart(); break;
                        case 2: part1 = new SprayGunPart(); part2 = new SprayGunPart(); break;
                        default: part1 = new EnginePart(); part2 = new EnginePart(); break;
                    }
                    if (i == 0)
                    {
                        p.AddPart(part1, 0);
                        p.AddPart(part2, 2);
                    }
                    else
                    {
                        if (part1 is EnginePart)
                        {
                            p.AddPart(new SprayGunPart(), 1);
                        }
                        else
                            p.AddPart(part1, 1);
                    }
                }
            }
            p.AddPart(new EnginePart(), 3);
        }


        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            FillParts(Hull);
        }

        protected override void Attack(GameTime gameTime)
        {
            Hull.TakeAction(typeof(SprayGunPart));
            Hull.TakeAction(typeof(MineGunPart));
            Hull.TakeAction(typeof(GunPart));
        }

        protected override void Wait(GameTime gameTime)
        {
        }

        public override void Move()
        {
            Hull.TakeAction(typeof(EnginePart));
        }
    }
}
