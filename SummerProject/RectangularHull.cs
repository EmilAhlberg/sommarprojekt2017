using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    class RectangularHull : Part, CompositePart
    {
        public RectangularHull(Vector2 position, ISprite sprite, CompositePart Hull) : base(position, sprite, Hull)
        {
            Parts = new Part[4];
        }

        public Part[] Parts { get; }

        protected override CompositePart Hull
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void addPart(Part p)
        {
            throw new NotImplementedException();
        }

        public override void Collision(Collidable c2)
        {
            throw new NotImplementedException();
        }

        public void SubPartDamaged()
        {
            throw new NotImplementedException();
        }
    }
}
