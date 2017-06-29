using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class CompositeSprite
    {
        public List<ISprite> spriteList { get; }

        public Vector2 Scale { get; set; } //!
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                foreach (ISprite s in spriteList)
                {
                    s.Rotation = value; //All sprites have the same rotation as the CompositeSprite.
                }
                rotation = value;
            }
        }
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                foreach (ISprite s in spriteList)
                {
                    s.Position = value; //All sprites have positions relative to the CompositeSprite.
                }
                position = value;
            }
        }

        public Rectangle SpriteRect { get; set; }
        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
            set
            {
                foreach (ISprite s in spriteList)
                {
                    s.Origin = s.Origin + value; //All sprites have origins relative to the CompositeSprite.
                }
                origin = value;
            }
        }
        private Color color;
        public Color MColor
        {
            get { return color; }
            set
            {
                foreach (ISprite s in spriteList)
                {
                    s.MColor = value; //All sprites have the same color as CompositeSprite.
                }
                color = value;
            }
        }

        public List<Vector2> Edges
        {
            get
            {
                return CalculateEdges();
            }
        }

        public CompositeSprite()
        {
            spriteList = new List<ISprite>();
        }

        public void addSprite(Sprite s, Vector2 relativePos)
        {
            if (spriteList.Count == 0)
            {
                Origin = new Vector2(s.SpriteRect.Width / 2, s.SpriteRect.Height / 2);
                SpriteRect = s.SpriteRect;
            }
            s.Origin = new Vector2(s.SpriteRect.Width / 2, s.SpriteRect.Height / 2); //! hmmm
            s.Origin += -relativePos;
            spriteList.Add(s);
        }

        public void Animate(GameTime gameTime)
        {
            foreach (ISprite s in spriteList)
            {
                s.Animate(gameTime);
            }
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (ISprite s in spriteList)
            {
                s.Draw(sb, gameTime);
            }
        }

        private List<Vector2> CalculateEdges()
        {
            return spriteList[0].Edges; //!
        }

        public void Colorize(Color c)
        {
            foreach(ISprite s in spriteList)
            {
                s.Colorize(c);
            }
        }
    }
}
