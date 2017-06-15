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
        private const float startSpeed = 1f;
        private const float maxSpeed = 10f;
        private const float acceleration = 0.2f;
        private KeyboardState prevKeyDown;
        private const float startTurnSpeed = 0.05f * (float)Math.PI;
        private const int playerHealth = 10;
        private const int playerDamage = 2; 

        private Projectiles projectiles;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles)
            : base(position, sprite) 
        {
            Position = position;
            this.projectiles = projectiles;
            Health = playerHealth;
            Damage = playerDamage;
            Speed = startSpeed;
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
                if (prevKeyDown.IsKeyDown(Keys.W))
                    Speed = startSpeed;
                Position = new Vector2(Position.X - (float)Math.Cos(angle) * Speed, Position.Y - (float)Math.Sin(angle) * Speed);
            }
            if (ks.IsKeyDown(Keys.W))
            {
                if (prevKeyDown.IsKeyDown(Keys.S))
                    Speed = startSpeed;
                Position = new Vector2(Position.X + (float)Math.Cos(angle) * Speed, Position.Y + (float)Math.Sin(angle) * Speed);
            }
            if (ks.IsKeyDown(Keys.A))
            {
                Position = new Vector2(Position.X + (float)Math.Cos(angle - Math.PI / 2) * Speed, Position.Y + (float)Math.Sin(angle - Math.PI / 2) * Speed);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                Position = new Vector2(Position.X - (float)Math.Cos(angle - Math.PI / 2) * Speed, Position.Y - (float)Math.Sin(angle - Math.PI / 2) * Speed);
            }
            bool pressed = false;
                foreach (Keys k in ks.GetPressedKeys())
                {
                    if (prevKeyDown.GetPressedKeys().Contains(k))
                    {
                        pressed = true;
                    if (Speed < maxSpeed)
                        Speed += acceleration;
                        break;
                    }
            }
            if (!pressed)
                Speed = startSpeed;
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
