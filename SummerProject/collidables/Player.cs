﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;

namespace SummerProject.collidables
{
    class Player : Entity
    {
        public int controlScheme { get; set; } = 1; // 1-4
        private const float maxSpeed = 10f;
        private float friction = 0.1f;
        private float oldFriction = 0.1f;
        private KeyboardState prevKeyDown;
        private const float startTurnSpeed = 0.05f * (float)Math.PI;
        private const int playerHealth = 10;
        private const int playerDamage = 2;
        public int score { get; set; }
        public int highScore { get; set; }

        private Projectiles projectiles;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles)
            : base(position, sprite)
        {
            Position = position;
            this.projectiles = projectiles;
            Health = playerHealth;
            Damage = playerDamage;
            TurnSpeed = startTurnSpeed;
        }

        public void Update(GameTime gameTime)
        {
            if (controlScheme != 4)
                CalculateAngle();
            Particles.GenerateParticles(Position, 4, angle);
            Move();
            HandleBulletType();
            Fire();
            if (Health <= 0)
            {
                Death();
            }
        }

        private void HandleBulletType()
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.D1))
           //     projectiles.switchBullets(EntityTypes.BULLET);
          //  if (Keyboard.GetState().IsKeyDown(Keys.D2))
         //       projectiles.switchBullets(EntityTypes.HOMINGBULLET);
        }

        private void Fire()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                projectiles.Fire(Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            }
        }

        private void CalculateAngle()
        {

            float dX = Position.X - Mouse.GetState().X;
            float dY = Position.Y - Mouse.GetState().Y;
            base.CalculateAngle(dX, dY);

        }

        protected override void Move()
        {
            KeyboardState ks = Keyboard.GetState();
            bool pressed = false;
            friction = oldFriction;
            if (ks.IsKeyDown(Keys.D1))
                controlScheme = 1;
            if (ks.IsKeyDown(Keys.D2))
                controlScheme = 2;
            if (ks.IsKeyDown(Keys.D3))
                controlScheme = 3;
            if (ks.IsKeyDown(Keys.D4))
                controlScheme = 4;

            if (controlScheme <= 1)
            {
                if (ks.IsKeyDown(Keys.S))
                {
                    Velocity += new Vector2((float)Math.Cos(angle + Math.PI), (float)Math.Sin(angle + Math.PI)) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.W))
                {
                    Velocity += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.A))
                {
                    Velocity += new Vector2((float)Math.Cos(angle - Math.PI / 2), (float)Math.Sin(angle - Math.PI / 2)) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.D))
                {
                    Velocity += new Vector2((float)Math.Cos(angle + Math.PI / 2), (float)Math.Sin(angle + Math.PI / 2)) * Acceleration;
                }
            }
            else if (controlScheme == 2)
            {
                if (ks.IsKeyDown(Keys.S))
                {
                    Velocity += new Vector2(0, 1) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.W))
                {
                    Velocity += new Vector2(0, -1) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.A))
                {
                    Velocity += new Vector2(-1, 0) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.D))
                {
                    Velocity += new Vector2(1, 0) * Acceleration;
                }
            }
            else if (controlScheme == 3)
            {
                friction = 0.03f;
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Velocity += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Acceleration;
                    pressed = true;
                }
            }
            else if (controlScheme == 4)
            {
                if (ks.IsKeyDown(Keys.S))
                {
                    Velocity += new Vector2((float)Math.Cos(angle + Math.PI), (float)Math.Sin(angle + Math.PI)) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.W))
                {
                    Velocity += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Acceleration;
                }
                if (ks.IsKeyDown(Keys.A))
                {
                    angle -= 0.1f;
                }
                if (ks.IsKeyDown(Keys.D))
                {
                    angle += 0.1f;
                }
            }
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (prevKeyDown.GetPressedKeys().Contains(k))
                {
                    pressed = true;
                    break;
                }
            }
            if (!pressed)
            {
                Velocity -= Velocity * friction;
            }
            if (Velocity.Length() > maxSpeed)
            {
                Vector2 temp = Velocity;
                temp.Normalize();
                Velocity = temp * maxSpeed;
            }
            base.Move();
            prevKeyDown = ks;
        }

        public override void Collision(Collidable c2)
        {
            if (c2 is Enemy)
            {
                Enemy e = c2 as Enemy;
                Health -= e.Damage;
            }
        }

        public override void Death()
        {
            if (score > highScore)
                highScore = score;
            score = 0;
            Health = playerHealth;
            Particles.GenerateParticles(Position, 3, angle); //Death animation
            Position = Vector2.Zero; //!
        }
    }
}
