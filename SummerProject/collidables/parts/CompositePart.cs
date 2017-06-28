﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables.parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class CompositePart : Part, IPartCarrier
    {
        protected Link[] parts;

        public CompositePart(Vector2 position, ISprite sprite, IPartCarrier carrier) : base(position, sprite, carrier)
        {
            AddLinkPositions();
        }

        public bool AddPart(Part part, int pos) {
            if (pos < parts.Length)
            {
                part.Carrier = this;
                SetPart(part, pos);
                return true;
            }
            return false;
        }

        private void SetPart(Part p, int pos)
        {
            p.Position = Position;
            parts[pos].SetPart(p);
        }

        public void UpdateParts(float angle)
        {
            Matrix rot = Matrix.CreateRotationZ(angle);
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Part != null)
                {
                    parts[i].RelativePos = Vector2.Transform(parts[i].RelativePos, rot);
                    parts[i].Angle += angle;
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

        public List<Part> GetParts()
        {
            List<Part> totalParts = new List<Part>();
            totalParts.Add(this);

            foreach(Link p in parts)
            {
                if (p.Part != null) {
                    if (p.Part is CompositePart)
                        totalParts.AddRange(((CompositePart)p.Part).GetParts());
                    else
                        totalParts.Add(p.Part);
                }
            }
            return totalParts;  
        }

        protected abstract void AddLinkPositions();

        protected class Link
        {
            public Vector2 RelativePos { set; get; }
            public float Angle { set;get; }
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
