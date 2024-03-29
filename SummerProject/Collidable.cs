﻿using Microsoft.Xna.Framework;
using SummerProject.util;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public abstract class Collidable : Movable, ICollidable
    {
        public Vector2 PrevPos { get; set; }
        public RotRectangle BoundBox { get; set; }
        public Circle BoundCircle { get; set; }
        public bool IsStatic { get; set; }
        public Collidable(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            InitBoundBoxes(position);
        }

        private void InitBoundBoxes(Vector2 position)
        {
            Rectangle rect = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), Sprite.SpriteRect.Width, Sprite.SpriteRect.Height);
            BoundBox = new RotRectangle(rect, angle);
            BoundCircle = new Circle(rect.Width / 2 + rect.Height / 2); // approx size, can optimize with a get corner from rotrect
            BoundBox.Origin = Sprite.Origin;
        }

        public override void ChangeSprite(Sprite sprite)
        {
            base.ChangeSprite(sprite);
            InitBoundBoxes(Position);
        }

        public override Vector2 Position
        {
            set
            {
                base.Position = value;
                BoundBox.Position = value;
                BoundCircle.Position = value;
            }
            get { return base.Position; }
        }

        public virtual float Angle
        {
            set
            {
                base.angle = value;
                BoundBox.Angle = value;
            }
            get { return angle; }
        }

        public virtual Vector2 Origin
        {
            set
            {
                BoundBox.Origin = value; //FIX
                Sprite.Origin = value;
            }
            get { return Sprite.Origin; }
        }

        public override void Move()
        {
            base.Move();

            BoundBox.Position = Position;
            BoundCircle.Position = Position;
            Angle = angle;
        }

        public virtual bool CollidesWith(ICollidable c2)
        {
            if (c2 is PartController)
                c2 = (c2 as PartController).Hull;
            if (c2 is Collidable)
            {
                if (BoundCircle.Intersects((c2 as Collidable).BoundCircle))
                    return BoundBox.Intersects((c2 as Collidable).BoundBox);
            }
            return false;
        }

        public virtual void Collision(ICollidable c2)
        {
            ////VILKEN SIDA BESKÄRS AV AVSTÅNDET MELLAN POSITIONERNA?
            //Vector2 axis1 = c2.BoundBox.AbsolutePosition - BoundBox.AbsolutePosition;
            //Vector2 middle = axis1 / 2 + BoundBox.AbsolutePosition;
            //axis1.Normalize();
            //Vector2 axis2 = Vector2.Transform(axis1, Matrix.CreateRotationZ((float)Math.PI / 2));











            //Vector2 momentum1 = Velocity * Mass;
            //Vector2 momentum2 = c2.Velocity * c2.Mass;
            //Vector2 vProj11 = Vector2.Dot(Velocity, axis1) * axis1;
            //Vector2 vProj12 = Vector2.Dot(Velocity, axis2) * axis2;
            //Vector2 vProj21 = Vector2.Dot(c2.Velocity, axis1) * axis1;
            //Vector2 vProj22 = Vector2.Dot(c2.Velocity, axis2) * axis2;
            //AddForce((vProj11 - vProj21) / (Mass + c2.Mass));

            //dV1 = v1 - 2 * m2 / (m1 + m2) * Vector2.Dot(v1 - v2, x1 - x2) / Vector2.DistanceSquared(x1 - x2) * (x1 - x2);
            //float mass1 = 2 * c2.Mass / (Mass + c2.Mass);
            //float mass2 = 2 * Mass / (Mass + c2.Mass);
            //float dot1 = Vector2.Dot(Velocity - c2.Velocity, BoundBox.AbsolutePosition - c2.BoundBox.AbsolutePosition);
            //float dot2 = Vector2.Dot(c2.Velocity - Velocity, c2.BoundBox.AbsolutePosition - BoundBox.AbsolutePosition);
            //Vector2 newV1 = Velocity - mass1 * dot1 / Vector2.DistanceSquared(BoundBox.AbsolutePosition, c2.BoundBox.AbsolutePosition) * (BoundBox.AbsolutePosition - c2.BoundBox.AbsolutePosition);
            //Vector2 newV2 = c2.Velocity - mass2 * dot2 / Vector2.DistanceSquared(c2.BoundBox.AbsolutePosition, BoundBox.AbsolutePosition) * (c2.BoundBox.AbsolutePosition - BoundBox.AbsolutePosition);
            //Velocity = newV1;
            //c2.Velocity = newV2;
        }
    }
}
