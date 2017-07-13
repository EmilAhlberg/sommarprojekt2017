using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts.guns
{
    class GravityGunPart : GunPart
    {
        public GravityGunPart(IDs id = IDs.DEFAULT) : base(id, IDs.GRAVITYBULLET)
        {
            RELOADTIME = 7f;
            reloadTimer = new Timer(RELOADTIME);
            reloadTimer.Finish();
        }
    }
}
