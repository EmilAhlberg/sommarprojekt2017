using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class Collidable
    {
        public Vector2 PrevPos { get; set; }
        public Rectangle BoundBox { get; set; }
        private Vector2 position; 
        public Vector2 Position {
            get { return position; }
            set {
                position = value;
                BoundBox = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), BoundBox.Width, BoundBox.Height);
            }
        }
        public bool IsStatic { get; set; }

        public Collidable(int width, int height)
        {
            BoundBox = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), width, height);
        }
    }
}
