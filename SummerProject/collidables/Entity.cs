using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    public abstract class Entity : Collidable
    {
        public int Damage { get; set; }
        public int Health { get; set; }


        public Entity(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        public abstract void Death();
        
    }
}
