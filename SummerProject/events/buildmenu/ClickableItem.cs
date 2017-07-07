using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.factories;

namespace SummerProject.events.buildmenu
{
    public abstract class ClickableItem : Drawable
    {
        protected Color markedColor = Color.Beige;
        protected Color defaultColor = Color.Wheat;
        public const int SCALEFACTOR = 4;
        public IDs id;
        public Rectangle BoundBox;
        public bool Active;
        public static float Width = SpriteHandler.GetSprite((int)IDs.RECTHULLPART).SpriteRect.Width * SCALEFACTOR;
        public static float Height = SpriteHandler.GetSprite((int)IDs.RECTHULLPART).SpriteRect.Height * SCALEFACTOR;

        public ClickableItem(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
            Sprite.Scale *= SCALEFACTOR;
            Position = position;
            this.id = id;

            BoundBox = new Rectangle((int)(position.X - Sprite.Origin.X * SCALEFACTOR), (int)(position.Y - Sprite.Origin.Y * SCALEFACTOR), (int)Width, (int)Height);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
                Sprite.MColor = markedColor;
            else
                Sprite.MColor = defaultColor;
            base.Draw(spriteBatch, gameTime);
        }
    }
}
