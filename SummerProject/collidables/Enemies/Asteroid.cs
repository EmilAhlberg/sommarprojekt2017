﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.Enemies
{
    class Asteroid : Enemy
    {
        private float spriteRotSpeed;
        private const float randomAngleOffsetMultiplier = .3f;

        public Asteroid(Vector2 position, ISprite sprite, Player player) : base(position, sprite, player)
        {
            
        }

        protected override void CalculateAngle()
        {
            Angle += spriteRotSpeed;
        }

        protected override void Enrage()
        {
            Death();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            base.SpecificActivation(source, target);
            Health = 3 * EntityConstants.HEALTH[EntityConstants.ENEMY];
            base.CalculateAngle();
            spriteRotSpeed = 0.05f * SRandom.NextFloat();
            Angle += randomAngleOffsetMultiplier * SRandom.NextFloat();
            friction = 0;
            Thrust = 0;
            AddSpeed(5, Angle);
        }

        public override void Death()
        {
            Particles.GenerateParticles(Position, 18, angle, sprite.MColor); //Death animation
            base.Death();
        }
    }
}