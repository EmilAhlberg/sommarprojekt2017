using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject
{
    public abstract class Movable : Drawable
    {
        public virtual float Friction { get; set; }
        private Vector2 FrictionForce { get { return Friction * Velocity / 100; } }
        public Vector2 Velocity { get; set; } = Vector2.Zero; //-!
        protected virtual float TurnSpeed { get; set; } 
        public virtual float Mass { get; set; } 
        private Vector2 Acceleration{ get { return (TotalExteriorForce - FrictionForce) / Mass; } }
        private Vector2 TotalExteriorForce { get; set; }
        public virtual float ThrusterAngle { get; set; }
        public static float MOTIONSPEED = 1f;

        public Movable(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id) { }

        public override void SetStats(IDs id)
        {
            Friction = EntityConstants.GetStatsFromID(EntityConstants.FRICTION, id);
            TurnSpeed = EntityConstants.GetStatsFromID(EntityConstants.TURNSPEED, id);
            Mass = EntityConstants.GetStatsFromID(EntityConstants.MASS, id);
            base.SetStats(id);
        }

        public virtual void Stop()
        {
            Velocity = new Vector2(0, 0);
            TotalExteriorForce = new Vector2(0, 0);
        }

        public virtual void AddSpeed(float speed, float angle)
        {
            AddForce(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed * Mass);
        }


        public virtual void AddForce(Vector2 appliedForce)
        {
            TotalExteriorForce += appliedForce;
        }

        public virtual void AddForce(float force, float angle) { AddForce(force * (new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)))); }

        public virtual void CalculateAngle(float dX, float dY)
        {
            float addedAngle = 0;
            if (dX == 0)
            {
                dX = 0.00001f;
            }
            addedAngle = (float)Math.Atan(dY / dX);
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
            Position += Velocity * MOTIONSPEED;
            TotalExteriorForce = new Vector2(0, 0);
            ThrusterAngle = angle;
        }
    }
}
