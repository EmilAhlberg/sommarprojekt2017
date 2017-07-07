using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    public class ShipItem : Drawable
    {
        public const int SCALEFACTOR = 6;
        private Color markedColor = Color.Beige;
        private Color defaultColor = Color.Wheat;
        public bool Active;
        public Rectangle BoundBox;
        public float Width;
        public float Height;
        public float itemPosition;
        public float itemCenter;

        public ShipItem(Vector2 position, int linkPosition, IDs id = IDs.DEFAULT) : base(position, id)
        {
            angle = (float) Math.PI/2 * linkPosition;
            Width = Sprite.SpriteRect.Width * SCALEFACTOR;
            Height = Sprite.SpriteRect.Height * SCALEFACTOR;
                      
            Sprite.Scale = new Vector2(SCALEFACTOR, SCALEFACTOR);            
            Position = position;

            BoundBox = new Rectangle((int)(position.X -Sprite.Origin.X*SCALEFACTOR), (int)(position.Y - Sprite.Origin.Y * SCALEFACTOR) , (int)Width, (int)Height);
        }

        public override void  Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
                Sprite.MColor = markedColor;
            else
                Sprite.MColor = defaultColor;
            base.Draw(spriteBatch, gameTime);
        }
    }
}
