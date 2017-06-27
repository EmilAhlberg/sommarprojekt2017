using Microsoft.Xna.Framework;
using System;

namespace SummerProject.collidables
{
    class Wall : Entity
    {

        public Wall(Vector2 position, ISprite sprite)
             : base(position, sprite)
        {
            Position = position;
            IsStatic = true;
        }

        private void MoveObject(Collidable c1, Collidable c2) // c1 should be moved 
        {
            Vector2 collidedPos = c1.Position;
            Vector2 backVect;
            backVect = c1.PrevPos - c2.Position;
            while (backVect.Length() == 0)
            {
                backVect = new Vector2(2 * SRandom.NextFloat() - 1, 2 * SRandom.NextFloat() - 1); //! LOL
            }
            backVect.Normalize();
            backVect *= 0.2f;
            float dotProd = Vector2.Dot(backVect, c1.Velocity);
            Vector2 result = dotProd / backVect.LengthSquared() * backVect;
            while (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                c1.Position += backVect;
            c1.Velocity -= result;

            //if (c1.BoundBox.Bottom == c2.BoundBox.Top || c1.BoundBox.Top == c2.BoundBox.Bottom)
            //    c1.Position = new Vector2(collidedPos.X, c1.Position.Y);
            //else
            //    c1.Position = new Vector2(c1.Position.X, collidedPos.Y);
        }

        protected override void Move()
        {
            throw new NotImplementedException();
        }

        public override void Collision(Collidable c2)
        {
            if (!(c2 is ExplosionDrop))
                MoveObject(c2, this);
        }

        public override void Death()
        {
            throw new NotImplementedException();
        }
    }
}
