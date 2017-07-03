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
        public GunPart(ISprite sprite) : base(sprite)
        {
            this.sprite = sprite;
        }

        public void SwitchBullets(int type)
        {
            projectiles.SwitchBullets(type);
        }
        public override void TakeAction(Type type)
        {
            Console.WriteLine(Angle);
            projectiles.Fire(Position, new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle))); //!
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
