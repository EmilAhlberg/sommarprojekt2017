using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class RotRectangle
    {
        private Vector2 UL { set; get; }
        private Vector2 DL { set; get; }
        private Vector2 DR { set; get; }
        private Vector2 UR { set; get; }
        public float Width { set; get; }
        public float Height { set; get; }

        public RotRectangle(Rectangle rect, float angleRad)
        {
            this.UL = new Vector2(rect.Left, rect.Top);
            this.DL = new Vector2(rect.Left, rect.Bottom);
            this.DR = new Vector2(rect.Right, rect.Bottom);
            this.UR = new Vector2(rect.Right, rect.Top);
            Width = rect.Width;
            Height = rect.Height;
            Rotate(angleRad);
        }

        public void Rotate(float angleRad)
        {
            Matrix rotate = Matrix.CreateRotationZ(angleRad);
            UL = Vector2.Transform(UL, rotate);
            DL = Vector2.Transform(DL, rotate);
            DR = Vector2.Transform(DR, rotate);
            UR = Vector2.Transform(UR, rotate);
        }

        public bool intersects(RotRectangle r)
        {
            Vector2[] axes = new Vector2[4];
            axes[0] = new Vector2(UR.X - UL.X, UR.Y - UL.Y);
            axes[1] = new Vector2(UR.X - DR.X, UR.Y - DR.Y);
            axes[2] = new Vector2(r.UL.X - r.DL.X, r.UL.Y - r.DL.Y);
            axes[3] = new Vector2(r.UL.X - r.UR.X, r.UL.Y - r.UR.Y);
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
                if (scalarB.Min() <= scalarA.Max() || scalarB.Max() >= scalarA.Min())
                    return true;
            }
            return false;
        }
    }
}
