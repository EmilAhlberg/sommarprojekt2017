using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables.parts;

namespace SummerProject
{
    public class ShipItem : Drawable
    {
        public const int SCALEFACTOR = 6;
        private Color markedColor = Color.Beige;
        private Color defaultColor = Color.Wheat;
        public int PartType;
        public bool Active;
        public Rectangle BoundBox;
        public float Width;
        public float Height;
        //public float itemPosition;
        //public float itemCenter;
        public int LinkPosition;
        public RectangularHull Hull;

        public ShipItem(Vector2 position, ISprite sprite, int linkPosition, int partType) : base(position, sprite)
        {
            angle = -(float)Math.PI / 2 * linkPosition;
            Width = sprite.SpriteRect.Width * SCALEFACTOR;
            Height = sprite.SpriteRect.Height * SCALEFACTOR;
            LinkPosition = linkPosition;

            sprite.Scale = new Vector2(SCALEFACTOR, SCALEFACTOR);
            Position = position;
            this.PartType = partType;
            BoundBox = new Rectangle((int)(position.X - sprite.Origin.X * SCALEFACTOR), (int)(position.Y - sprite.Origin.Y * SCALEFACTOR), (int)Width, (int)Height);
        }

        public ShipItem(Vector2 position, ISprite sprite, int linkPosition, int partType, RectangularHull hull) : this(position, sprite, linkPosition, partType)
        {
            Hull = hull;
        }

        public Part ReturnPart()
        {
            switch (PartType)
            {
                case PartTypes.GUNPART:
                    return new GunPart(new Sprite((Sprite) Sprite));
                case PartTypes.ENGINEPART:
                    return new EnginePart(new Sprite((Sprite)Sprite));
                case PartTypes.RECTANGULARHULL:
                    return new RectangularHull(new Sprite((Sprite)Sprite));
                case PartTypes.SPRAYGUNPART:
                    return new SprayGunPart(new Sprite((Sprite)Sprite));
                case PartTypes.MINEGUNPART:
                    return new MineGunPart(new Sprite((Sprite)Sprite));
                case PartTypes.CHARGINGGUNPART:
                    return new ChargingGunPart(new Sprite((Sprite)Sprite));
                case PartTypes.EMPTYPART:
                    return new GunPart(new Sprite((Sprite)Sprite)); //!!
                default: return new EnginePart(new Sprite((Sprite)Sprite));

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
