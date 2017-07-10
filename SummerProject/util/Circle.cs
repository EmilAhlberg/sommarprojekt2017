using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.util
{
  public class Circle
    {
        public int Radius { get; set; }
        public Vector2 Position { get; set; }

        public Circle (int radius)
        {
            Radius = radius;
        }

        public bool Intersects(Circle c)
        {
            return (c.Position - Position).Length() <= Radius + c.Radius;
        }

    }
}
