using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Wall : Drawable
    {
        private Sprite sprite;

        public Wall(Vector2 position, Sprite sprite)
             : base(position, sprite)
        {
            Position = position;
            this.sprite = sprite;
            sprite.origin = new Vector2(sprite.spriteRect.Width / 2, sprite.spriteRect.Height / 2);
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
