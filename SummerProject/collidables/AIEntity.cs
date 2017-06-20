using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public abstract class AIEntity : Entity
    {
        public bool IsActive { get; set; }       

        public AIEntity(Vector2 position, ISprite sprite) : base (position, sprite)
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
