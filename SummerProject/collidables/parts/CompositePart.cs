using Microsoft.Xna.Framework;
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
        public new float friction { set { base.friction = value; } get { return base.friction; } }
        public override Color Color
        {
            set
            {
                base.Color = value;
                foreach (Link p in parts)
                    if (p.Part != null)
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
        public override float ThrusterAngle
        {
            set
            {
                base.ThrusterAngle = value;
                foreach (Link p in parts)
                {
                    if (p.Part != null)
                    {
                        p.Part.ThrusterAngle = value;
                    }
                }
            }
        }

        public CompositePart(ISprite sprite) : base(sprite)
        {
            AddLinkPositions();
        }

        public bool AddPart(Part part, int pos)
        {
            part.LinkPosition = pos; //!!! change
            if (pos >= 0 && pos < parts.Length)
            {
                part.Carrier = this;
                parts[pos].SetPart(part, this);
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            foreach (Link p in parts)
            {
                if (p.Part != null)
                {
                    p.Part.Update(gameTime);
                }
            }
        }

        //protected void UpdatePartsPos()
        //{
        //    for (int i = 0; i < parts.Length; i++)
        //    {
        //        if (parts[i].Part != null)
        //        {
        //            //parts[i].SetPart(parts[i].Part, this);
        //            parts[i].Part.angle = angle;
        //            parts[i].Part.Position = Position;


        //            if(parts[i].Part is CompositePart)
        //            {   
        //                ((CompositePart)parts[i].Part).UpdatePartsPos();
        //            }
        //        }
        //    }
        //}

        public override void AddForce(float force, float angle)
        {
            if (Carrier is CompositePart)
                (Carrier as CompositePart).AddForce(force, angle);
            else
                base.AddForce(force, angle);
        }

        public override Vector2 Position
        {
            set
            {
                base.Position = value;
                foreach (Link p in parts)
                {
                    if (p.Part != null)
                    {
                        p.SetPart(p.Part, this);
                    }
                }
            }
        }

        public override float Angle
        {
            set
            {
                base.Angle = value;
                foreach (Link p in parts)
                {
                    if (p.Part != null)
                    {
                        p.SetPart(p.Part, this);
                    }
                }
            }
        }

        //public void Move()
        //{
        //    base.Move();
        //    //UpdatePartsPos();
        //}

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            base.Draw(sb, gameTime);
            foreach (Link p in parts)
            {
                if (p.Part != null)
                    p.Part.Draw(sb, gameTime);
            }
        }

        //public override Vector2 Origin
        //{
        //    set
        //    {
        //        base.Origin = value;
        //        foreach (Link p in parts)
        //            if(p.Part != null)
        //                p.Part.Origin = value;
        //    }
        //}

        public override void Death()
        {
            foreach (Link p in parts)
                if (p.Part != null)
                    p.Part.Death();
            base.Death();
        }

        public void TakeAction(Type type)
        {
            if (type == GetType())
                TakeAction();
            foreach (Link p in parts)
            {
                if (p.Part != null)
                    if (p.Part is CompositePart)
                        (p.Part as CompositePart).TakeAction(type);
                    else if (p.Part.GetType() == type)
                        p.Part.TakeAction();
            }
        }

        public override void TakeAction()
        {
        }

        //public override bool CollidesWith(Collidable c2)
        //{

        //}

        protected abstract void AddLinkPositions();

        protected class Link
        {

            public Vector2 RelativePos { set; get; }
            public float RelativeAngle { set; get; }
            public Part Part { private set; get; } = null;

            public Link(Vector2 relativePos, float angle)
            {
                RelativePos = relativePos;
                RelativeAngle = angle;
            }

            public void SetPart(Part p, CompositePart hull)
            {
                Part = p;
                Vector2 linkToCenter = new Vector2(p.BoundBox.Width, p.BoundBox.Height) / 2;
                Vector2 posChange = new Vector2(RelativePos.X, RelativePos.Y);
                posChange.Normalize();
                Matrix rot = Matrix.CreateRotationZ(hull.Angle);
                p.Position = hull.Position + Vector2.Transform(RelativePos + posChange * linkToCenter, rot);
                p.Angle = hull.angle;
                if (!(p is CompositePart))
                    p.Angle += RelativeAngle;
            }
        }
    }
}