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
        bool usingWaitTimer = false;
        public RandomEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public void FillParts(CompositePart p)
        {
            p.ResetParts();
            int level = GameMode.Level;
            switch (level % 10)
            {
                case 1:
                    p.AddPart(new EnginePart(), 3);
                    if(level > 10)
                    {
                        p.AddPart(new GunPart(), 1);
                    }

                    break;
                case 2:
                    p.AddPart(new EnginePart(), 3);
                    if (level > 10)
                    {
                        p.AddPart(new GunPart(), 1);
                    }

                    break;
                case 3:
                    p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0);
                    if (level > 10)
                    {
                        p.AddPart(new GunPart(), 1);
                    }

                    break;
                case 4:
                    p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0);
                    if (level > 10)
                    {
                        p.AddPart(new GunPart(), 1);
                    }

                    break;
                case 5:
                    Random rnd = new Random();
                    int n = rnd.Next(0, 100);
                    if (n < 50)
                        p.AddPart(new EnginePart(), 3); 
                    else
                    {
                        p.AddPart(new EnginePart(), 0);
                        p.AddPart(new EnginePart(), 2);
                    }
                    if (n > 65)
                    {
                        p.AddPart(new EnginePart(), 1);
                        p.AddPart(new EnginePart(), 3);
                        usingWaitTimer = true;
                    }

                    if (level > 10)
                    {
                        p.AddPart(new GunPart(), 1);
                    }
                    break;
                    //maxHealth = 5;
                    //Health = 5;
                    //CompositePart u = new RectangularHull();
                    //CompositePart l = new RectangularHull();
                    //CompositePart r = new RectangularHull();
                    //CompositePart ld = new RectangularHull();
                    //CompositePart rd = new RectangularHull();
                    //p.AddPart(u, 1);
                    //p.AddPart(r, 0);
                    //p.AddPart(l, 2);
                    //p.AddPart(new EnginePart(), 3);
                    //u.AddPart(new RectangularHull(), 0);
                    //u.AddPart(new RectangularHull(), 2);
                    //l.AddPart(ld, 3);
                    //r.AddPart(rd, 3);
                    //ld.AddPart(new EnginePart(), 3);
                    //rd.AddPart(new EnginePart(), 3);

                case 6:
                    p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3);
                    usingWaitTimer = true;
                    break;

                case 7:
                    p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3);
                    usingWaitTimer = true;
                    break;

                case 8:
                    p.AddPart(new EnginePart(), 3);

                    break;

                case 9:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    if (n < 50)
                    {
                        p.AddPart(new EnginePart(), 3); break;
                    }
                    if (n < 60)
                    {
                        p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 0); break;
                    }
                    usingWaitTimer = true;
                    p.AddPart(new EnginePart(), 0); p.AddPart(new EnginePart(), 1); p.AddPart(new EnginePart(), 2); p.AddPart(new EnginePart(), 3);

                    break;

                case 0:
                    usingWaitTimer = true;
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY)*2;
                    CompositePart u = new RectangularHull();
                    CompositePart d = new RectangularHull();
                    CompositePart l = new RectangularHull();
                    CompositePart r = new RectangularHull();
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
            usingWaitTimer = false;
            FillParts(Hull);
        }

        protected override void Attack(GameTime gameTime)
        {
        }

        protected override void Wait(GameTime gameTime)
        {
            Hull.TakeAction(typeof(SprayGunPart));
            Hull.TakeAction(typeof(MineGunPart));
            Hull.TakeAction(typeof(GunPart));
        }

        public override void Move()
        {
            if (!usingWaitTimer || (waitTimer.IsFinished && !attackTimer.IsFinished))
                Hull.TakeAction(typeof(EnginePart));
        }
    }
}
