using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public abstract class Collidable : Movable
    {
        public Vector2 PrevPos { get; set; }
        public List<RotRectangle> BoundBoxes { get; private set; }
        public bool IsStatic { get; set; }
        public Collidable(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            BoundBoxes = new List<RotRectangle>();
            //if (sprite is CompositeSprite)
            //{
            //    List<ISprite> spriteList = ((CompositeSprite)sprite).spriteList;
            //    foreach (ISprite s in spriteList)
            //    {
            //        BoundBoxes.Add(new RotRectangle(new Rectangle((int)Math.Round(s.Position.X - s.Origin.X), (int)Math.Round(s.Position.Y - s.Origin.Y), s.SpriteRect.Width, s.SpriteRect.Height), angle));
            //    }
            //}
            //else
            BoundBoxes.Add(new RotRectangle(new Rectangle((int)Math.Round(position.X - sprite.Origin.X), (int)Math.Round(position.Y - sprite.Origin.Y), sprite.SpriteRect.Width, sprite.SpriteRect.Height), angle));
        }
        public void AddBoundBox(RotRectangle rect)
        {
            rect.Location = Position;
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
                    BoundBoxes[i].Location = value;
                    BoundBoxes[i].Angle = angle;
                }
            }
            get { return base.Position; }
        }

        protected override void Move()
        {
            base.Move();
            for (int i = 0; i < BoundBoxes.Count; i++)
            {
                BoundBoxes[i].Location = Position;
                BoundBoxes[i].Angle = angle;
            }
        }
        public abstract void Collision(Collidable c2);
    }
}
