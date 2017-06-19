using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    public class Sprite : ISprite
    {
        static Texture2D baseTexture;
        Texture2D texture;
        public Rectangle SpriteRect { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteFX { get; set; }
        public float LayerDepth { get; set; }
        public Color MColor { get; set; }
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
            SpriteRect = new Rectangle(0, 0, texture.Width / subimages, texture.Height);
            Rotation = 0;
            Scale = 1f;
            MColor = Color.White;
            SpriteFX = SpriteEffects.None;
            LayerDepth = 0;
        }

        public Sprite() : this(baseTexture, 1, 1)
        {
            Origin = new Vector2(SpriteRect.Width / 2, SpriteRect.Height / 2); //! HMM
        }

        public static void addBaseTexture(Texture2D t)
        {
            baseTexture = t;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            Animate(gameTime);
            sb.Draw(texture, Position, SpriteRect, MColor, Rotation, Origin, Scale, SpriteFX, LayerDepth);
        }

        public void Animate(GameTime gameTime)
        {
            currentFrame += (float)gameTime.ElapsedGameTime.TotalSeconds * fps;
            if ((int)currentFrame > subimages - 1)
            {
                currentFrame = 0;
            }
            SpriteRect = new Rectangle((int)currentFrame * SpriteRect.Width, 0, SpriteRect.Width, SpriteRect.Height);
        }
    }
}
