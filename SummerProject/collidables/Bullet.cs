using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Bullet : Projectile
    {               

        public Bullet(ISprite sprite) : base(sprite)
        {
            this.sprite = sprite;
            Damage = 10; //!   
        }
            

        protected override void Move()
        {
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y + (float)Math.Sin(angle));
        }

        public override void Collision(Collidable c2)
        {
            if(c2 is Enemy || c2 is Wall)
            {
                Death();
            }
        }
    }
}
