using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject
{
    public abstract class Movable : Drawable
    {
        public virtual float friction { set; get; } = EntityConstants.FRICTION[(int)IDs.DEFAULT]; //!
        private Vector2 Friction { get { return friction * Velocity / 100; } }
        public Vector2 Velocity { set; get; } = Vector2.Zero; //-!
        protected virtual float TurnSpeed { set; get; } = EntityConstants.TURNSPEED[(int)IDs.DEFAULT];
        protected virtual float Mass { set; get; } = EntityConstants.MASS[(int)IDs.DEFAULT]; 
        private Vector2 Acceleration{ get { return (TotalExteriorForce - Friction) / Mass; } }
        private Vector2 TotalExteriorForce { set; get; }
        public virtual float ThrusterAngle { set; get; }

        public virtual void Stop()
        {
            Velocity = new Vector2(0, 0);   
            TotalExteriorForce = new Vector2(0, 0);
        }

        public virtual void AddSpeed(float speed, float angle)
        {
            AddForce(new Vector2((float)Math.Cos(angle),(float)Math.Sin(angle)) * speed*Mass);
        }

        public Movable(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id) { }

        public virtual void AddForce(Vector2 appliedForce)
        {
            TotalExteriorForce += appliedForce;
        }

        public virtual void AddForce(float force, float angle) { AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }

        public virtual void CalculateAngle(float dX, float dY)
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
            else if (addedAngle < -TurnSpeed)
            {
                addedAngle = -TurnSpeed;
            }
            angle += addedAngle;
            if (angle < 0)
                angle += (2 * (float)Math.PI);
            angle = angle % (2 * (float)Math.PI);
        }

        public virtual void Move()
        {
            Velocity += Acceleration;
            Position += Velocity;
            TotalExteriorForce = new Vector2(0, 0);
            ThrusterAngle = angle;
        }
    }
}
