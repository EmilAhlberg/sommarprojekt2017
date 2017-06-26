using Microsoft.Xna.Framework;
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
        protected int Health { get; set; }
        public int Damage { get; protected set; }
        public bool IsDead { get; set; }

        public Entity2(Vector2 position, ISprite sprite)
        {
            Hull = new RectangularHull(position, sprite, this);
        }

        public virtual void Update(GameTime gameTime)
        {
            CalculateAngle();
            Hull.Move();
            if (Health <= 0 && !IsDead)
                Death();
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

        public List<Part> GetParts()
        {
            return Hull.GetParts();
        }
    }
}
