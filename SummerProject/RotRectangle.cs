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
        private Vector2 c1 {get;}
        private Vector2 c2 {get;}

        public RotRectangle(float x1, float y1, float x2, float y2)
        {
            this.c1 = new Vector2(x1,y1);
            this.c2 = new Vector2(x2,y2);
        }
    }
}
