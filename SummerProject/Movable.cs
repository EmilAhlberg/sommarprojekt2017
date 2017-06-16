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
        private const float FRICTION = 10f; //!
        private Vector2 Friction { get { return FRICTION * Velocity*Mass*Mass/1000; } }
        private Vector2 Velocity { set; get; } = new Vector2(0,0); //-!
        protected float TurnSpeed { set; get; } = 1000f * (float)Math.PI; //! //rad per tick
        protected float Mass { set; get; } = 10; //-!
        protected float Thrust { set; get; } = 10; //-!
        private Vector2 Acceleration{get{ return (ThrusterForce + TotalExteriorForce - Friction) /Mass; } }
        private Vector2 ThrusterForce { get { return DirectionVector * Thrust;}}
        private Vector2 TotalExteriorForce { set; get; }
        protected Vector2 DirectionVector { set { angle = (float)Math.Atan(value.Y / value.X); } get { return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); } }

        protected void Stop()
        {
            Velocity = new Vector2(0, 0);
            TotalExteriorForce = new Vector2(0, 0);
        }

        protected void AddSpeed(float speed)
        {
            AddForce(DirectionVector * speed*Mass);
        }
        public Movable(Vector2 position, ISprite sprite) : base(position, sprite) { }

        protected void AddForce(Vector2 appliedForce)
        {
            TotalExteriorForce = TotalExteriorForce + appliedForce;

            //Velocity -= Acceleration * Friction;
        }

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
            Velocity += Acceleration;
            Position += Velocity;
            TotalExteriorForce = new Vector2(0, 0);
        }
    }
}
