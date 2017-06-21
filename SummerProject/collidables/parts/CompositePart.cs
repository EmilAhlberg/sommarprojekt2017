using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class CompositePart : Part
    {
        protected Link[] parts;

        public CompositePart(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            AddLinkPositions();
        }

        public bool AddPart(Part p, int slot) {
            if (slot < parts.Length)
            {
                p.Hull = this;
                SetPart(p, slot);
                return true;
            }
            return false;
        }

        private void SetPart(Part p, int slot)
        {
            p.Position = Position;
            parts[slot].SetPart(p);
        }

        public void UpdateParts(float angle)
        {
            Matrix rot = Matrix.CreateRotationZ(angle);
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Part != null)
                {
                    parts[i].RelativePos = Vector2.Transform(parts[i].RelativePos, rot);
                    SetPart(parts[i].Part,i);
                    if(parts[i].Part is CompositePart)
                    {
                        ((CompositePart)parts[i].Part).UpdateParts(angle);
                    }
                }
            }
        }

        protected override void Move()
        {
            float prevAngle = angle;
            base.Move();
            UpdateParts(angle-prevAngle);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            base.Draw(sb, gameTime);
            foreach(Link p in parts)
            {
                if (p.Part != null)
                    p.Part.Draw(sb, gameTime);
            }

        }

        protected abstract void AddLinkPositions();

        protected class Link
        {
            public Vector2 RelativePos { set; get; }
            public float Angle { get { return (float)Math.Atan((RelativePos.Y / RelativePos.X)); } }
            public Part Part { private set; get; } = null;

            public Link(Vector2 relativePos, float angle)
            {
                RelativePos = relativePos;
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

