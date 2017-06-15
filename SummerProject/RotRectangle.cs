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
        public int Width { set; get; }
        public int Height { set; get; }
        //public Point Location { set; get; }

        public RotRectangle(Rectangle rect, float angleRad)
        {
            this.UL = new Vector2(rect.Left, rect.Top);
            this.DL = new Vector2(rect.Left, rect.Bottom);
            this.DR = new Vector2(rect.Right, rect.Bottom);
            this.UR = new Vector2(rect.Right, rect.Top);
            //Location = rect.Location;
            Width = rect.Width;
            Height = rect.Height;
            Rotate(angleRad);
        }

        public void Rotate(float angleRad)
        {
            Matrix rotation = Matrix.CreateRotationZ(angleRad);
            Vector2 pos = new Vector2((DR.X + UL.X)/2, (DR.Y + UL.Y)/2);
            UL = Vector2.Add(pos, Vector2.Transform(UL - pos, rotation));
            DL = Vector2.Add(pos, Vector2.Transform(DL-pos, rotation));
            DR = Vector2.Add(pos, Vector2.Transform(DR - pos, rotation));
            UR = Vector2.Add(pos, Vector2.Transform(UR - pos, rotation));
        }

        public bool Intersects(RotRectangle r)
        {
            //Console.WriteLine("Coord1 : " + UL + ", " + DL + ", " + DR + ", " + UR + " Coord2 : " + r.UL + ", " + r.DL + ", " + r.DR + ", " + r.UR);
            Vector2[] axes = GenerateAxes(r);
            //Console.WriteLine("Axes : " + axes[0] + ", " + axes[1] + ", " + axes[2] + ", " + axes[3]);
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
                //Console.WriteLine("A0 : " + scalarA[0]);
                //Console.WriteLine("A1 : " + scalarA[1]);
                //Console.WriteLine("A2 : " + scalarA[2]);
                //Console.WriteLine("A3 : " + scalarA[3]);
                //Console.WriteLine("B0 : " + scalarB[0]);
                //Console.WriteLine("B1 : " + scalarB[1]);
                //Console.WriteLine("B2 : " + scalarB[2]);
                //Console.WriteLine("B3 : " + scalarB[3]);
                //Console.WriteLine("A: min - " + scalarA.Min() + " max - " + scalarA.Max() + " B: min - " + scalarB.Min() + " max - " + scalarB.Max());
               // if (!(scalarB.Min() <= scalarA.Max() && scalarB.Max() >= scalarA.Min()))
               if(scalarB.Max() < scalarA.Min() || scalarA.Max() < scalarB.Min() )
                    return false;
            }
            return true;
        }

        public Vector2[] GenerateAxes(RotRectangle r)
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
//Vector2 v = Vector2.Divide(Vector2.Multiply(axis, Vector2.Dot(UL, axis)), axis.LengthSquared());