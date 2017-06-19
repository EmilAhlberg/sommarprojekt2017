using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public class Player : Entity
    {
        private new float Thrust = 12;
        public int controlScheme { get; set; } = 1; // 1-4
        private const float maxSpeed = 10f;
        private float respawnTimer = 3f;
        private const float respawnTime = 3f;
        public bool IsDead { get; private set; }
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
            friction = oldFriction;
            if (ks.IsKeyDown(Keys.D1))
                ControlScheme = 1;
            if (ks.IsKeyDown(Keys.D2))
                ControlScheme = 2;
            if (ks.IsKeyDown(Keys.D3))
                ControlScheme = 3;
            if (ks.IsKeyDown(Keys.D4))
                ControlScheme = 4;

            base.Thrust = 0;
            if (ControlScheme <= 1)
            {
                if (ks.IsKeyDown(Keys.S))
                    base.Thrust = -Thrust;

                if (ks.IsKeyDown(Keys.W))
                    base.Thrust += Thrust;

                if (ks.IsKeyDown(Keys.A))
                    AddForce(Thrust*(new Vector2((float)Math.Cos(angle-Math.PI/2), (float)Math.Sin(angle-Math.PI/2))));

                if (ks.IsKeyDown(Keys.D))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(angle + Math.PI / 2), (float)Math.Sin(angle + Math.PI / 2))));
            }

            else if (ControlScheme == 2)
            {
                if (ks.IsKeyDown(Keys.S))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(Math.PI/2), (float)Math.Sin(Math.PI/2))));

                if (ks.IsKeyDown(Keys.W))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(-Math.PI / 2), (float)Math.Sin(-Math.PI / 2))));

                if (ks.IsKeyDown(Keys.A))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(Math.PI), (float)Math.Sin(Math.PI))));
               
                if (ks.IsKeyDown(Keys.D))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(0), (float)Math.Sin(0))));
            }

            else if (ControlScheme == 3)
            {
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    base.Thrust = Thrust;

            }
            else if (ControlScheme == 4)
            {
                base.Thrust = 0;
                if (ks.IsKeyDown(Keys.S))
                    base.Thrust = -Thrust;

                if (ks.IsKeyDown(Keys.W))
                    base.Thrust += Thrust;

                if (ks.IsKeyDown(Keys.A))
                    angle -= 0.1f;

                if (ks.IsKeyDown(Keys.D))
                    angle += 0.1f;
            }
            base.Move();
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
