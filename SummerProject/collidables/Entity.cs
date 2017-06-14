using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    abstract class Entity : Collidable
    {
        public bool isActive { get; set; }
        public int Damage {get; set; }
        public int Health { get; set; }

        public Entity(Vector2 position, ISprite sprite) : base (position, sprite)
        {
        }

        public abstract void Update(GameTime gameTime);

        protected abstract void SpecificActivation(Vector2 source, Vector2 target);

        public void Activate(Vector2 source, Vector2 target)
        {
            Position = source;          
            isActive = true;
            SpecificActivation(source, target);
        }       

        public void Death()
        {            
            isActive = false;
            Position = new Vector2(-4000, -4000); //!
        }
      
    }
}
