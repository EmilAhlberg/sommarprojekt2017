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
        public RectangularHull(IDs id = IDs.DEFAULT) : base(id)
        {
        }

        protected override void AddLinkPositions()
        {
            parts = new Link[4];
            float tempAngle = -(float)Math.PI/2;
            parts[0] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
            tempAngle += (float)Math.PI/2;
            parts[1] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
            tempAngle += (float)Math.PI/2;
            parts[2] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
            tempAngle += (float)Math.PI/2;
            parts[3] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
        }
    }
}
