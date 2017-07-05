using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
   public abstract class Projectile : ActivatableEntity
    {
        protected Timer despawnTimer;
        protected float despawnTime = 7f; //!!
        public bool IsEvil;

        public Projectile(Vector2 position, ISprite sprite, bool isEvil) : base(position, sprite)
        {
            this.IsEvil = isEvil;
            despawnTimer = new Timer(despawnTime);
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (IsActive)
            {
                despawnTimer.CountDown(gameTime);
                if (despawnTimer.IsFinished || WindowSize.IsOutOfBounds(Position))
                    Death();
            }
        }

        protected void ResetSpawnTime()
        {
            despawnTimer.Reset();
        }
    }
}
