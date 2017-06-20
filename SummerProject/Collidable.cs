using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerProject
{
    public abstract class Collidable : Movable
    {
        public Vector2 PrevPos { get; set; }
        public List<RotRectangle> BoundBoxes { get; set; }
        public bool IsStatic { get; set; }
        private int activeIndex;
        public int ActiveBoundBoxIndex
        {
            get
            {
                return activeIndex;
            }
            set
            {
                ActiveBoundBox = BoundBoxes[value];
                activeIndex = value;
            }
        }
        public RotRectangle ActiveBoundBox { get; private set; }
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
            ActiveBoundBoxIndex = 0;
        }
        public void AddBoundBox(RotRectangle rect)
        {
            BoundBoxes.Add(rect);
        }

        public override Vector2 Position
        {
            set
            {
                base.Position = value;
                BoundBoxes[ActiveBoundBoxIndex].Location = value;
                BoundBoxes[ActiveBoundBoxIndex].Angle = angle;
            }
            get { return base.Position; }
        }

        protected override void Move()
        {
            base.Move();
            BoundBoxes[ActiveBoundBoxIndex].Location = Position;
            BoundBoxes[ActiveBoundBoxIndex].Angle = angle;
        }
        public abstract void Collision(Collidable c2);
    }
}
