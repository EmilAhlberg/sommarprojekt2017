using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerProject
{
    public class Sprite
    {
        static Texture2DPlus baseTexture;
        Texture2DPlus texture;
        public Rectangle SpriteRect { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteFX { get; set; }
        public float LayerDepth { get; set; }
        public Color MColor { get; set; }
        public List<Vector2> Edges
        {
            get
            {
                return texture.Edges;
            }
        }
        public List<Sprite> SplitSprites
        {
            get
            {
                return texture.SplitTextures.ConvertAll(t => new Sprite(new Texture2DPlus(t)));
            }
        }


        public Color PrimaryColor
        {
            get
            {
                if (MColor == Color.White)
                    return texture.PrimaryColor;
                return MColor;
            }
        }


        public List<Color> Colors
        {
            get
            {
                    return texture.Colors;
            }
        }



        private int subimages;
        private float currentFrame;
        private int fps;

        public Sprite(Sprite sprite) : this(sprite.texture, sprite.fps)
        {
        }

        public Sprite(Texture2DPlus texture, int fps = 4)
        {
            this.texture = texture;
            this.subimages = texture.Subimages;
            this.fps = fps;
            SpriteRect = new Rectangle(0, 0, texture.Texture.Width / subimages, texture.Texture.Height);
            Rotation = 0;
            Scale = new Vector2(1,1);
            MColor = Color.White;
            SpriteFX = SpriteEffects.None;
            LayerDepth = 0;
        }

        //public Sprite GetRotatedSprite(float rads)
        //{
        //    return new Sprite(texture.GetRotatedTexture(rads));
        //}

        public Sprite() : this(baseTexture, 1)
        {
            Origin = new Vector2(SpriteRect.Width / 2, SpriteRect.Height / 2); //! HMM
        }

        public static void addBaseTexture(Texture2DPlus t)
        {
            baseTexture = t;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            Animate(gameTime);
            sb.Draw(texture.Texture, Position, SpriteRect, MColor, Rotation, Origin, Scale, SpriteFX, LayerDepth);
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
 
        /// <summary>
        /// Don't use this either. Needs to have a way to restore the texture. Colors the grays in the texture. 
        /// </summary>
        /// <param name="c"></param>

        //public void Colorize(Color c)
        //{
        //    Color[] cArray = new Color[texture.Width * texture.Height];
        //    texture.GetData(cArray);
        //    for (int i = 0; i < cArray.Length; i++)
        //    {
        //        Color currentColor = cArray[i];
        //        if (currentColor.R == currentColor.G && currentColor.G == currentColor.B && currentColor.A != 0)
        //            cArray[i] = new Color(currentColor.R * c.R / 255, currentColor.G * c.G / 255, currentColor.B * c.B / 255, currentColor.A * c.A / 255);
        //    }
        //    texture.SetData(cArray);
        //}

        //public void ColorRestore()
        //{
        //}

    }
}
