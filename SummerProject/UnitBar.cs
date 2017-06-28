using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;

namespace SummerProject
{
    class UnitBar : Drawable
    {
        private float currentValue;
        private float currentMax;
        private float startingMax;
        private Sprite scaleSprite;
        private Sprite borderSprite;
        public UnitBar(Vector2 position, Sprite borderSprite, Color color, float startingMax) : base(position, borderSprite)
        {
            Position = position;
            this.startingMax = startingMax;
            scaleSprite = new Sprite();
            scaleSprite.MColor = color;
            this.borderSprite = borderSprite;
            this.borderSprite.Origin = new Vector2(0, this.borderSprite.SpriteRect.Height / 2);
            scaleSprite.Origin = new Vector2(0, this.borderSprite.SpriteRect.Height / 2);
            scaleSprite.SpriteRect = borderSprite.SpriteRect;
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
                for (int i = 0; i < sprite.SpriteRect.Height; i++)
                {
                    listo.Add(new Vector2(scaleSprite.Scale.X * scaleSprite.SpriteRect.Width, i - sprite.SpriteRect.Height / 2));
                }
                Particles.GenerateParticles(listo, Position, Vector2.Zero, 15, scaleSprite.MColor);
            }
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
        }
    }
}
