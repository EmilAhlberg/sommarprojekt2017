using Microsoft.Xna.Framework;
using System;

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
