﻿using Microsoft.Xna.Framework;
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
        protected Sprite sprite;
        protected float angle = 0;

        public Drawable(Vector2 position, Sprite sprite) // : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            this.sprite = sprite;
            sprite.origin = new Vector2(sprite.spriteRect.Width / 2, sprite.spriteRect.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
    }
}
