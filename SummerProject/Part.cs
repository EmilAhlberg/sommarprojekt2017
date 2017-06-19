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
        protected abstract CompositePart Hull {set; get; }

        public Part(Vector2 position, ISprite sprite, CompositePart Hull) : base(position, sprite)
        {
            this.Hull = Hull;
        }

    }
}
