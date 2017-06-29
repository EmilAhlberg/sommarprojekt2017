﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
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
        public new float TurnSpeed { set { base.TurnSpeed = value; } get { return base.TurnSpeed; } }
        public new float Thrust { set { base.Thrust = value; } get { return base.Thrust; } }
        public override Color Color {
            set
            {
                base.Color = value;
                foreach (Link p in parts)
                    if(p.Part != null)
                        p.Part.Color = value;
            }
            get
            {
                return base.Color;
            }
        }
        public new float Mass
        {
            set { base.Mass = value; }
            get
            {
                float m = base.Mass;
                foreach (Link p in parts)
                    m += p.Part.Mass;
                return m;
            }
        }
        public List<Part> Parts
        {
            get
            {
                List<Part> totalParts = new List<Part>();
                totalParts.Add(this);

                foreach (Link p in parts)
                {
                    if (p.Part != null)
                    {
                        if (p.Part is CompositePart)
                            totalParts.AddRange((p.Part as CompositePart).Parts);
                        else
                            totalParts.Add(p.Part);
                    }
                }
                return totalParts;
            }
        }

        public CompositePart(ISprite sprite) : base(sprite)
        {
            //BoundBoxes[0].Origin = Vector2.Zero;
            AddLinkPositions();
        }

        public bool AddPart(Part part, int pos) {
            if (pos >= 0 && pos < parts.Length)
            {
                part.Carrier = this;
                parts[pos].SetPart(part, this);
                return true;
            }
            return false;
        }

        protected void UpdatePartsPos()
        {
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Part != null)
                {
                    //parts[i].SetPart(parts[i].Part, this);
                    parts[i].Part.angle = angle;
                    parts[i].Part.Position = Position;
   
                    if(parts[i].Part is CompositePart)
                    {   
                        ((CompositePart)parts[i].Part).UpdatePartsPos();
                    }
                }
            }
        }

        public void Move()
        {
            base.Move();
            UpdatePartsPos();
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
            public float Angle { set; get; }
            public Part Part { private set; get; } = null;

            public Link(Vector2 relativePos, float angle)
            {
                RelativePos = relativePos;
                Angle = angle;
            }

            public void SetPart(Part p, CompositePart hull)
            {
                Vector2 linkToCenter = new Vector2(p.BoundBoxes[0].Width, p.BoundBoxes[0].Height);
                p.Position = hull.Position;
                Vector2 posChange = new Vector2(RelativePos.X, RelativePos.Y);
                posChange.Normalize();
                p.Origin = (hull.Origin - new Vector2(hull.BoundBoxes[0].Width/2, hull.BoundBoxes[0].Height/2)) + p.Origin + RelativePos + posChange * linkToCenter/2; //plussa på hull.RelativePosition-hull.hull.origin-hull.hull.hull.origin osvosv FUCKED UP FAN JOHAN
                p.angle = Angle;
                Part = p;
            }  
        }
    }
}

