﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.bullets
{
    class MineBullet : Bullet
    {
        public MineBullet(Vector2 position) : base(position)
        {
        }

        protected override void TrailParticles()
        {
        }
    }
}
