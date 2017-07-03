using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public abstract class ActivatableEntity : Entity
    {
  

        public ActivatableEntity(Vector2 position, ISprite sprite) : base (position, sprite)
        {
        }

        public abstract void Update(GameTime gameTime);

        protected abstract void SpecificActivation(Vector2 source, Vector2 target);

        public void Activate(Vector2 source, Vector2 target)
        {
            Position = source;          
            IsActive = true;
            SpecificActivation(source, target);
        }       

        public override void Death()
        {            
            IsActive = false;
            Stop();
            Position = EntityFactory.FarAway();
        }

    }
}
