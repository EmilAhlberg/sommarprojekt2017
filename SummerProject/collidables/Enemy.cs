﻿using System;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.wave;
using SummerProject.achievements;

namespace SummerProject
{
    class Enemy : AIEntity, IPartCarrier
    {
        public int WorthScore { get; protected set; }
        private Player player;
        public static Projectiles projectiles;
        private bool CanShoot { get; set; }
        private bool IsSpeedy { get; set; }
        private bool IsAsteroid { get; set; }
        protected CompositePart Hull;
        private Timer rageTimer;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {
            this.player = player;

            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            rageTimer = new Timer(15);
            //Hull = new RectangularHull(position, sprite);                
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAsteroid)
                CalculateAngle();
            Move();
            rageTimer.CountDown(gameTime);
            if (rageTimer.IsFinished)
            {
                Enrage();
                if (IsAsteroid)
                    Death();
            }
            else
                Particles.GenerateParticles(Position, 4, angle, Color.Green);
            if (CanShoot && SRandom.NextFloat() < Difficulty.ENEMY_FIRE_RISK)
            {
                projectiles.EvilFire(Position, player.Position);
            }

            if (Health < 1)
            {
                ScoreHandler.AddScore(WorthScore);
                Traits.KillTrait.Counter++;
                Death();
            }
        }



        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            rageTimer.Reset();
            Health = EntityConstants.HEALTH[EntityConstants.ENEMY];
            sprite.MColor = Color.White;
            Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.ENEMY];
            DecideType();
            if (IsAsteroid)
            {
                sprite.MColor = Color.DarkGreen;
                CalculateAngle();
            }
            else if (CanShoot)
            {
                sprite.MColor = Color.Red;
            }
            else
             if (IsSpeedy)
            {
                sprite.MColor = Color.Blue;
                Thrust = 2.5f * EntityConstants.THRUST[EntityConstants.ENEMY];
            }
        }

        private void DecideType()
        {
            float rnd = SRandom.NextFloat();
            if (rnd < Difficulty.CAN_SHOOT_RISK) //! chance of being able to shoot
                CanShoot = true;
            else if (rnd < Difficulty.IS_SPEEDY_RISK) //! chance of being shupeedo
                IsSpeedy = true;
            else if (rnd < Difficulty.IS_ASTEROID_RISK)
                IsAsteroid = true;
        }

        private void Enrage()
        {
            Thrust = 5 * EntityConstants.THRUST[EntityConstants.ENEMY];
            Particles.GenerateParticles(Position, 5, angle, Color.Red);
            sprite.MColor = Color.Black;
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
                if (b.IsActive && !b.IsEvil)
                    Health -= b.Damage;
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
            Particles.GenerateParticles(Position, 2, angle, sprite.MColor); //Death animation
            DropSpawnPoints.DeathAt(Position);
            CanShoot = false;
            IsAsteroid = false;
            IsSpeedy = false;
            sprite.Scale = new Vector2(1, 1);
            base.Death();
        }

        public bool AddPart(Part part, int pos)
        {
            throw new NotImplementedException();
        }
    }
}
