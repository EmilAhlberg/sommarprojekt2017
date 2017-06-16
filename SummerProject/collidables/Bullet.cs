using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    class Bullet : Projectile
    {
        private const int bulletDamage = 10;
        private const int bulletHealth = 1;
        public Bullet(Vector2 position, ISprite sprite) : base(position, sprite)
        {

            Damage = bulletDamage;
            Health = bulletHealth;
        }

        public override void Update(GameTime gameTime)
        {
            Particles.GenerateParticles(Position, 6, angle);
            UpdateTimer(gameTime);
            Move();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            
            float dX = source.X - target.X;
            float dY = source.Y - target.Y;
            base.CalculateAngle(dX, dY);
            Speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))*10; //!
            ResetSpawnTime(); 
        }
        public override void Collision(Collidable c2)
        {
            if(c2 is Enemy || c2 is Wall)
            {
                Particles.GenerateParticles(Position, 5);
                Death();
            }
        }
    }
}
