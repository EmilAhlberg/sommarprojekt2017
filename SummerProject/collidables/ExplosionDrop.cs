using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class ExplosionDrop : Drop
    {
        private const int damage = 10;
        private bool exploding;
        private RotRectangle originalBoundBox;
        private Timer explosionTimer;
        private const float explosionTime = 1f;
        private const int explosionSize = 500;
        public ExplosionDrop(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            explosionTimer = new Timer(explosionTime);
            originalBoundBox = BoundBoxes[0];
        }
        public override void Collision(Collidable c2)
        {
            if (c2 is Player)
                Explode();
        }
        public override void Update(GameTime gameTime)
        {
            if (exploding)
            {
                Particles.GenerateParticles(Position, 9);
                explosionTimer.CountDown(gameTime);
            }
            if (explosionTimer.IsFinished)
                Death();
            base.Update(gameTime);
        }

        private void Explode()
        {
          exploding = true;
            Damage = damage;
            BoundBoxes[0] =  new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, explosionSize, explosionSize), 0);
        }

        public override void Death()
        {
            exploding = false;
            explosionTimer.Reset();
            BoundBoxes[0] = originalBoundBox;
            Particles.GenerateParticles(Position, 8); //Death animation
            Damage = 0;
            base.Death();
        }
    }
}
