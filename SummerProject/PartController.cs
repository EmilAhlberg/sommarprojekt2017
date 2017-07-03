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
        public CompositePart Hull;
        public override Vector2 Position { get { return Hull.Position; } set { Hull.Position = value; } }
        public IEnumerable<Collidable> Collidables { get { return Parts; } }
        public List<Part> Parts { get { return Hull.Parts; } }

        public PartController(Vector2 position, ISprite sprite) : base(position,sprite)
        {
            Hull = new RectangularHull(sprite);
            Hull.Carrier = this;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Hull.Update(gameTime);
            if (Health <= 0 && !IsActive)
                Death();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Hull.Draw(spriteBatch, gameTime);
        }

        protected abstract void CalculateAngle();

        public override void Death()
        {
            IsActive = true;
            Hull.Death();
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }
    }
}
