using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    class SprayGunPart : GunPart
    {
        public SprayGunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            bulletCap = 10;
            RELOADTIME = 0.02f;
            reloadTimer = new Timer(RELOADTIME);
            bulletID = IDs.SPRAYBULLET;
            projectiles.AddExtraBullets((int)bulletID, bulletCap);
            bullet = (Projectile)projectiles.GetEntity((int)bulletID);
        }
    }
}
