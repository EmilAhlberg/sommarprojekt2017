using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.util
{
    class AnimatedBackgroundObject
    {
        private Sprite sprite;
        private Vector2 direction;
        private float speed;
        private Timer respawnDelay;
        public Vector2 RespawnPoint { get; set; }

        public static int OOBOFFSET = 1000;

        public AnimatedBackgroundObject(Sprite sprite, Vector2 startingPos, Vector2 direction, float speed, int respawnDelay)
        {
            this.sprite = sprite;
            this.direction = direction;
            this.speed = speed;
            this.respawnDelay = new Timer(respawnDelay);
            RespawnPoint = startingPos;
            sprite.Position = startingPos;
        }

        public void Update(GameTime gameTime)
        {
            if (IsOutOfBounds())
            {
                respawnDelay.CountDown(gameTime);
                if (respawnDelay.IsFinished)
                      Respawn();
            }
            sprite.Position += direction * speed;
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            sprite.Draw(sb, gt);
        }

        private bool IsOutOfBounds()
        {
            return sprite.Position.X > WindowSize.Width + OOBOFFSET || sprite.Position.Y > WindowSize.Height + OOBOFFSET || sprite.Position.X < -OOBOFFSET || sprite.Position.Y < -OOBOFFSET;
                
        }

        private void Respawn()
        {
            sprite.Position = RespawnPoint;
        }
    }
}
