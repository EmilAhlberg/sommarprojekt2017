using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SummerProject
{
    public class Sprite
    {
        Texture2D texture;
        public Rectangle spriteRect { get; private set; }
        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public Vector2 origin { get; set; }
        public float scale { get; set; }
        public SpriteEffects spriteFX { get; set; }
        public float layerDepth { get; set; }
        private int subimages;
        private float currentFrame;
        private int fps;

        public Sprite(Sprite sprite) : this(sprite.texture, sprite.subimages, sprite.fps)
        {
        }

        public Sprite(Texture2D texture, int subimages = 1, int fps = 4)
        {
            this.texture = texture;
            this.subimages = subimages;
            this.fps = fps;
            spriteRect = new Rectangle(0, 0, texture.Width/subimages, texture.Height);
            position = Vector2.Zero;
            rotation = 0;
            origin = Vector2.Zero;
            scale = 1f;
            spriteFX = SpriteEffects.None;
            layerDepth = 0;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            Animate(gameTime);
            sb.Draw(texture, position, spriteRect, Color.White, rotation, origin, scale, spriteFX, layerDepth);
        }

        private void Animate(GameTime gameTime)
        {      
            currentFrame += (float)gameTime.ElapsedGameTime.TotalSeconds * fps;
            if((int)currentFrame > subimages-1)
            {
                currentFrame = 0;
            }
            spriteRect = new Rectangle((int)currentFrame * spriteRect.Width, 0, spriteRect.Width, spriteRect.Height);
        }
    }
}
