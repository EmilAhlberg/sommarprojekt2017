using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;

namespace SummerProject.collidables
{
    public class Player : Entity
    {
        private new float Thrust = EntityConstants.THRUST[EntityConstants.PLAYER];
        public int ControlScheme { get; set; } = 1; // 1-4      
        public bool IsDead { get; private set; }
             
        private Projectiles projectiles;
        private Vector2 startPosition;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles)
            : base(position, sprite)
        {
            Position = position;
            startPosition = position;
            this.projectiles = projectiles;
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            Damage = EntityConstants.DAMAGE[EntityConstants.PLAYER];
            TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.PLAYER];
            Mass = EntityConstants.MASS[EntityConstants.PLAYER];           
        }

        public void Update(GameTime gameTime)
        {
            if (!IsDead)
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
            if (InputHandler.isPressed(MouseButton.LEFT))
            {
                projectiles.Fire(Position, new Vector2(InputHandler.mPosition.X, InputHandler.mPosition.Y));
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
            if (InputHandler.isPressed(Keys.D1))
                ControlScheme = 1;
            if (InputHandler.isPressed(Keys.D2))
                ControlScheme = 2;
            if (InputHandler.isPressed(Keys.D3))
                ControlScheme = 3;
            if (InputHandler.isPressed(Keys.D4))
                ControlScheme = 4;

            base.Thrust = 0;
            if (ControlScheme <= 1)
            {
                if (InputHandler.isPressed(Keys.S))
                    base.Thrust = -Thrust;

                if (InputHandler.isPressed(Keys.W))
                    base.Thrust += Thrust;

                if (InputHandler.isPressed(Keys.A))
                    AddForce(Thrust*(new Vector2((float)Math.Cos(angle-Math.PI/2), (float)Math.Sin(angle-Math.PI/2))));

                if (InputHandler.isPressed(Keys.D))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(angle + Math.PI / 2), (float)Math.Sin(angle + Math.PI / 2))));
            }

            else if (ControlScheme == 2)
            {
                if (InputHandler.isPressed(Keys.S))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(Math.PI/2), (float)Math.Sin(Math.PI/2))));

                if (InputHandler.isPressed(Keys.W))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(-Math.PI / 2), (float)Math.Sin(-Math.PI / 2))));

                if (InputHandler.isPressed(Keys.A))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(Math.PI), (float)Math.Sin(Math.PI))));
               
                if (InputHandler.isPressed(Keys.D))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(0), (float)Math.Sin(0))));
            }

            else if (ControlScheme == 3)
            {
                if (InputHandler.isPressed(MouseButton.RIGHT))
                    base.Thrust = Thrust;

            }
            else if (ControlScheme == 4)
            {
                base.Thrust = 0;
                if (InputHandler.isPressed(Keys.S))
                    base.Thrust = -Thrust;

                if (InputHandler.isPressed(Keys.W))
                    base.Thrust += Thrust;

                if (InputHandler.isPressed(Keys.A))
                    angle -= 0.1f;

                if (InputHandler.isPressed(Keys.D))
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
            sprite.MColor = Color.Transparent;
        }

        public void Reset()
        {
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            Position = startPosition;
            IsDead = false;

        }
    }
}
