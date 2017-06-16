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
        protected float TurnSpeed { set; get; } = 1000f * (float)Math.PI; //! //rad per tick
        protected Vector2 Velocity { set; get; } = new Vector2(0,0); //-!
        protected float mass { set; get; } = 1;
        protected float thrust { set; get; } = 1;



        public Movable(Vector2 position, ISprite sprite) : base(position, sprite) { }

        protected void CalculateAngle(float dX, float dY)
        {
            float addedAngle = 0;
            if (dX != 0)
            {
                addedAngle = (float)Math.Atan(dY / dX);
            }
            if (dX > 0)
                addedAngle += (float)Math.PI;

            if (Math.Abs(addedAngle - angle) < Math.Abs(addedAngle - angle + (2 * (float)Math.PI)))
                addedAngle = addedAngle - angle;
            else
                addedAngle = addedAngle - angle + (2 * (float)Math.PI);

            if (addedAngle > TurnSpeed)
            {
                addedAngle = TurnSpeed;
            }
            else if(addedAngle < -TurnSpeed)
            {
                addedAngle = -TurnSpeed;
            }
            angle += addedAngle;
            if (angle < 0)
                angle += (2 * (float)Math.PI);
            angle = angle % (2 * (float)Math.PI);
        }

        protected virtual void Move()
        {
            if (this is collidables.Bullet)
            {
                Position += Velocity;
            } else
            {
                Position += Velocity;
            }
           
        }
    }
}
