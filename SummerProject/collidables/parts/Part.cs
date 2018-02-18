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
        public IPartCarrier Carrier { get; set; }
        public Vector2 AbsolutePosition { get { return BoundBox.AbsolutePosition; } }
        public virtual Color Color { set { Sprite.MColor = value; } get { return Sprite.MColor; } }

        public int LinkPosition { get; internal set; } = -1;

        public Part(IDs id = IDs.DEFAULT) : base(Vector2.Zero, id)
        {       
            IsActive = true;
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle));
        }

        public void TurnTowardsVector(float dx, float dy) { base.CalculateAngle(dx, dy); }

        public override void AddForce(float force, float angle)
        {
            if (Carrier is CompositePart)
                (Carrier as CompositePart).AddForce(force, angle);
            else
                base.AddForce(force, angle);
        }

        protected override void HandleCollision(ICollidable c2)
        {
            Carrier.Collision(c2);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            throw new NotImplementedException();
        }

        public abstract void TakeAction();
        
        public IPartCarrier GetController()
        {
            if (Carrier is Part)
                return ((Part)Carrier).GetController();
            return Carrier;
        }

        public override void Death()
        {
            Particles.GenerateDeathParticles(Sprite, AbsolutePosition, 2, Angle, true);
            base.Death();
        }
    }
}
