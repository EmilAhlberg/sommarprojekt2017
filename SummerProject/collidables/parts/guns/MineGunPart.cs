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
        public MineGunPart(IDs id = IDs.DEFAULT) : base(id, IDs.MINEBULLET)
        {
            RELOADTIME = 0.4f;
            reloadTimer = new Timer(RELOADTIME);
            reloadTimer.Finish();
            projectiles.AddExtraBullets((int)bulletID, bulletCap); //Extra 5 bullets for this gun

        }

    }
}
