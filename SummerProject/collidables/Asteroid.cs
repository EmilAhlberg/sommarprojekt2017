using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class Asteroid : Enemy
    {
        public Asteroid(Vector2 position, ISprite sprite, Player player) : base(position, sprite, player)
        {
            WorthScore = 0;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY] * 2;
            Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            float dX = Position.X - target.X;
            float dY = Position.Y - target.Y;
            base.CalculateAngle(dX, dY);
        }
    }
}
