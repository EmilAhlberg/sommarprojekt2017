using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject
{
    class RectangularHull : CompositePart
    {
        public RectangularHull(Vector2 position, ISprite sprite, IPartCarrier hull) : base(position, sprite, hull)
        {
        }

        public override void Collision(Collidable c2)
        {
            throw new NotImplementedException();
        }

        protected override void AddLinkPositions()
        {
            parts = new Link[4];
            float tempAngle = angle;
            parts[0] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * BoundBoxes[0].Width / 2, tempAngle);
            tempAngle += (float)Math.PI;
            parts[1] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * BoundBoxes[0].Height / 2, tempAngle);
            tempAngle += (float)Math.PI;
            parts[2] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * BoundBoxes[0].Width / 2, tempAngle);
            tempAngle += (float)Math.PI;
            parts[3] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * BoundBoxes[0].Height / 2, tempAngle);
        }
    }
}
