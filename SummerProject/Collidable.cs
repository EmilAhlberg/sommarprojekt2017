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
        public Rectangle BoundBox {protected set; get;}
    }
}
