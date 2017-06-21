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
        public CompositePart Hull { set; get; }
        public Part(Vector2 position, ISprite sprite, CompositePart hull) : base(position, sprite)
        {
            Hull = hull;
        }

        public virtual void Collission(Collidable c2)
        {
            Hull.Collision(c2);
        }
    }
}
