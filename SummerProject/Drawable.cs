﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    public class Drawable
    {
        public virtual Vector2 Position { set; get; }
        protected ISprite sprite;
        public float angle = 0;
        private float prevAngle;

        public Drawable(Vector2 position, ISprite sprite) // : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            this.sprite = sprite;
            prevAngle = angle;
            if (sprite is Sprite)
                sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Position = Position;
            //if (prevAngle != angle) // for changing sprite rotation independently (asteroid)
                  sprite.Rotation = angle;
            sprite.Draw(spriteBatch, gameTime);
            prevAngle = angle;
            //Color c = Color.White;
            //Microsoft.Xna.Framework.Graphics.DrawLine(new Pen(c),new Point((int)Position.X,(int)Position.Y), new Point((int)Position.X+(int)sprite.Origin.X, (int)Position.Y+(int)sprite.Origin.Y));
        }
    }
}
