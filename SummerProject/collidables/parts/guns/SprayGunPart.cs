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
        private const float randomAngleOffset = 0.3f;
        protected override float firingAngle { get { return Angle + (SRandom.NextFloat() - 0.5f) * randomAngleOffset; } }

        public SprayGunPart(IDs id = IDs.DEFAULT) : base(id, IDs.SPRAYBULLET)
        {
            RELOADTIME = 0.02f;
            reloadTimer = new Timer(RELOADTIME);
            BulletSpeed = 30;
            EvilBulletSpeed = 20;
            projectiles.AddExtraBullets((int)bulletID, bulletCap); //Extra 5 bullets for this gun
        }
    }
}
