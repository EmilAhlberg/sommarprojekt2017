using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public abstract class Collidable
    {
        public Vector2 PrevPos { get; set; }
        public Rectangle BoundBox { get; set; }
        public Vector2 Position { get; set; }
        public bool IsStatic { get; set; }
    }
}
