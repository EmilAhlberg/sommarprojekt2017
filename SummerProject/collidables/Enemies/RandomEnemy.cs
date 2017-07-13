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
        }

        public void FillParts(CompositePart p)
        {
            //int randomNumber;
            //int currentLevel = GameMode.Level; //hmm?
            //for (int i = 0; i < 2; i++)
            //{
            //    Part part1;
            //    Part part2;
            //    if (currentLevel / 10 != 0) //!!!temporary control of stuff
            //    {  
            //        randomNumber = SRandom.Next(1, 4);
            //        switch (randomNumber)
            //        {
            //            case 1: part1 = new MineGunPart(); part2 = new MineGunPart(); break;
            //            case 2: part1 = new SprayGunPart(); part2 = new SprayGunPart(); break;
            //            default: part1 = new EnginePart(); part2 = new EnginePart(); break;
            //        }
            //        if (i == 0)
            //        {
            //            p.AddPart(part1, 0);
            //            p.AddPart(part2, 2);
            //        }
            //        else
            //        {
            //            if (part1 is EnginePart)
            //            {
            //                p.AddPart(new SprayGunPart(), 1);
            //            }
            //            else
            //                p.AddPart(part1, 1);
            //        }
            //    }
            //}
            //p.AddPart(new EnginePart(), 3);
            p.ResetParts();
            int level = GameMode.Level;
            switch (level % 10)
            {
                case 1: p.AddPart(new EnginePart(), 3); break;
                case 2: p.AddPart(new EnginePart(), 3); break;
                case 3: p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0); break;
                case 4: p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0); break;
                case 5:
                    CompositePart u = new RectangularHull();
                    CompositePart l = new RectangularHull();
                    CompositePart r = new RectangularHull();
                    CompositePart ld = new RectangularHull();
                    CompositePart rd = new RectangularHull();
                    p.AddPart(u, 1);
                    p.AddPart(r, 0);
                    p.AddPart(l, 2);
                    p.AddPart(new EnginePart(), 3);
                    u.AddPart(new RectangularHull(), 0);
                    u.AddPart(new RectangularHull(), 2);
                    l.AddPart(ld, 3);
                    r.AddPart(rd, 3);
                    ld.AddPart(new EnginePart(), 3);
                    rd.AddPart(new EnginePart(), 3);
                    break;
                case 6: p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3); break;
                case 7: p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3); break;
                case 8: p.AddPart(new EnginePart(), 3); break;
                case 9:
                    Random rnd = new Random();
                    int n = rnd.Next(0, 100);
                    if (n < 50)
                    {
                        p.AddPart(new EnginePart(), 3); break;
                    }
                    if (n < 60)
                    {
                        p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0); break;
                    }
                    p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3); break;
                case 0:
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY)*2;
                    u = new RectangularHull();
                    CompositePart d = new RectangularHull();
                    l = new RectangularHull();
                    r = new RectangularHull();
                    p.AddPart(u, 1);
                    p.AddPart(d, 3);
                    p.AddPart(l, 2);
                    p.AddPart(r, 0);
                    u.AddPart(new EnginePart(), 0);
                    u.AddPart(new EnginePart(), 1);
                    u.AddPart(new EnginePart(), 2);
                    d.AddPart(new EnginePart(), 0);
                    d.AddPart(new EnginePart(), 2);
                    d.AddPart(new EnginePart(), 3);
                    l.AddPart(new EnginePart(), 2);
                    r.AddPart(new EnginePart(), 0);
                    //-----
                    break;
                default: throw new Exception();
            }
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
            if (!((Hull.Parts.Where(x => x is CompositePart).ToArray().Length == 1 && (Hull.Parts.Where(x => x is EnginePart).ToArray().Length == 4) || GameMode.Level == 10) && (waitTimer.IsFinished && !attackTimer.IsFinished)))
                Hull.TakeAction(typeof(EnginePart));
        }
    }
}
