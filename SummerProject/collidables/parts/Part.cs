using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject
{
    public abstract class Part : Collidable
    {
        public IPartCarrier Carrier { set; get; }
        public new float Mass { set { base.Mass = value; } get { return base.Mass; } }
        public virtual Color Color { set { sprite.MColor = value; } get { return sprite.MColor; } }
        public Part(ISprite sprite) : base(Vector2.Zero, sprite)
        {
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle));
        }

        public void AddForce(float force, float angle) { base.AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }

        public void TurnTowardsVector(float dx, float dy) { base.CalculateAngle(dx, dy); }

        public virtual void Collission(Collidable c2)
        {
            Carrier.Collision(c2);
        }
    }
}
