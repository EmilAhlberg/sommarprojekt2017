using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.collidables;

namespace SummerProject
{
    class Enemy : AIEntity
    {
        private const float speedMultiplier = 5f; //-!
        private const int enemyHealth = 10;
        private const int enemyDamage = 2;
        public int worthScore {get; private set;}
        private Player player;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {           
            this.player = player;
            Velocity = speedMultiplier * Velocity;
            Health = enemyHealth; 
            Damage = enemyDamage;
            worthScore = 100;                        //!!
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))* speedMultiplier;
            Move();
            Particles.GenerateParticles(Position, 4, angle);
            if (Health < 1)
            {
                ScoreHandler.AddScore(worthScore);
                Death();
            }    
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Health = enemyHealth;
        }

        private void CalculateAngle()
        {
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            base.CalculateAngle(dX, dY);
        }   
       
        public override void Collision(Collidable c2)
        {
            if (c2 is Projectile)
            {
                Projectile b = c2 as Projectile;
                if (isActive)
                    Health -= b.Damage;
            }
            if (c2 is Player)
                Death();
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 2, angle); //Death animation
            base.Death();
        }
    }
}
