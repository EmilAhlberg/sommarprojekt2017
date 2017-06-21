using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class CompositePart : Collidable
    {
        protected Link[] parts;

        public CompositePart(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            AddLinkPositions();
        }

        public bool AddPart(Part p, int slot) {
            if (slot < parts.Length)
            {
                p.Position = Position;
                parts[slot].SetPart(p);
                p.Hull = this;
                return true;
            }
            return false;
        }

        protected abstract void AddLinkPositions();

        protected class Link
        {
            public Vector2 RelativePos { private set; get; }
            public float Angle { private set; get; }
            public Part Part { private set; get; } = null;

            public Link(Vector2 relativePos, float angle)
            {
                RelativePos = relativePos;
                Angle = angle;
            }

            public void SetPart(Part p)
            {
                p.Origin = -RelativePos - new Vector2((float)Math.Cos(Angle) * p.BoundBoxes[0].Width, (float)Math.Sin(Angle) * p.BoundBoxes[0].Height);
                p.angle = Angle;
                Part = p;
            }
        }

    }
}

