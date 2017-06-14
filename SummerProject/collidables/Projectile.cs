using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    abstract class Projectile : Entity
    {
        private float despawnTimer = 7f;
        private const float despawnTime = 7f; //!!

        public Projectile(ISprite sprite) : base (Vector2.Zero, sprite)
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
