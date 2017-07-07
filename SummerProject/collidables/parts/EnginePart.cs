﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.parts
{
    class EnginePart : Part
    {
        public EnginePart(IDs id = IDs.DEFAULT) : base(id)
        {
        }

        public override void TakeAction()
        {
            if (Carrier is CompositePart)
                (Carrier as CompositePart).AddForce(EntityConstants.GetStatsFromID(EntityConstants.THRUST, IDs.PLAYER), ThrusterAngle);
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(Position, 4, Angle+(float)Math.PI, Color.MonoGameOrange);
        }
    }
}
