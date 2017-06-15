using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class Collidable : Movable
    {
        public Vector2 PrevPos { get; set; }
        public List<Rectangle> BoundBoxes { get; set; }
        public bool IsStatic { get; set; }

        public Collidable(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            BoundBoxes = new List<Rectangle>();
            if (sprite is CompositeSprite)
            {
                List<ISprite> spriteList = ((CompositeSprite)sprite).spriteList;
                foreach (ISprite s in spriteList)
                {
                    BoundBoxes.Add(new Rectangle((int)Math.Round(s.Position.X), (int)Math.Round(s.Position.Y), s.SpriteRect.Width, s.SpriteRect.Height));
                }
            }
            else
               BoundBoxes.Add(new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), sprite.SpriteRect.Width, sprite.SpriteRect.Height));
        }
        public void AddBoundBox(Rectangle rect)
        {
            BoundBoxes.Add(rect);
        }
        public override Vector2 Position {
            set
            {
                base.Position = value;
                for (int i = 0; i < BoundBoxes.Count(); i++)
                {
                    BoundBoxes[i] = new Rectangle((int)Math.Round(base.Position.X), (int)Math.Round(base.Position.Y), BoundBoxes[i].Width, BoundBoxes[i].Height);
                }
            }
            get { return base.Position; }   
        }

        protected override void Move()
        {
            base.Move();
            for (int i = 0; i < BoundBoxes.Count(); i++) {
                BoundBoxes[i] = new Rectangle((int)Math.Round(base.Position.X), (int)Math.Round(base.Position.Y), BoundBoxes[i].Width, BoundBoxes[i].Height);
            }
        }
        public abstract void Collision(Collidable c2);
    }
}
