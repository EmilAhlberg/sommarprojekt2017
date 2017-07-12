﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts.guns
{
    class GravityGunPart : GunPart
    {
        public GravityGunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            RELOADTIME = 7f;
            reloadTimer = new Timer(RELOADTIME);
            bulletID = IDs.CHARGINGBULLET;
            projectiles.AddExtraBullets((int)bulletID, bulletCap);
        }
        protected override void Fire()
        {
            projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), (int)IDs.GRAVITYBULLET);
        }
    }
}
