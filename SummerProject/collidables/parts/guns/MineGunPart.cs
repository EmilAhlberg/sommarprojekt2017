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
    class MineGunPart : GunPart
    {
        public MineGunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            RELOADTIME = 0.4f;
            bulletCap = 10;
            reloadTimer = new Timer(RELOADTIME);
            reloadTimer.Finish();
            bulletID = IDs.MINEBULLET;
            projectiles.AddExtraBullets((int)bulletID, bulletCap);
            bullet = (Projectile)projectiles.GetEntity((int)bulletID);
        }

    }
}
