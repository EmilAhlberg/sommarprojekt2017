using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public abstract class Movable : Drawable
    {
        protected float speed { set; get; } = 1f;

        public Movable(Vector2 position, Sprite sprite) : base(position, sprite) { }

        protected void CalculateAngle(float dX, float dY)
        {
            if (dX != 0)
            {
                angle = (float)Math.Atan(dY / dX);
            }
            if (dX > 0)
                angle += (float)Math.PI;

            angle = angle % (2 * (float)Math.PI);
        }

        protected Vector2 Move(int TODO)
        {
            return new Vector2(Position.X + (float)Math.Cos(angle) * speed, Position.Y + (float)Math.Sin(angle) * speed); //TODO
        }

        protected abstract void Move();
    }
}
