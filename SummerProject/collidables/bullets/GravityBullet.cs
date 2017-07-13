using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.util;
using SummerProject.factories;

namespace SummerProject.collidables.bullets
{
    class GravityBullet : Bullet
    {
        private float health = 1;
        public override float Health
        {
            get
            {
                return health;
            }
        }

        public GravityBullet(Vector2 position) : base(position)
        {
        }
        public override bool CollidesWith(ICollidable c2)
        {
            Part c3;
            int nrParts;
            if (c2 is Part)
            {
                nrParts = (((c2 as Part).GetController()) as PartController).Parts.Count;
                c3 = c2 as Part;
            }
            else
                return base.CollidesWith(c2);
            Vector2 dist = (BoundBox.AbsolutePosition- c3.BoundBox.AbsolutePosition);
            //float r2 = dist.LengthSquared();
            //dist.Normalize();
            //c3.AddForce(20000*dist * Mass * c3.Mass / r2);
            float r2 = dist.Length();
            dist.Normalize();
            if (r2 > 10)
                c3.AddForce(3000 * dist / r2);
            else
                (c3.GetController() as PartController).Health -= 1;
            return base.CollidesWith(c2);
        }

        protected override void TrailParticles()
        {
            Particles.GenerateParticles(Position, 10, Angle, Sprite.PrimaryColor);
        }

        protected override void HandleCollision(ICollidable c2)
        {
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            SoundHandler.PlayLoopedSoundEffect((int)id);
            base.SpecificActivation(source, target);
        }

        public override void Death()
        {
            SoundHandler.StopLoopedSoundEffect((int)id);
            base.Death();
        }
    }
}
