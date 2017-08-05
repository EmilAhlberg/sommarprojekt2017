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
        int specialMove = 0;
        const int BOSS3 = 1;
        //protected float BOSSATTACKTIME { get; set; } = 1f;
        //protected Timer bossTimer;
        //protected float BOSSMOVETIME { get; set; } = 1f;
        //protected Timer bossTimer;

        public RandomEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public void FillParts(CompositePart p)
        {
            p.ResetParts();
            int level = GameMode.Level;
            p.AddPart(new EnginePart(), 3);
            switch (level)
            {
                case 1:
                    p.AddPart(new EnginePart(), 3);
                    break;
                case 2:
                    p.AddPart(new EnginePart(), 3);
                    break;
                case 3:
                    p.AddPart(new EnginePart(), 2);
                    p.AddPart(new EnginePart(), 0);
                    break;
                case 4:
                    p.AddPart(new EnginePart(), 2);
                    p.AddPart(new EnginePart(), 0);
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
                    break;
                case 6:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);
                    p.AddPart(new EnginePart(), 1);
                    p.AddPart(new EnginePart(), 2);
                    p.AddPart(new EnginePart(), 3);
                    usingWaitTimer = true;
                    break;
                case 7:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);
                    p.AddPart(new EnginePart(), 1);
                    p.AddPart(new EnginePart(), 2);
                    p.AddPart(new EnginePart(), 3);
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
                    p.AddPart(new EnginePart(), 0);
                    p.AddPart(new EnginePart(), 1);
                    p.AddPart(new EnginePart(), 2);
                    p.AddPart(new EnginePart(), 3);
                    break;
                case 10:
                    usingWaitTimer = true;
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY) * 50;
                    CompositePart u = new RectangularHull(IDs.BOSS1);
                    CompositePart d = new RectangularHull();
                    CompositePart l = new RectangularHull();
                    CompositePart r = new RectangularHull();
                    p.AddPart(u, 1);
                    p.AddPart(d, 3);
                    p.AddPart(l, 2);
                    p.AddPart(r, 0);
                    u.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);
                    u.AddPart(new EnginePart(IDs.TURBOENGINEPART), 1);
                    u.AddPart(new EnginePart(IDs.TURBOENGINEPART), 2);
                    d.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);
                    d.AddPart(new EnginePart(IDs.TURBOENGINEPART), 2);
                    d.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    l.AddPart(new EnginePart(IDs.TURBOENGINEPART), 2);
                    r.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);

                    break;
                case 11:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 12:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 13:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    break;
                case 14:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    break;
                case 15:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
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
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 16:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    p.AddPart(new GunPart(IDs.DEFAULT), 0);
                    p.AddPart(new GunPart(IDs.DEFAULT), 2);
                    usingWaitTimer = true;
                    break;
                case 17:
                    usingWaitTimer = true;
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    p.AddPart(new GunPart(IDs.DEFAULT), 0);
                    p.AddPart(new GunPart(IDs.DEFAULT), 2);
                    break;
                case 18:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new GunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 19:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    p.AddPart(new EnginePart(), 3);
                    if (n > 30)
                    {
                        p.AddPart(new GunPart(), 1);
                    }
                    if (n > 70)
                    {
                        p.AddPart(new GunPart(), 2); p.AddPart(new GunPart(), 0);
                    }
                    if (rnd.Next(0, 100) > 50)
                        usingWaitTimer = true;
                    break;
                case 20:
                    TurnSpeed = 0.05f * (float)Math.PI;
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY) * 100;
                    u = new RectangularHull();
                    CompositePart ul = new RectangularHull(IDs.BOSS2);
                    CompositePart ur = new RectangularHull();
                    l = new RectangularHull();
                    r = new RectangularHull();
                    CompositePart ld = new RectangularHull();
                    CompositePart rd = new RectangularHull();
                    p.AddPart(u, 1);
                    p.AddPart(r, 0);
                    p.AddPart(l, 2);
                    p.AddPart(new EnginePart(), 3);
                    u.AddPart(ur, 0);
                    u.AddPart(ul, 2);
                    u.AddPart(new GunPart(), 1);
                    ul.AddPart(new GunPart(), 1);
                    ur.AddPart(new GunPart(), 1);
                    l.AddPart(new GunPart(), 2);
                    r.AddPart(new GunPart(), 0);
                    l.AddPart(ld, 3);
                    r.AddPart(rd, 3);
                    ld.AddPart(new EnginePart(), 3);
                    rd.AddPart(new EnginePart(), 3);
                    break;
                case 21:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 22:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 23:
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    break;
                case 24:
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    break;
                case 25:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
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
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 26:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 2);
                    usingWaitTimer = true;
                    break;
                case 27:
                    usingWaitTimer = true;
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 2);
                    break;
                case 28:
                    p.AddPart(new EnginePart(), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 29:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    p.AddPart(new EnginePart(), 3);
                    if (n > 30)
                    {
                        usingWaitTimer = true;
                        p.AddPart(new SprayGunPart(), 1);
                    }
                    if (n > 70)
                    {
                        usingWaitTimer = true;
                        p.AddPart(new SprayGunPart(), 2);
                        p.AddPart(new SprayGunPart(), 0);
                    }
                    if (rnd.Next(0, 100) > 50)
                        usingWaitTimer = true;
                    break;

                case 30:
                    waitTimer.maxTime = 10;
                    attackTimer.maxTime = 6;
                    waitTimer.Reset();
                    attackTimer.Reset();
                    specialMove = BOSS3;
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY) * 100;
                    u = new RectangularHull();
                    d = new RectangularHull();
                    l = new RectangularHull();
                    r = new RectangularHull();
                    CompositePart uu = new RectangularHull(IDs.BOSS3);
                    CompositePart dd = new RectangularHull();
                    CompositePart ll = new RectangularHull();
                    CompositePart rr = new RectangularHull();
                    p.AddPart(u, 1);
                    p.AddPart(r, 0);
                    p.AddPart(l, 2);
                    p.AddPart(d, 3);
                    u.AddPart(uu, 1);
                    d.AddPart(dd, 3);
                    l.AddPart(ll, 2);
                    r.AddPart(rr, 0);
                    uu.AddPart(new SprayGunPart(), 1);
                    dd.AddPart(new SprayGunPart(), 3);
                    ll.AddPart(new SprayGunPart(), 2);
                    rr.AddPart(new SprayGunPart(), 0);
                    uu.AddPart(new EnginePart(), 0);
                    uu.AddPart(new EnginePart(), 2);
                    dd.AddPart(new EnginePart(), 0);
                    dd.AddPart(new EnginePart(), 2);
                    ll.AddPart(new EnginePart(), 1);
                    ll.AddPart(new EnginePart(), 3);
                    rr.AddPart(new EnginePart(), 1);
                    rr.AddPart(new EnginePart(), 3);
                    break;

                case 31:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 2);
                    usingWaitTimer = true;
                    break;
                case 32:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 2);
                    usingWaitTimer = true;
                    break;
                case 33:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 2);
                    break;
                case 34:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 2);
                    break;
                case 35:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    if (n < 50)
                        p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    else
                    {
                        p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 0);
                        p.AddPart(new EnginePart(), 2);
                    }
                    if (n > 65)
                    {
                        p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 1);
                        p.AddPart(new EnginePart(), 3);
                        usingWaitTimer = true;
                    }
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 36:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new SprayGunPart(IDs.DEFAULT), 2);
                    usingWaitTimer = true;
                    break;
                case 37:
                    usingWaitTimer = true;
                    attackTimer.maxTime = 3;
                    attackTimer.Reset();
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 1);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 0);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 2);
                    break;
                case 38:
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    p.AddPart(new ChargingGunPart(IDs.DEFAULT), 1);
                    usingWaitTimer = true;
                    break;
                case 39:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    p.AddPart(new EnginePart(IDs.TURBOENGINEPART), 3);
                    if (n > 30)
                    {
                        usingWaitTimer = true;
                        p.AddPart(new ChargingGunPart(), 1);
                    }
                    if (n > 70)
                    {
                        usingWaitTimer = true;
                        p.AddPart(new SprayGunPart(), 2);
                        p.AddPart(new SprayGunPart(), 0);
                    }
                    if (rnd.Next(0, 100) > 50)
                        usingWaitTimer = true;
                    break;

                case 40:
                    waitTimer.maxTime = 8;
                    attackTimer.maxTime = 4;
                    waitTimer.Reset();
                    attackTimer.Reset();
                    specialMove = BOSS3;
                    Damage = EntityConstants.GetStatsFromID(EntityConstants.DAMAGE, IDs.DEFAULT_ENEMY) * 100;
                    u = new RectangularHull();
                    d = new RectangularHull();
                    l = new RectangularHull();
                    r = new RectangularHull();
                    CompositePart ll2 = new RectangularHull();
                    CompositePart rr2 = new RectangularHull(IDs.BOSS4);
                    p.AddPart(u, 1);
                    p.AddPart(r, 0);
                    p.AddPart(l, 2);
                    p.AddPart(d, 3);
                    l.AddPart(ll2, 2);
                    r.AddPart(rr2, 0);
                    u.AddPart(new ChargingGunPart(), 3);
                    d.AddPart(new ChargingGunPart(), 1);
                    l.AddPart(new ChargingGunPart(), 0);
                    r.AddPart(new ChargingGunPart(), 2);
                    u.AddPart(new SprayGunPart(), 1);
                    d.AddPart(new SprayGunPart(), 3);
                    ll2.AddPart(new GunPart(), 2);
                    rr2.AddPart(new GunPart(), 0);
                    ll2.AddPart(new EnginePart(), 1);
                    ll2.AddPart(new EnginePart(), 3);
                    rr2.AddPart(new EnginePart(), 1);
                    rr2.AddPart(new EnginePart(), 3);
                    break;

                default:
                    rnd = new Random();
                    n = rnd.Next(0, 100);
                    p.AddPart(new EnginePart(), 3);
                    break;
            }
        }


        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            usingWaitTimer = false;
            specialMove = 0;
            FillParts(Hull);
        }

        protected override void Attack(GameTime gameTime)
        {
            Hull.TakeAction(typeof(SprayGunPart));
            Hull.TakeAction(typeof(MineGunPart));
            Hull.TakeAction(typeof(GunPart));
            Hull.TakeAction(typeof(ChargingGunPart));
        }

        protected override void Wait(GameTime gameTime)
        {
            switch (specialMove)
            {
                case BOSS3:
                    Hull.TakeAction(typeof(SprayGunPart));
                    Hull.TakeAction(typeof(MineGunPart));
                    Hull.TakeAction(typeof(GunPart));
                    break;
            }

        }

        protected override void CalculateAngle()
        {
            switch (specialMove)
            {
                case BOSS3:
                    if (waitTimer.IsFinished)
                        Hull.Angle += 0.1f;
                    else
                        Hull.Angle += 0.03f;
                    break;
                default:
                    base.CalculateAngle();
                    break;
            }

        }
        public override void Move()
        {
            switch (specialMove)
            {
                case BOSS3:
                    if (!waitTimer.IsFinished)
                    {
                        AddForce((player.Position - Position) / 10);
                    }
                    break;
                default:
                    if (!usingWaitTimer || (!waitTimer.IsFinished && !attackTimer.IsFinished))
                        Hull.TakeAction(typeof(EnginePart));
                    break;
            }

        }
    }
}
