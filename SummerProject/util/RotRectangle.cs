using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace SummerProject
{
    public class RotRectangle
    {
        private Vector2 UL { set; get; }
        private Vector2 DL { set; get; }
        private Vector2 DR { set; get; }
        private Vector2 UR { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        private Vector2 origin;
        public Vector2 Origin
        {
            set
            {
                Matrix rotation = Matrix.CreateRotationZ(angle);
                origin = Vector2.Transform(value, rotation);
                origin = value;
            }
            private get { return origin; }
        }
        private float angle = 0;
        public float Angle
        {
            set
            {
                Rotate(value - angle);
            }
            get { return angle; }
        }
        private Vector2 location;
        public Vector2 Location
        {
            set
            {
                Vector2 DRUL = (DR - UL) / 2;
                Vector2 URDL = (UR - DL) / 2;
                UL = value + origin - DRUL;
                DR = value + origin + DRUL;
                DL = value + origin - URDL;
                UR = value + origin + URDL;
                location = value;
            }
            get { return location; }
        }

        public RotRectangle(Rectangle rect, float angleRad)
        {
            this.UL = new Vector2(rect.Left, rect.Top);
            this.DL = new Vector2(rect.Left, rect.Bottom);
            this.DR = new Vector2(rect.Right, rect.Bottom);
            this.UR = new Vector2(rect.Right, rect.Top);
            Location = new Vector2(rect.Location.X, rect.Location.Y);
            Width = rect.Width;
            Height = rect.Height;
            Rotate(angleRad);
            origin = new Vector2(rect.Width / 2, rect.Height / 2);
        }

        public void Rotate(float angleRad)
        {
            angle = (angle + angleRad) % (float)(2 * Math.PI);
            Matrix rotation = Matrix.CreateRotationZ(angleRad);
            UL = Vector2.Add(location, Vector2.Transform(UL - location - origin, rotation));
            DL = Vector2.Add(location, Vector2.Transform(DL - location - origin, rotation));
            DR = Vector2.Add(location, Vector2.Transform(DR - location - origin, rotation));
            UR = Vector2.Add(location, Vector2.Transform(UR - location - origin, rotation));
            Location = Vector2.Transform(location, rotation);
        }

        public bool Intersects(RotRectangle r)
        {
            Vector2[] axes = GenerateAxes(r);
            float[] scalarA = new float[4];
            float[] scalarB = new float[4];
            foreach (Vector2 axis in axes)
            {
                scalarA[0] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(UL, axis) / axis.LengthSquared()));
                scalarA[1] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(DL, axis) / axis.LengthSquared()));
                scalarA[2] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(DR, axis) / axis.LengthSquared()));
                scalarA[3] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(UR, axis) / axis.LengthSquared()));
                scalarB[0] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(r.UL, axis) / axis.LengthSquared()));
                scalarB[1] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(r.DL, axis) / axis.LengthSquared()));
                scalarB[2] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(r.DR, axis) / axis.LengthSquared()));
                scalarB[3] = Vector2.Dot(axis, Vector2.Multiply(axis, Vector2.Dot(r.UR, axis) / axis.LengthSquared()));
                if (scalarB.Max() < scalarA.Min() || scalarA.Max() < scalarB.Min())
                    return false;
            }
            return true;
        }

        private Vector2[] GenerateAxes(RotRectangle r)
        {
            Vector2[] axes = new Vector2[4];
            axes[0] = new Vector2(UR.X - UL.X, UR.Y - UL.Y);
            axes[1] = new Vector2(UR.X - DR.X, UR.Y - DR.Y);
            axes[2] = new Vector2(r.UL.X - r.DL.X, r.UL.Y - r.DL.Y);
            axes[3] = new Vector2(r.UL.X - r.UR.X, r.UL.Y - r.UR.Y);
            return axes;
        }
    }
}
