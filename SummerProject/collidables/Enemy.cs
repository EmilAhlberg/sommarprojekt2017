using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject
{
    class Enemy : AIEntity
    {        
        public int WorthScore {get; private set;}
        private Player player;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {
            this.player = player;            
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];                  
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Move();
            Particles.GenerateParticles(Position, 4, angle);
            if (Health < 1)
            {
                ScoreHandler.AddScore(WorthScore);
                Death();
            }    
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
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
                if (IsActive)
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
