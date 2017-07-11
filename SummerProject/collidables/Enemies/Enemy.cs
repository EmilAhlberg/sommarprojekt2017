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
    public abstract class Enemy : PartController, IPartCarrier
    {
        public float WorthScore { get; private set; }
        private Player player;
        private Timer rageTimer;

        public Enemy(Vector2 position, Player player, IDs id = IDs.DEFAULT)
            : base(position, id)
        {
            this.player = player;
            rageTimer = new Timer(15); //!!    
        }

        public override void SetStats(IDs id)
        {
            base.SetStats(id);
            WorthScore = EntityConstants.GetStatsFromID(EntityConstants.SCORE, id);
        }

        protected virtual void AI(GameTime gameTime)
        {
            CalculateAngle();
            ThrusterAngle = Angle;
        }

        public override void Update(GameTime gameTime)
        {
            AI(gameTime);
            //AddForce(10, Angle); //!
            Hull.Update(gameTime);
            if (Health <= 0 && IsActive)
            {
                ScoreHandler.AddScore((int)WorthScore);
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

        protected override void HandleCollision(ICollidable c2)
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
            if (c2 is Player)
                Death();
        }


        public override void Death()
        {
            DropSpawnPoints.DeathAt(Position);
            base.Death();
        }
    }
}
