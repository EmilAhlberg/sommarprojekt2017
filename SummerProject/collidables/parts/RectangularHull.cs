using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject
{
    public class RectangularHull : CompositePart
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
        public void ResetLinks()
        {
            AddLinkPositions();
            TakenPositions = new bool[4];
        }

        internal void RemovePart(Part part)
        {
            for(int i = 0; i < 4; i++)
            {
                if (parts[i].Part == part)
                {
                    float tempAngle = -(float)Math.PI / 2 + i*(float)Math.PI/2;
                    parts[i] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
                }
            }
        }
        internal List<RectangularHull> GetHulls()
        {
            List<RectangularHull> hulls = new List<RectangularHull>();
            foreach(Link l in parts)
            {
                if (l.Part is RectangularHull)
                {
                    hulls.Add((RectangularHull)l.Part);
                }
            }
            return hulls;
        }

        internal int Carries(RectangularHull part)
        {
            int pos = -1;
            for (int i = 0; i < 4; i ++)
            {
                if (parts[i].Part == part)
                    return i;
            }
            return pos;
        }

        internal void RemoveAt(int linkPosition)
        {
            float tempAngle = -(float)Math.PI / 2 + linkPosition * (float)Math.PI / 2;
            parts[linkPosition] = new Link(new Vector2((float)Math.Cos(tempAngle), (float)Math.Sin(tempAngle)) * new Vector2(BoundBox.Width, BoundBox.Height) / 2, tempAngle);
        }

        protected override void ResetTakenPositions()
        {
           for (int i = 0; i< TakenPositions.Length; i++)
            {
                TakenPositions[i] = false;
            }
        }
    }
}
