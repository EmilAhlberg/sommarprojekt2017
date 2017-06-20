using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public abstract class Part : Collidable
    {
        public Part(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        public CompositePart Hull {set; get;}
    }
}
