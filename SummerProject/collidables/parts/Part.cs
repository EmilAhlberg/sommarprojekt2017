using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject
{
    public abstract class Part : Collidable
    {
        public IPartCarrier Carrier { set; get; }
        public Part(Vector2 position, ISprite sprite, IPartCarrier carrier) : base(position, sprite)
        {
            Carrier = carrier;
        }

        public virtual void Collission(Collidable c2)
        {
            Carrier.Collision(c2);
        }
    }
}
