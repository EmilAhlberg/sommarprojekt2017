using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    class SprayGunPart : GunPart
    {
        public SprayGunPart(ISprite sprite) : base(sprite)
        {
            RELOADTIME = 0.1f;
            reloadTimer = new Timer(RELOADTIME);
        }
    }
}
