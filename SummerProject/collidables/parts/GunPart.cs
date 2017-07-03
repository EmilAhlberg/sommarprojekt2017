using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject.collidables.parts
{
    class GunPart : Part
    {
        public static Projectiles projectiles;
        private const float RELOADTIME = 0.3f;
        private Timer reloadTimer;

        public GunPart(ISprite sprite) : base(sprite)
        {
            this.sprite = sprite;
            reloadTimer = new Timer(RELOADTIME);
        }

        public void SwitchBullets(int type)
        {
            projectiles.SwitchBullets(type);
        }
        public override void TakeAction(Type type)
        {
            if (reloadTimer.IsFinished)
            {
                projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle))); //!
                reloadTimer.Reset();
            }
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
        }
    }
}
