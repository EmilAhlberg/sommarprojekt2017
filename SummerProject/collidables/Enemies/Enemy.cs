using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.wave;
using SummerProject.achievements;
using SummerProject.collidables.Enemies;
using SummerProject.collidables.bullets;

namespace SummerProject
{
    public abstract class Enemy : PartController, IPartCarrier
    {
        public float WorthScore { get; private set; }
        protected PartController player;
        private Timer rageTimer;

        public Enemy(Vector2 position, PartController player, IDs id = IDs.DEFAULT)
            : base(position, true, id)
        {
            this.player = player;
            rageTimer = new Timer(15); //!!    
        }

        public override void SetStats(IDs id)
        {
            base.SetStats(id);
            WorthScore = EntityConstants.GetStatsFromID(EntityConstants.SCORE, id);
            friction = EntityConstants.GetStatsFromID(EntityConstants.FRICTION, id) *  Difficulty.ENEMY_FRICTIONFACTOR;
        }

        public override void Update(GameTime gameTime)
        {
            if (Health <= 0 && IsActive)
            {
                //WARNING
                //this block is shit and never executes.... i think
                ScoreHandler.AddScore((int)WorthScore);
                if (!(this is Asteroid))
                    Traits.KILLS.Counter++; //maybe not counted as a kill
            }
            base.Update(gameTime);
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
            CalculateAngle();
            Particles.GenerateParticles(Position, 1, Angle);
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
            if (c2 is Player)
            {
                Player p = (c2 as Player);
                if (IsEvil)
                {
                    p.Health -= Damage;
                    Death();
                }
            }
            else if (c2 is ChargingBullet)
            {
                    }
            else if (c2 is Entity)
            {
                Entity e = c2 as Entity;
                e.Health -= Damage;
            }
            if (Health <= 0)//REPLACE THIS WITH GOOD COLISSIONHANDLING
            { 
                Death();
                ScoreHandler.AddScore((int)WorthScore);
                if (!(this is Asteroid))
                    Traits.KILLS.Counter++; //maybe not counted as a kill
            }
        }

        public override void Death()
        {
            DropSpawnPoints.DeathAt(Position);
            SoundHandler.PlaySoundEffect((int)IDs.EXPLOSIONDEATHSOUND);
            base.Death();
        }

        protected void OutOfBoundsDeath() // currently to handle asteroid death out of bounds
        {
            base.Death();
        }
    }
}
