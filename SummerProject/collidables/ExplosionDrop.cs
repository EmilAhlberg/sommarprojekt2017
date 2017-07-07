using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    class ExplosionDrop : Drop
    {
        private const int damage = 10;
        private bool exploding;
        private RotRectangle originalBoundBox;
        private Timer explosionTimer;
        private const float explosionTime = .13f;
        private const int explosionSize = 750;
        public ExplosionDrop(Vector2 position) : base(position)
        {
            explosionTimer = new Timer(explosionTime);
            originalBoundBox = BoundBox;
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
            BoundBox = new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, explosionSize, explosionSize), 0);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!exploding)
                base.Draw(spriteBatch, gameTime);
        }

        public override void Death()
        {
            exploding = false;
            explosionTimer.Reset();
            BoundBox = originalBoundBox;
            // Particles.GenerateParticles(Position, 8); //Death animation
            Damage = 0;
            base.Death();
        }
    }
}