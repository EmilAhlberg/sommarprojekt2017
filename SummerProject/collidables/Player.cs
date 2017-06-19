using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public class Player : Entity
    {
        private Vector2 startPosition;
        private float respawnTimer = 3f;
        private const float respawnTime = 3f;
        public bool IsDead { get; private set; }
        public int ControlScheme { get; set; } = 1; // 1-4
        private const float maxSpeed = 10f;
        private float friction = 0.1f;
        private float oldFriction = 0.1f;
        private KeyboardState prevKeyDown;
        private const float startTurnSpeed = 0.05f * (float)Math.PI;
        private const int playerHealth = 10;
        private const int playerDamage = 2;
        private Projectiles projectiles;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles)
            : base(position, sprite)
        {
            Position = position;
            startPosition = position;
            this.projectiles = projectiles;
            Health = playerHealth;
            Damage = playerDamage;
            TurnSpeed = startTurnSpeed;
        }

        public void Update(GameTime gameTime)
        {
            if (IsDead)
            {
                respawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (respawnTimer < 0)
                    Respawn();
            }
            else
            {
                sprite.MColor = Color.White; //Move to Respawn()
                if (ControlScheme != 4)
                    CalculateAngle();
                Particles.GenerateParticles(Position, 4, angle);
                Move();
                //HandleBulletType();
                Fire();
                if (Health <= 0 && !IsDead)
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
                ControlScheme = 1;
            if (ks.IsKeyDown(Keys.D2))
                ControlScheme = 2;
            if (ks.IsKeyDown(Keys.D3))
                ControlScheme = 3;
            if (ks.IsKeyDown(Keys.D4))
                ControlScheme = 4;

            if (ControlScheme <= 1)
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
            else if (ControlScheme == 2)
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
            else if (ControlScheme == 3)
            {
                friction = 0.03f;
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    Velocity += new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Acceleration;
                    pressed = true;
                }
            }
            else if (ControlScheme == 4)
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
            IsDead = true;
            Particles.GenerateParticles(Position, 3, angle); //Death animation
            respawnTimer = respawnTime;
            sprite.MColor = Color.Transparent;
        }

        private void Respawn()
        {
            Health = playerHealth;
            Position = startPosition;
            IsDead = false;

        }
    }
}
