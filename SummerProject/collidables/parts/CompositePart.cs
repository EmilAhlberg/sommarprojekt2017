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
            if (parts[slot].SetPart(p))
            {
                p.Hull = this;
                return true;
            }
            return false;
        }

        protected abstract void AddLinkPositions();

        protected class Link
        {
            public Vector2 LinkPos { private set; get; }
            public float Angle { private set; get; }
            public Part Part { private set; get; } = null;

            public Link(Vector2 linkPos, float angle)
            {
                LinkPos = linkPos;
                Angle = angle;
            }
        
            public bool SetPart(Part p)
            {
                Vector2 prevPos = p.Position;
                float prevAngle = p.angle;
                p.Position = LinkPos;
                p.angle = Angle;
                if (p.BoundBoxes[0].Intersects(null)/*TODO : INTERSECTAR ANDRA PARTS??*/)
                {
                    p.Position = prevPos;
                    p.angle = prevAngle;
                    return false;
                }
                Part = p;
                return true;
            }
        }

    }
}
