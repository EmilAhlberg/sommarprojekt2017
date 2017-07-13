using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.util;

namespace SummerProject
{
    class UnitBar : Drawable
    {
        private float currentValue;
        private float currentMax;
        private float startingMax;
        private Sprite scaleSprite;
        private Sprite borderSprite;
        private SpriteFont font;

        public UnitBar(Vector2 position, Sprite borderSprite, Color color, float startingMax, SpriteFont font, IDs id = IDs.DEFAULT) : base(position, id)
        {
            Position = position;
            this.startingMax = startingMax;
            scaleSprite = new Sprite();
            scaleSprite.MColor = color;
            this.borderSprite = borderSprite;
            this.borderSprite.Origin = new Vector2(0, this.borderSprite.SpriteRect.Height / 2);
            scaleSprite.Origin = new Vector2(0, this.borderSprite.SpriteRect.Height / 2);
            scaleSprite.SpriteRect = borderSprite.SpriteRect;
            this.font = font;
        }

        public void Update(float value, float max)
        {
            if (currentMax != max)
            {
                currentMax = max;
                ScaleSprite(borderSprite, max);
            }
            if (currentValue != value)
            {
                ScaleSprite(scaleSprite, value);
                currentValue = value;
            }
            if(currentValue < currentMax)
            {
                List<Vector2> listo = new List<Vector2>();
                for (int i = 0; i < Sprite.SpriteRect.Height; i++)
                {
                    listo.Add(new Vector2(scaleSprite.Scale.X * scaleSprite.SpriteRect.Width, i - Sprite.SpriteRect.Height / 2));
                }
                Particles.GenerateParticles(listo, Position, Vector2.Zero, 15, 0, scaleSprite.MColor);
            }
        }

        public void Reset()
        {
            currentMax = startingMax;
            currentValue = startingMax;
            borderSprite.Scale = new Vector2(1, 1);
            scaleSprite.Scale = new Vector2(1, 1);
        }

        private void ScaleSprite(Sprite sprite, float unit)
        {
            sprite.Scale = new Vector2(unit / startingMax, 1);      
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            scaleSprite.Position = Position;
            borderSprite.Position = Position;
            scaleSprite.Draw(spriteBatch, gameTime);
            borderSprite.Draw(spriteBatch, gameTime);
            string s = currentValue + "/" + currentMax;
            //spriteBatch.DrawOutlinedString(1, new Color(32, 32, 32), font, s, Position, Color.White, 0, Vector2.Zero, 0.6f);
        }
    }
}
