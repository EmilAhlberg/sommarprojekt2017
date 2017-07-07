using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public abstract class Collidable : Movable
    {
        public Vector2 PrevPos { get; set; }
        public RotRectangle BoundBox { get; set; }
        public bool IsStatic { get; set; }
        public Collidable(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), Sprite.SpriteRect.Width, Sprite.SpriteRect.Height), angle);
            BoundBox.Origin = Sprite.Origin;
        }



        public override void ChangeSprite(ISprite sprite)
        {
            base.ChangeSprite(sprite);
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), Sprite.SpriteRect.Width, Sprite.SpriteRect.Height), angle);
            BoundBox.Origin = Sprite.Origin;
        }

        public override Vector2 Position
        {
            set
            {
                base.Position = value;
                BoundBox.Position = value;
                BoundBox.Angle = angle;
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
            get{ return Sprite.Origin; }
        }

        public override void Move()
        {
            base.Move();

                BoundBox.Position = Position;
                BoundBox.Angle = angle;
        }

        public virtual bool CollidesWith(Collidable c2)
        {
            return BoundBox.Intersects(c2.BoundBox);
        }

        public virtual void Collision(Collidable c2)
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
