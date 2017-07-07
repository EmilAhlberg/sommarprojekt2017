using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables.parts;
using SummerProject.factories;

namespace SummerProject
{
    public class ShipItem : Drawable
    {
        public const int SCALEFACTOR = 6;
        private Color markedColor = Color.Beige;
        private Color defaultColor = Color.Wheat;
        public IDs id;
        public bool Active;
        public Rectangle BoundBox;
        public static float Width = SpriteHandler.GetSprite((int)IDs.RECTHULLPART).SpriteRect.Width * SCALEFACTOR;
        public static float Height = SpriteHandler.GetSprite((int)IDs.RECTHULLPART).SpriteRect.Height * SCALEFACTOR;
        public int LinkPosition;
        public Part Part;
        public RectangularHull Hull;

        public ShipItem(Vector2 position, int linkPosition, IDs id = IDs.DEFAULT) : base(position, id)
        {
            angle = -(float)Math.PI / 2 * linkPosition;
            if (linkPosition % 2 == 0)
                angle += (float)Math.PI;
            //Width = Sprite.SpriteRect.Width * SCALEFACTOR;
            //Height = Sprite.SpriteRect.Height * SCALEFACTOR;
            LinkPosition = linkPosition;

            Sprite.Scale = new Vector2(SCALEFACTOR, SCALEFACTOR);
            Position = position;
            this.id = id;
          
            BoundBox = new Rectangle((int)(position.X - Sprite.Origin.X * SCALEFACTOR), (int)(position.Y - Sprite.Origin.Y * SCALEFACTOR), (int)Width, (int)Height);
        }

        public ShipItem(Vector2 position, int linkPosition, RectangularHull hull, Part part, IDs id = IDs.DEFAULT) : this(position, linkPosition, id)
        {
            Hull = hull;
            Part = part;
        }

        public Part ReturnPart()
        {
            switch (id)
            {
                case IDs.GUNPART:
                    return new GunPart();
                case IDs.ENGINEPART:
                    return new EnginePart();
                case IDs.RECTHULLPART:
                    return new RectangularHull();
                case IDs.SPRAYGUNPART:
                    return new SprayGunPart();
                case IDs.MINEGUNPART:
                    return new MineGunPart();
                case IDs.CHARGINGGUNPART:
                    return new ChargingGunPart();
                case IDs.EMPTYPART:
                    return new GunPart(); //!!
                default: return new EnginePart();

            }
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
