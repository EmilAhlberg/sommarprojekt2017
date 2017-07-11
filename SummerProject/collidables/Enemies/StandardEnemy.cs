﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class StandardEnemy : Enemy
    {
        public StandardEnemy(Vector2 position, Player player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Particles.GenerateParticles(Position, 4, Angle, Color.Green);
            base.Update(gameTime);
        }

        public override void Death()
        {
            base.Death();
            //Particles.GenerateParticles(Position, 2, angle, sprite.MColor); //Death animation
        }
    }
}
