using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    abstract class Projectile : AIEntity
    {
        private float despawnTimer = 7f;
        private const float despawnTime = 7f;

        public Projectile(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (IsActive)
            {
                despawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (despawnTimer < 0)
                    Death();
            }
        }

        protected void ResetSpawnTime()
        {
            despawnTimer = despawnTime;
        }
    }
}
