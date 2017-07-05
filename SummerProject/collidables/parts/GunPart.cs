using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.factories;
using Microsoft.Xna.Framework.Input;

namespace SummerProject.collidables.parts
{
    class GunPart : Part
    {
        public static Projectiles projectiles;
        protected float RELOADTIME = 0.3f; //!
        protected Timer reloadTimer;

        public GunPart(ISprite sprite) : base(sprite)
        {
            this.Sprite = sprite;
            reloadTimer = new Timer(RELOADTIME);
        }

        public override void TakeAction()
        {
            if (reloadTimer.IsFinished)
            {
                Fire();
                reloadTimer.Reset();
            }
        }
        protected virtual void Fire()
        {
            projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), (int)IDs.DEFAULT_BULLET);
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
        }
    }
}
