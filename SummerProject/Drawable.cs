using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Drawable : Collidable
    {
        protected float angle = 0;
        private Sprite sprite;
        public Drawable(Vector2 position, Sprite sprite) : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            this.sprite = sprite;
        }
        protected void CalculateAngle(float dX, float dY)
        {
            if (dX != 0)
            {
                angle = (float)Math.Atan(dY / dX);
            }
            if (dX > 0)
                angle += (float)Math.PI;

            angle = angle % (2 * (float)Math.PI);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
    }
}
