using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class Drawable
    {
        public virtual Vector2 Position { set; get; }
        protected ISprite sprite;
        protected float angle = 0;

        public Drawable(Vector2 position, ISprite sprite) // : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            this.sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Position = Position;
            sprite.Rotation = angle;
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
