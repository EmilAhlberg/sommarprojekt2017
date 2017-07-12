using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.util;

namespace SummerProject.collidables.bullets
{
    class GravityBullet : Bullet
    {
        public GravityBullet(Vector2 position) : base(position)
        {
        }
        public override bool CollidesWith(ICollidable c2)
        {
            Collidable c3;
            if (c2 is Part)
                c3 = c2 as Collidable;
            else if (c2 is Enemy)
                c3 = (c2 as PartController).Hull as Collidable;
            else
                return base.CollidesWith(c2);
            Vector2 dist = (Position - c3.Position);
            //float r2 = dist.LengthSquared();
            //dist.Normalize();
            //c3.AddForce(20000*dist * Mass * c3.Mass / r2);
            float r2 = dist.Length();
            dist.Normalize();
            c3.AddForce(5000 * dist / r2);
            return base.CollidesWith(c2);
        }
    }
}
