﻿using System;
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
        private const float startSpeed = 0.5f; //-!
        private const int enemyHealth = 10;
        private const int enemyDamage = 2;

        private Player player;

        public Enemy(Vector2 position, ISprite sprite, Player player)
            : base(position, sprite)
        {           
            this.player = player;
            Speed = startSpeed;
            Health = enemyHealth; ; 
            Damage = enemyDamage;
        }

        public override void Update(GameTime gameTime)
        {
            CalculateAngle();
            Move();
            if (Health < 1)
                Death();           
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
            {
                Death();
            }
        }
    }
}
