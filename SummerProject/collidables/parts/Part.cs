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
        public new float Mass { set { base.Mass = value; } get { return base.Mass; } }
        public virtual Color Color { set { sprite.MColor = value; } get { return sprite.MColor; } }
        public Part(ISprite sprite) : base(Vector2.Zero, sprite)
        {
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle));
        }

        public virtual void AddForce(float force, float angle) { base.AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }

        public void TurnTowardsVector(float dx, float dy) { base.CalculateAngle(dx, dy); }

        public override void Collision(Collidable c2)
        {
            Carrier.Collision(c2);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            throw new NotImplementedException();
        }

        public abstract void TakeAction(Type type);
    }
}
