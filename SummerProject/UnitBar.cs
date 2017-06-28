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
        private float prevMax;
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
            if (prevMax != max)
            {
                prevMax = max;
                ScaleSprite(borderSprite, max);
            }
            if (currentValue != value)
            {
                ScaleSprite(scaleSprite, value);
                currentValue = value;
            }
        }

        public void Reset()
        {
            prevMax = startingMax;
            currentValue = startingMax;
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
