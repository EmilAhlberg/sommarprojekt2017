using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class PartController : ActivatableEntity, IPartCarrier
    {
        public CompositePart Hull { get; set; }
        public override Vector2 Position { get { return Hull.Position; } set { Hull.Position = value; } }
        public IEnumerable<Collidable> Collidables { get { return Parts; } }
        public List<Part> Parts { get { return Hull.Parts; } }

        public PartController(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            Hull = new RectangularHull();
            Hull.Carrier = this;
            Position = position;
        }

        public override float Angle
        {
            set
            {
                Hull.Angle = value;
            }
            get
            {
                return Hull.Angle;
            }

        }

        protected override float TurnSpeed { set { Hull.TurnSpeed = value; } get { return Hull.TurnSpeed; } }
        public override float friction { set { Hull.friction = value; } get { return Hull.friction; } }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Hull.Update(gameTime);
            if (Health <= 0 && IsActive)
                Death();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Hull.Draw(spriteBatch, gameTime);
        }

        protected abstract void CalculateAngle();

        public override void Death()
        {
            IsActive = false;
            Hull.Death();
            Stop();
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }

        public override void Stop()
        {
            Hull.Stop();
        }

        public override void AddSpeed(float speed, float angle)
        {
            Hull.AddSpeed(speed, angle);
        }

        public override void AddForce(Vector2 appliedForce)
        {
            Hull.AddForce(appliedForce);
        }

        public override void CalculateAngle(float dX, float dY)
        {
            Hull.CalculateAngle(dX, dY);
        }

        public override void Move()
        {
            Hull.TakeAction(typeof(EnginePart));
        }

        public override void AddForce(float force, float angle) { Hull.AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }
    }
}
