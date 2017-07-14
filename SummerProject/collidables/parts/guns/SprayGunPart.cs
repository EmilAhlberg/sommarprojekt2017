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
        public SprayGunPart(IDs id = IDs.DEFAULT) : base(id, IDs.SPRAYBULLET)
        {
            RELOADTIME = 0.02f;
            reloadTimer = new Timer(RELOADTIME);
            BulletSpeed = 30;
            EvilBulletSpeed = 15;
            projectiles.AddExtraBullets((int)bulletID, bulletCap); //Extra 5 bullets for this gun
        }
    }
}
