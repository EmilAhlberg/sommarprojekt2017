﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using SummerProject.factories;
using SummerProject.wave;
using SummerProject.achievements;

namespace SummerProject
{
    class Enemy : PartController, IPartCarrier
    {        
        public int WorthScore {get; private set;}

        private Player player;
        public static Projectiles projectiles;
        private bool CanShoot { get; set; }
        private bool IsSpeedy { get; set; }
        private bool IsAsteroid { get; set; }
        private float spriteRotSpeed;
        private const float randomAngleOffsetMultiplier = .3f;
        protected CompositePart Hull;
        private Timer rageTimer;
        private Timer reloadTimer;


        public Enemy(Vector2 position, ISprite sprite, Player player, int type)
            : base(position, sprite)
        {
            this.player = player;
            switch (type)
            {
                case 151: CanShoot = true; break;
                case 152: IsSpeedy = true; break;
                case 153: IsAsteroid = true; break;
            }
            Damage = EntityConstants.DAMAGE[EntityConstants.ENEMY];
            WorthScore = EntityConstants.SCORE[EntityConstants.ENEMY];
            Hull.Thrust = EntityConstants.THRUST[EntityConstants.ENEMY];
            Hull.Mass = EntityConstants.MASS[EntityConstants.ENEMY];
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.ENEMY];
            Hull.friction = EntityConstants.FRICTION[EntityConstants.DEFAULT];
        }

        public override void Update(GameTime gameTime) //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            if (Health < 1 && IsDead==false)
            rageTimer.CountDown(gameTime);
            if (rageTimer.IsFinished)
            {
                Enrage();
                if (IsAsteroid)
                    Death();
            }
            if (!IsAsteroid)
            {
                CalculateAngle();
                Particles.GenerateParticles(Position, 4, angle, Color.Green);
            }
            else
                sprite.Rotation += spriteRotSpeed;
            if (CanShoot)
            {
                reloadTimer.CountDown(gameTime);
                if (reloadTimer.IsFinished)
                {
                    projectiles.EvilFire(Position, player.Position);
                    reloadTimer.Reset();
                }
            }
            Move();
            if (Health < 1)
            {
                ScoreHandler.AddScore(WorthScore);
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
            reloadTimer = new Timer(Difficulty.ENEMY_FIRE_RATE);
            if (IsAsteroid)
            {
                CalculateAngle();
                Health *= 3;
                spriteRotSpeed = 0.05f * SRandom.NextFloat();
                angle += randomAngleOffsetMultiplier * SRandom.NextFloat();
                friction = 0;
                Thrust = 0;
                AddSpeed(5, angle);
            }
            else
             if (IsSpeedy)
            {
                Thrust = 2.5f * EntityConstants.THRUST[EntityConstants.ENEMY];
            }
        }

        private void Enrage()
        {
            Thrust = 5 * EntityConstants.THRUST[EntityConstants.ENEMY];
            Particles.GenerateParticles(Position, 5, angle, Color.Red);
            sprite.MColor = Color.Black;
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
                    Traits.ShotsHitTrait.Counter++;
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
            if(CanShoot)
                Particles.GenerateParticles(Position, 16, angle, sprite.MColor); //Death animation
            else if (IsSpeedy)
                Particles.GenerateParticles(Position, 17, angle, sprite.MColor); //Death animation
            else if (IsAsteroid)
                Particles.GenerateParticles(Position, 18, angle, sprite.MColor); //Death animation
            else
                Particles.GenerateParticles(Position, 2, angle, sprite.MColor); //Death animation
            DropSpawnPoints.DeathAt(Position);
            reloadTimer.Reset();
            base.Death();
        }
    }
}
