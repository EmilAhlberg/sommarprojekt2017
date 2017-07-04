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
                sprite.Origin = value;
            }
            get{ return sprite.Origin; }
        }

        public override void Move()
        {
            base.Move();

                BoundBox.Position = Position;
                BoundBox.Angle = angle;
        }
        public abstract void Collision(Collidable c2);
    }
}
