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

        public override void TakeAction()
        {
            if (Carrier is CompositePart)
                (Carrier as CompositePart).AddForce(EntityConstants.THRUST[EntityConstants.PLAYER], ThrusterAngle);
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(AbsolutePosition, 4, Angle, Color.MonoGameOrange);
        }
    }
}
