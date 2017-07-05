using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.wave;
using SummerProject.achievements;

namespace SummerProject
{
    abstract class Enemy : PartController, IPartCarrier
    {        
        public int WorthScore {get; private set;}
        private Player player;
        private Timer rageTimer;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {
            this.player = player;
            rageTimer = new Timer(15); //!!    
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            Hull.Mass = EntityConstants.MASS[EntityConstants.ENEMY];
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.ENEMY];
            Hull.friction = EntityConstants.FRICTION[EntityConstants.DEFAULT];
        }

        public override void Update(GameTime gameTime) //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            CalculateAngle();
            ThrusterAngle = Angle;
            Move();
            AddForce(10, Angle);
            Hull.Update(gameTime);
            if (Health <= 0 && IsActive)
                Death();
            if (Health > 1 && IsActive)
                rageTimer.CountDown(gameTime);
            if (rageTimer.IsFinished)
            {
                Enrage();
            }
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            rageTimer.Reset();
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
            Sprite.MColor = Color.White;
            TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.ENEMY];
        }

        protected virtual void Enrage()
        {
            //Thrust = 5 * EntityConstants.THRUST[EntityConstants.ENEMY];
            Particles.GenerateParticles(Position, 5, Angle, Color.Red);
            Sprite.MColor = Color.Black;
        }

        protected override void CalculateAngle()
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
                if (b.IsActive && !b.IsEvil)
                {
                    Health -= b.Damage;
                    AddForce(b.Velocity); //! remove lator
                    Traits.SHOTSHIT.Counter++;
                }
                
            }
            if (c2 is ExplosionDrop)
            {
                ExplosionDrop ed = c2 as ExplosionDrop;
                if (ed.IsActive)
                    Health -= ed.Damage;
            }
            if (c2 is Player)
            {
                //Traits.KillTrait.Counter++; //maybe not counted as a kill
                Death();
            }
              
        }

        public override void Death()
        { 
            DropSpawnPoints.DeathAt(Position);
            ScoreHandler.AddScore(WorthScore);
            base.Death();
        }
    }
}
