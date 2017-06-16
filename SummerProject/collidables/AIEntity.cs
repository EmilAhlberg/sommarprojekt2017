using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    public abstract class AIEntity : Entity
    {
        public bool isActive { get; set; }       

        public AIEntity(Vector2 position, ISprite sprite) : base (position, sprite)
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

        public override void Death()
        {            
            isActive = false;
            Position = factories.EntityFactory.FarAway();
        }


    }
}
