using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    abstract class Projectile : AIEntity
    {
        private float despawnTimer = 7f;
        private const float despawnTime = 7f;

        public Projectile(Vector2 position, ISprite sprite) : base (position, sprite)
        {
            Speed = 10;
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (isActive)
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
