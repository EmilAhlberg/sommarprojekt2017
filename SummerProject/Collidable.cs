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
        public Rectangle BoundBox { get; set; }

        public Vector2 Position {
            get { return position; }
            set {
                position = value;
                BoundBox = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), BoundBox.Width, BoundBox.Height);
            }
        }
        public bool IsStatic { get; set; }

        public Collidable(Vector2 position, Sprite sprite) : base(position,sprite)
        {
            BoundBox = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), sprite.spriteRect.Width, sprite.spriteRect.Height);
        }

        public abstract void collision(Collidable c2);
    }
}
