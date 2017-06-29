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
    public abstract class Entity2 : IPartCarrier
    {
        protected CompositePart Hull;
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public bool IsDead { get; set; }
        public Vector2 Position { get { return Hull.Position; } set { Hull.Position = value; } }
        public IEnumerable<Collidable> Collidables { get { return Parts; } }
        public List<Part> Parts { get { return Hull.Parts; } }

        public Entity2(ISprite sprite)
        {
            Hull = new RectangularHull(sprite);
            Hull.Carrier = this;
        }

        public virtual void Update(GameTime gameTime)
        {
            CalculateAngle();
            Hull.Move();
            if (Health <= 0 && !IsDead)
                Death();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Hull.Draw(spriteBatch, gameTime);
        }
        protected abstract void CalculateAngle();

        public virtual void Death()
        {
            IsDead = true;
        }

        public void Collision(Collidable c2)
        {
            throw new NotImplementedException();
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }
    }
}
