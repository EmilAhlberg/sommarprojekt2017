﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    abstract class Drop : Projectile
    {
        protected const float stdDespawnTime = 10f;
        private const int blinkTimer = 2;
        private const int blinkSpeed = 3;

        public Drop(Vector2 position) : base(position, false)
        {
            despawnTime = stdDespawnTime;
        }

        public override void Update(GameTime gameTime)
        {
            if (despawnTimer.currentTime < blinkTimer && (despawnTimer.currentTime * blinkSpeed) % 2 < 1)
                Sprite.MColor = new Color(1, 1, 1, 0.4f);
            else
                Sprite.MColor = Color.White;
            UpdateTimer(gameTime);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
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
