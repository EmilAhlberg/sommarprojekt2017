using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.util;
using SummerProject.collidables;

namespace SummerProject.events.buildmenu
{
    public class UpgradeBarItem : ClickableItem
    {
        private Sprite box;
        private SpriteFont font;
        public int Price { get; set; }
        public string Name { get; private set; }
        private Vector2 belowItemPosition;
        private Vector2 belowNamePosition;
        private const int belowItemSpacing = 18;
        private const int halfSpriteSize = 16;
        private const int namePriceSpacing = 25;

        public UpgradeBarItem(Vector2 position, SpriteFont font, IDs id = IDs.DEFAULT) : base(position, id)
        {
            this.font = font;
            box = SpriteHandler.GetSprite((int)IDs.EMPTYPART);
            box.Position = position;
            box.Origin -= new Vector2(-halfSpriteSize, -halfSpriteSize); // corrects origin to sprite 
            box.Scale *= SCALEFACTOR;
            if (id == IDs.RECTHULLPART)
                Sprite.Scale *=  0.7f;
            BoundBox = new Rectangle((int)position.X,(int)position.Y, (int) (32 * SCALEFACTOR),(int) (32 * SCALEFACTOR));
            BoundBox.Offset(-halfSpriteSize * SCALEFACTOR, -halfSpriteSize * SCALEFACTOR); 
            SetPrice();
            SetName();
            Vector2 belowItemVector = new Vector2(position.X, position.Y + halfSpriteSize * SCALEFACTOR + belowItemSpacing);
            Vector2 belowNameVector = new Vector2(belowItemVector.X, belowItemVector.Y + namePriceSpacing);
            belowItemPosition = DrawHelper.CenteredWordPosition(Name, font, belowItemVector);
            belowNamePosition = DrawHelper.CenteredWordPosition("$" + Price.ToString(),font, belowNameVector);

        }

        public Part ReturnPart()
        {
            Part p = null;
            if (id != IDs.EMPTYPART && id != IDs.ROTATEPART)
            {
                Type t = EntityConstants.IDTOTYPE[id];
                p = (Part)Activator.CreateInstance(t, IDs.DEFAULT); //! Needs to be changed if Part takes more than one constructor
            }
            return p;
        }

        private void SetPrice()
        {
            Price = (int)EntityConstants.GetStatsFromID(EntityConstants.PRICE, id);
        }
        private void SetName()
        {
            Name = EntityConstants.GetStatsFromID(EntityConstants.NAME, id);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawHelper.DrawOutlinedString(spriteBatch, 2, new Color(32, 32, 32), font, Name, belowItemPosition, Color.Wheat, 0, Vector2.Zero, 1);
            if (!(id.Equals(IDs.ROTATEPART) || id.Equals( IDs.EMPTYPART)))
            {
                DrawHelper.DrawOutlinedString(spriteBatch, 2, new Color(32, 32, 32), font, "$" + Price, belowNamePosition, Color.Gold, 0, Vector2.Zero, 1);
                box.Draw(spriteBatch, gameTime);
            }
            base.Draw(spriteBatch, gameTime);
        }
    }
}
