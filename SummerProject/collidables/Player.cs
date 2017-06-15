using System;
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
        private const float maxSpeed = 10f;
        private const float acceleration = 1f;
        private const float friction = 0.1f;
        private KeyboardState prevKeyDown;
        private const float startTurnSpeed = 0.05f * (float)Math.PI;
        private const int playerHealth = 10;
        private const int playerDamage = 2; 
        public int score { get; set; }

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
            CalculateAngle();
            Move();
            HandleBulletType();
            Fire();     
            if(Health <= 0)
            {
                Death();
            }     
        }

        private void HandleBulletType()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                projectiles.switchBullets(EntityTypes.BULLET);
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
                projectiles.switchBullets(EntityTypes.HOMINGBULLET);
        }

        private void Fire()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                projectiles.Fire(Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
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
            if (ks.IsKeyDown(Keys.S))
            {
                Speed += new Vector2((float)Math.Cos(angle + Math.PI), (float)Math.Sin(angle + Math.PI)) * acceleration;
            }
            if (ks.IsKeyDown(Keys.W))
            {
                Speed += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * acceleration;
            }
            if (ks.IsKeyDown(Keys.A))
            {
                Speed += new Vector2((float)Math.Cos(angle - Math.PI/2), (float)Math.Sin(angle - Math.PI/2)) * acceleration;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                Speed += new Vector2((float)Math.Cos(angle + Math.PI/2), (float)Math.Sin(angle + Math.PI/2)) * acceleration;
            }
            bool pressed = false;
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
                Speed -= Speed * friction;
            }
            
            if (Speed.Length() > maxSpeed)
            {
                Vector2 temp = Speed;
                temp.Normalize();
                Speed = temp * maxSpeed;
            }


            base.Move();
            prevKeyDown = ks;

            //if (ks.IsKeyDown(Keys.A))
            //{
            //    angle = angle - 0.1f;
            //}
            //if (ks.IsKeyDown(Keys.D))
            //{
            //    angle = angle + 0.1f;
            //}
            //if (ks.IsKeyDown(Keys.S))
            //{
            //    Speed = -Speed;
            //    base.Move();
            //    Speed = -Speed;
            //}
            //if (ks.IsKeyDown(Keys.W))
            //{
            //    base.Move();
            //}
        }

        public override void Collision(Collidable c2)
        {
            if(c2 is Enemy)
            {
                Enemy e = c2 as Enemy;
                Health -= e.Damage;
            }
        }

        public override void Death()
        {
            Health = playerHealth;
            Position = Vector2.Zero; //!
        }
    }
}
