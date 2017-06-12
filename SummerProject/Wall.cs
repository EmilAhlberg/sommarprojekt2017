using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Wall : Collidable
    {
        private float angle = 0;
        private Sprite sprite;

        public Wall(Vector2 position, Sprite sprite)
        {
            Position = position;
            this.sprite = sprite;
            BoundBox = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), sprite.spriteRect.Width, sprite.spriteRect.Height);
            IsStatic = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
    }
}
