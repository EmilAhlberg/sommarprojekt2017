using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public abstract class ActivatableEntity : Entity, IActivatable
    {
        public bool IsBusy { get; set; }

        public ActivatableEntity(Vector2 position, IDs id = IDs.DEFAULT) : base (position, id)
        {
        }

        protected abstract void SpecificActivation(Vector2 source, Vector2 target);

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
                base.Update(gameTime);
        }

        public override void Collision(ICollidable c2)
        {
            if (IsActive)
                HandleCollision(c2);
        }

        protected abstract void HandleCollision(ICollidable c2);

        public virtual void Activate(Vector2 source, Vector2 target)
        {
            SetStats(id);
            Position = source;
            IsActive = true;
            SpecificActivation(source, target);
        }       

        public override void Death()
        {
            IsActive = false;
            IsBusy = false;
            Stop();
            Position = EntityFactory.FarAway();
        }

    }
}
