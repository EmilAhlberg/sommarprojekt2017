using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Wall : Drawable
    {

        public Wall(Vector2 position, Sprite sprite)
             : base(position, sprite)
        {
            Position = position;
            IsStatic = true;
        }

    }
}
