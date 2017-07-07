using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;
using SummerProject.collidables;

namespace SummerProject
{
    public abstract class Part : ActivatableEntity
    {
        public IPartCarrier Carrier { set; get; }
        public Vector2 AbsolutePosition { get { return BoundBox.AbsolutePosition; } }
        public new float Mass { set { base.Mass = value; } get { return base.Mass; } }
        public virtual Color Color { set { Sprite.MColor = value; } get { return Sprite.MColor; } }

        public int LinkPosition { get; internal set; } = -1;

        public Part(IDs id = IDs.DEFAULT) : base(Vector2.Zero, id)
        {
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle));
        }

        public void TurnTowardsVector(float dx, float dy) { base.CalculateAngle(dx, dy); }

        public override void Collision(Collidable c2)
        {
            Carrier.Collision(c2);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            throw new NotImplementedException();
        }

        public abstract void TakeAction();

        public override void Death()
        {
            Particles.GenerateDeathParticles(Sprite, AbsolutePosition, 2, Angle, true);
            base.Death();
        }
    }
}
