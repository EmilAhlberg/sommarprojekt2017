using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public static class WindowSize
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public const int OOBOFFSET = 100;
     

        static WindowSize()
        {
            Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        }

        public static bool IsOutOfBounds(Vector2 position)
        {
            return position.X > Width + OOBOFFSET || position.Y > Height + OOBOFFSET || position.X < -OOBOFFSET || position.Y < -OOBOFFSET;
        }
    }
}
