using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    abstract class Projectile : ActivatableEntity
    {
        private Timer despawnTimer;
        protected float despawnTime = 7f; //!!

        public Projectile(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            despawnTimer = new Timer(despawnTime);
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (IsActive)
            {
                despawnTimer.CountDown(gameTime);
                if (despawnTimer.IsFinished)
                    Death();
            }
        }

        protected void ResetSpawnTime()
        {
            despawnTimer.Reset();
        }
    }
}
