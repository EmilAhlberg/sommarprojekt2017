using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.events.buildmenu;

namespace SummerProject
{
    public class ShipItem : ClickableItem
    {        
        public int LinkPosition;
        public Part Part;
        public RectangularHull Hull;

        public ShipItem(Vector2 position, int linkPosition, RectangularHull hull, Part part,  IDs id = IDs.DEFAULT) : base(position, id)
        {
            angle = -(float)Math.PI / 2 * linkPosition;

            if (linkPosition % 2 == 0)
                angle += (float)Math.PI;

            Hull = hull;
            Part = part;
            //Width = Sprite.SpriteRect.Width * SCALEFACTOR;
            //Height = Sprite.SpriteRect.Height * SCALEFACTOR;
            LinkPosition = linkPosition;
        }

        public void UpdateRotation()
        {
            angle = -(float)Math.PI / 2 * LinkPosition;
            if (LinkPosition % 2 == 1)
                angle += (float)Math.PI;
        }
    }
}
