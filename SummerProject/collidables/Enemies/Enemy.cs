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
        public float WorthScore {get; private set;}

        private Player player;
        private Timer rageTimer;

        public Enemy(Vector2 position, Player player, IDs id = IDs.DEFAULT)
            : base(position, id)
        {
            this.player = player;
            rageTimer = new Timer(15); //!!    
            //Damage = EntityConstants.DAMAGE[(int)IDs.DEFAULT_ENEMY];
            //WorthScore = EntityConstants.SCORE[(int)IDs.DEFAULT_ENEMY]; 
        }

        public override void SetStats(IDs id)
        {
            base.SetStats(id);
            WorthScore = EntityConstants.GetStatsFromID(EntityConstants.SCORE, id);
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            ThrusterAngle = Angle;
            //Hull.TakeAction(typeof(EnginePart));
            Move();
            Hull.TakeAction(typeof(ChargingGunPart));
            Hull.TakeAction(typeof(SprayGunPart));
            Hull.TakeAction(typeof(MineGunPart));
            //AddForce(10, Angle); //!
            Hull.Update(gameTime);
            if (Health <= 0 && IsActive)
            {
                Traits.KILLS.Counter++; //maybe not counted as a kill
                Death();
            }
            if (Health > 0 && IsActive)
                rageTimer.CountDown(gameTime);
            if (rageTimer.IsFinished)
            {
                //Enrage();
            }
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            rageTimer.Reset();
            Hull.Color = Color.White;
        }

        protected virtual void Enrage()
        {
            Particles.GenerateParticles(Position, 5, Angle, Color.Red);
            Hull.Color = Color.Black;
        }

        protected override void CalculateAngle()
        {
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            base.CalculateAngle(dX, dY);
        }

        public override void Collision(ICollidable c2)
        {
            if (c2 is Projectile)
            {
                Projectile b = c2 as Projectile;
                if (b.IsActive && !b.IsEvil)
                {
                    Health -= b.Damage;
                    AddForce(b.Velocity); //! remove lator
                    Traits.SHOTSHIT.Counter++;
                    ScoreHandler.AddScore((int)WorthScore);
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
                Death();
            }
              
        }

        public override void Death()
        { 
            DropSpawnPoints.DeathAt(Position);
            base.Death();
        }
    }
}
