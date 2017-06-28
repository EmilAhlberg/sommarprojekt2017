using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;

namespace SummerProject
{
    class Enemy : Entity2, IPartCarrier
    {        
        public int WorthScore {get; private set;}
        private Player player;

        public Enemy(Vector2 position, ISprite sprite, Player player) : base(position, sprite)
        {
            this.player = player;            
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            Hull.Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            Hull.Mass = EntityConstants.MASS[EntityConstants.ENEMY];
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.ENEMY];
        }

        public override void Update(GameTime gameTime) //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            if (Health < 1 && IsDead==false)
                ScoreHandler.AddScore(WorthScore);
            base.Update(gameTime);
            //Particles.GenerateParticles(Position, 4, angle);
        }

        protected void SpecificActivation(Vector2 source, Vector2 target)
        {
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
        }

        protected override void CalculateAngle()
        {
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            Hull.TurnTowardsVector(dX, dY);
        }

        public new void Collision(Collidable c2) //Add support for Bullets, Healthdrops, shield etc
        {
            if (c2 is Part)
            {
                Part p = c2 as Part;
                if (p.Carrier is Part)
                    Collision(p.Carrier as Part);
                else
                {
                    if (p.Carrier is Player)
                        Health -= (p.Carrier as Player).Damage;
                }
                //Projectile b = c2 as Projectile;
                //if (IsActive)
                //    Health -= b.Damage;
            }
            //if (c2 is Player)
            //    Death();
        }

        public override void Death() //NEEDS FIX
        {
            //Particles.GenerateParticles(Position, 2, angle); //Death animation
            base.Death();
        }
    }
}
