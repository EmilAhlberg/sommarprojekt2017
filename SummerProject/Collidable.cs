using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public abstract class Collidable : Movable
    {
        public Vector2 PrevPos { get; set; }
        public List<RotRectangle> BoundBoxes { get; set; }
        public bool IsStatic { get; set; }
        public Collidable(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            BoundBoxes = new List<RotRectangle>();
            BoundBoxes.Add(new RotRectangle(new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), sprite.SpriteRect.Width, sprite.SpriteRect.Height), angle));
            BoundBoxes[0].Origin = sprite.Origin;
        }
        public void AddBoundBox(RotRectangle rect)
        {
            rect.Position = Position;
            BoundBoxes.Add(rect);
        }
        public void RemoveBoundBox(int index)
        {
            BoundBoxes.RemoveAt(index);
        }

        public override Vector2 Position
        {
            set
            {
                base.Position = value;
                for (int i = 0; i < BoundBoxes.Count; i++)
                {
                    BoundBoxes[i].Position = value;
                    BoundBoxes[i].Angle = angle;
                }
            }
            get { return base.Position; }
        }

        public float Angle
        {
            set
            {
                base.angle = value;
                BoundBoxes[0].Angle = value;
            }
            get { return angle; }
        }

        public virtual Vector2 Origin
        {
            set
            {
                BoundBoxes[0].Origin = value; //FIX
                sprite.Origin = value;
            }
            get{ return sprite.Origin; }
        }

        protected override void Move()
        {
            base.Move();
            for (int i = 0; i < BoundBoxes.Count; i++)
            {
                BoundBoxes[i].Position = Position;
                BoundBoxes[i].Angle = angle;
            }
        }
        public abstract void Collision(Collidable c2);
    }
}
