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
        public MineGunPart(ISprite sprite) : base(sprite)
        {
            RELOADTIME = 0.1f;
            reloadTimer = new Timer(RELOADTIME);
        }
        protected override void Fire()
        {
            projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), (int)IDs.MINEBULLET);
        }
    }
}
