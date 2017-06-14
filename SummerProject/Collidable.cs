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
        public RotRectangle BoundBox { get; set; }

        public override Vector2 Position {
            set
            {
                base.Position = value;
                BoundBox = new RotRectangle(new Rectangle((int)Math.Round(base.Position.X), (int)Math.Round(base.Position.Y),(int) BoundBox.Width,(int) BoundBox.Height), angle);
            } //SPRITE:ORIGIN????
            get { return base.Position; }   
        }

        protected override void Move()
        {
            base.Move();
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(base.Position.X), (int)Math.Round(base.Position.Y),(int) BoundBox.Width,(int) BoundBox.Height), angle);
        }
        public bool IsStatic { get; set; }

        public Collidable(Vector2 position, ISprite sprite) : base(position,sprite)
        {
            BoundBox = new RotRectangle(new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), sprite.SpriteRect.Width, sprite.SpriteRect.Height), angle);
        }

        public abstract void Collision(Collidable c2);
    }
}
