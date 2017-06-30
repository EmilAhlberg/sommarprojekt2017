﻿using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject
{
    public abstract class Movable : Drawable
    {
        protected float friction = EntityConstants.FRICTION[EntityConstants.DEFAULT]; //!
        private Vector2 Friction { get { return friction * Velocity * Mass * Mass / 1000; } }
        public Vector2 Velocity { set; get; } = Vector2.Zero; //-!
        protected float TurnSpeed { set; get; } = EntityConstants.TURNSPEED[EntityConstants.DEFAULT];
        protected float Mass { set; get; } = EntityConstants.MASS[EntityConstants.DEFAULT]; 
        protected float Thrust { set; get; } = EntityConstants.THRUST[EntityConstants.DEFAULT];
        private Vector2 Acceleration{ get { return (ThrusterForce + TotalExteriorForce - Friction) / Mass; } }
        private Vector2 ThrusterForce { get { return DirectionVector * Thrust;}}
        private Vector2 TotalExteriorForce { set; get; }
        protected Vector2 DirectionVector { set { angle = (float)Math.Atan(value.Y / value.X); } get { return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); } }

        public void Stop()
        {
            Velocity = new Vector2(0, 0);   
            TotalExteriorForce = new Vector2(0, 0);
        }

        protected void AddSpeed(float speed, float angle)
        {
            AddForce(new Vector2((float)Math.Cos(angle),(float)Math.Sin(angle)) * speed*Mass);
        }

        public Movable(Vector2 position, ISprite sprite) : base(position, sprite) { }

        protected void AddForce(Vector2 appliedForce)
        {
            TotalExteriorForce = TotalExteriorForce + appliedForce;
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
            else if (addedAngle < -TurnSpeed)
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
