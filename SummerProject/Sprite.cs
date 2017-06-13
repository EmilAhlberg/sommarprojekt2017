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

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            spriteRect = new Rectangle(0, 0, texture.Width, texture.Height);
            position = Vector2.Zero;
            rotation = 0;
            origin = Vector2.Zero;
            scale = 1f;
            spriteFX = SpriteEffects.None;
            layerDepth = 0;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, spriteRect, Color.White, rotation, origin, scale, spriteFX, layerDepth);
        }
    }
}
