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
        private float currentUnit;
        private float currentMax;
        private float startingUnit;
        private Sprite scaleSprite;
        private Sprite bkgSprite;
        Vector2 position;
        public UnitBar(Vector2 position, Sprite bkgSprite, Color color, float startingUnit) : base(position, bkgSprite)
        {
            this.Position = position;
            this.startingUnit = startingUnit;
            scaleSprite = new Sprite();
            scaleSprite.MColor = color;
            this.bkgSprite = bkgSprite;
            this.bkgSprite.Origin = new Vector2(0, this.bkgSprite.SpriteRect.Height / 2);
            scaleSprite.Origin = new Vector2(0, this.bkgSprite.SpriteRect.Height / 2);
            scaleSprite.SpriteRect = bkgSprite.SpriteRect;
        }

        public void Update(float currentUnit, float currentMax)
        {
            if (this.currentMax != currentMax)
                ScaleSprite(bkgSprite, currentMax);
            if (this.currentUnit != currentUnit)
                ScaleSprite(scaleSprite, currentUnit);
        }

        private void ScaleSprite(Sprite sprite, float unit)
        {
            sprite.Scale = new Vector2(unit / startingUnit, 1);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            scaleSprite.Position = Position;
            bkgSprite.Position = Position;
            scaleSprite.Draw(spriteBatch, gameTime);
            bkgSprite.Draw(spriteBatch, gameTime);
        }
    }
}
