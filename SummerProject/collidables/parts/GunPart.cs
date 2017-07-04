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
                projectiles.Fire(AbsolutePosition, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)), EntityTypes.BULLET); //!
                reloadTimer.Reset();
            }
        }

        public override void Update(GameTime gameTime)
        {
            reloadTimer.CountDown(gameTime);
        }
    }
}
