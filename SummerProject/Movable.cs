using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public abstract class Movable : Drawable
    {
        protected float speed { set; get; } = 1f;

        public Movable(Vector2 position, Sprite sprite) : base(position, sprite) { }

        protected void Move(int TODO)
        {

        }

        protected abstract void Move();
    }
}
