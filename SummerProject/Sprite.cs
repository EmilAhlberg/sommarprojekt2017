using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteFX { get; set; }
        public float LayerDepth { get; set; }
        public Color MColor { get; set; }
        private List<Vector2> edges;
        public List<Vector2> Edges
        {
            get
            {
                return edges ?? CalculateEdges();
            }
        }

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
            Scale = new Vector2(1,1);
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

        /// <summary>
        /// Don't use this either. Needs to have a way to restore the texture. Colors the grays in the texture. 
        /// </summary>
        /// <param name="c"></param>
        public void Colorize(Color c)
        {
            Color[] cArray = new Color[texture.Width * texture.Height];
            texture.GetData(cArray);
            for (int i = 0; i < cArray.Length; i++)
            {
                Color currentColor = cArray[i];
                if (currentColor.R == currentColor.G && currentColor.G == currentColor.B && currentColor.A != 0)
                    cArray[i] = new Color(currentColor.R * c.R / 255, currentColor.G * c.G / 255, currentColor.B * c.B / 255, currentColor.A * c.A / 255);
            }
            texture.SetData(cArray);
        }

        public void ColorRestore()
        {

        }


        /// <summary>
        /// Slow as fuck, just so that you know. Use only once per sprite, at most.
        /// </summary>
        /// <returns></returns>
        private List<Vector2> CalculateEdges() 
        {
            Color[] colors1D = new Color[texture.Width*texture.Height];
            texture.GetData(colors1D);
            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for(int x = 0; x < texture.Width; x++)
                for(int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            
            List<Vector2> edgeList = new List<Vector2>();
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                {
                    if (colors2D[x, y].A != 0)
                    {
                        if (x == 0 || x == texture.Width - 1 || y == 0 || y == texture.Height - 1)
                        {
                             edgeList.Add(new Vector2(x - Origin.X, y - Origin.Y));
                        }
                        else
                            for (int i = -1; i <= 1; i++)
                                for (int j = -1; j <= 1; j++)
                                    if (!(i == j))
                                    {
                                        if (colors2D[x + i, y + j].A == 0)
                                        {
                                            edgeList.Add(new Vector2(x - Origin.X, y - Origin.Y));
                                            break;
                                        }
                                    }
                    }
                }

            edges = edgeList;
            return edgeList;
        }
    }
}
