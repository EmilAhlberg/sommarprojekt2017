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
        public Collidable(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), sprite.SpriteRect.Width, sprite.SpriteRect.Height), angle);
            BoundBox.Origin = sprite.Origin;
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
            ////


            //Matrix rot = Matrix.CreateRotationZ((float)Math.PI / 2);
            //Vector2 axis1 = Position - c2.Position; axis1.Normalize();
            //Vector2 axis2 = Vector2.Transform(axis1, rot);
            //Vector2 momentum1 = Velocity * Mass;
            //Vector2 momentum2 = c2.Velocity * c2.Mass;
            //Vector2 vProj11 = Vector2.Dot(Velocity, axis1) * axis1;
            //Vector2 vProj12 = Vector2.Dot(Velocity, axis2) * axis2;
            //Vector2 vProj21 = Vector2.Dot(c2.Velocity, axis1) * axis1;
            //Vector2 vProj22 = Vector2.Dot(c2.Velocity, axis2) * axis2;
            //AddForce((vProj11 - vProj21) / (Mass + c2.Mass));
        }
    }
}
