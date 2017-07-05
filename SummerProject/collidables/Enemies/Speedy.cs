using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class Speedy : Enemy
    {
        public Speedy(Vector2 position, ISprite sprite, Player player) : base(position, sprite, player)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(Position, 4, Angle, Color.Green);
            base.Update(gameTime);
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            Thrust = 1.5f * EntityConstants.THRUST[(int)IDs.DEFAULT_ENEMY];
        }

        public override void Death()
        {
            //Particles.GenerateParticles(Position, 17, angle, sprite.MColor); //Death animation
            base.Death();
        }
    }
}
