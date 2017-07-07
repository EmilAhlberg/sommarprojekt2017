using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
        public Vector2 AbsolutePosition { get { return (UL + DR) / 2; } }
        private Vector2 origin { set; get; }
        public Vector2 Origin
        {
            set
            {
                origin = value;
                UpdateRotation();
            }
            get
            {
                return origin;
            }
        }
        private Vector2 position;
        public Vector2 Position
        {
            set
            {
                Vector2 change = value - position;
                UL += change;
                DL += change;
                DR += change;
                UR += change;
                position = value;
            }
            get
            {
                return position;
            }
        }
        private float angle = 0;
        public float Angle
        {
            set
            {
                angle = value;
                UpdateRotation();
            }
            get { return angle; }
        }
        public RotRectangle(Rectangle rect, float angle)
        {
            UL = new Vector2(rect.Left, rect.Top);
            DL = new Vector2(rect.Left, rect.Bottom);
            DR = new Vector2(rect.Right, rect.Bottom);
            UR = new Vector2(rect.Right, rect.Top);
            position = Vector2.Zero;
            Width = rect.Width;
            Height = rect.Height;
            origin = new Vector2(Width / 2, Height / 2);
            Angle = angle;
        }

        private void UpdateRotation()
        {
            Matrix rotation = Matrix.CreateRotationZ(angle);
            Vector2 height = new Vector2(0, Height);
            Vector2 width = new Vector2(Width, 0);
            UL = Position + Vector2.Transform(-Origin, rotation);
            DL = Position + Vector2.Transform(-Origin+height, rotation);
            DR = Position + Vector2.Transform(-Origin+height+width, rotation);
            UR = Position + Vector2.Transform(-Origin+width, rotation);
        }

        public bool Contains(Point point)
        {
            List<float> xVals = new List<float>(new float[] { UL.X, DL.X, DR.X, UR.X });
            List<float> yVals = new List<float>(new float[] { UL.Y, DL.Y, DR.Y, UR.Y });
            return xVals.Max() > point.X && xVals.Min() < point.X && yVals.Max() > point.Y && yVals.Min() < point.Y;
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

       public Vector2 MiddleToDirectionSideIntersections(Vector2 direction)
        {
            
            ////Vector2 kRay = direction - AbsolutePosition; kRay.Normalize();
            ////Vector2 k1 = UR-DR; k1.Normalize();
            ////Vector2 k22 = UR - UL; k22.Normalize();
            ////Vector2
            //Vector2 v = direction - AbsolutePosition; v.Normalize();
            //Vector2 u1 = UR - DL; u1.Normalize();
            //Matrix m = new Matrix( v.X, u1.X,
            //                       v.Y, u1.Y);
            //Matrix.Invert()
            //float d = v.X * (UR.Y - UL.Y) - v.Y * (UR.X - UL.X);
            //return Position + v * Matrix.Transform(UL,Matrix.Invert(new Matrix(v.X,u1.X,v.Y,u1.Y));
            return Vector2.Zero;
        }

    }
}
