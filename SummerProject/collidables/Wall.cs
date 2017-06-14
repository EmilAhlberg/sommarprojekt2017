using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables
{
    class Wall : Entity
    {

        public Wall(Vector2 position, ISprite sprite)
             : base(position, sprite)
        {
            Position = position;
            IsStatic = true;
        }

        protected override void Move()
        {
            throw new NotImplementedException();
        }

        public override void Collision(Collidable c2)
        {
        }

        public override void Death()
        {
            throw new NotImplementedException();
        }
    }
}
