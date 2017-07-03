using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.parts
{
    class EnginePart : Part
    {
        public EnginePart(ISprite sprite) : base(sprite)
        {

        }

        public override void TakeAction(Type type)
        {
            if(Carrier is CompositePart)
                (Carrier as CompositePart).AddForce(10, Angle);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
