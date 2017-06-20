using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    class RectangularHull : CompositePart
    {
        public RectangularHull(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        public override void Collision(Collidable c2)
        {
            throw new NotImplementedException();
        }

        protected override void AddLinkPositions()
        {
            throw new NotImplementedException();
        }
    }
}
