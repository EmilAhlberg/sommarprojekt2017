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
        public GravityGunPart(IDs id = IDs.DEFAULT) : base(id)
        {
            RELOADTIME = 0.4f;
            reloadTimer = new Timer(RELOADTIME);
        }
        protected override void Fire()
        {
            projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), (int)IDs.GRAVITY_BULLET);
        }
    }
}
