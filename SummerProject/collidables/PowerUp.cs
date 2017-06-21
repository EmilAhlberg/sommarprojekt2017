using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    abstract class PowerUp : Projectile
    {
        protected const float stdDespawnTime = 5f;

        public PowerUp(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            despawnTime = stdDespawnTime; 
        }

        public override void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Position = source;
            //if (source != target)             // if we want movable
            //{
            //    float dX = source.X - target.X;
            //    float dY = source.Y - target.Y;
            //    base.CalculateAngle(dX, dY);
            //}
         //   Stop();
            ResetSpawnTime();
        }

    }
}
