using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;
using System.Collections.Generic;
using SummerProject.collidables.parts;

namespace SummerProject.collidables
{
    public class Player : Entity, IPartCarrier
    {
        private new float Thrust = EntityConstants.THRUST[EntityConstants.PLAYER];
        public int ControlScheme { get; set; } = 1; // 1-4      
        public bool IsDead { get; private set; }
        public float Energy { get; set; }
        private const float shieldDischargeRate = 0.1f;
        private const float shieldRechargeRate = shieldDischargeRate / 10;
        private const float maxEnergy = 100;
        private const int shieldSize = 300;
        private bool shieldOn;
        private Projectiles projectiles;
        private Vector2 startPosition;
        protected CompositePart Hull;

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
            Energy = maxEnergy;
            AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle)); // shield
            Hull = new RectangularHull(position, sprite, this);
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
                HandleBulletType();
                Fire();
                if (Health <= 0 && !IsDead)
                    Death();
                if (InputHandler.isPressed(MouseButton.RIGHT))
                {
                    if (Energy > 0)
                    {
                        Particles.GenerateEdgeParticles(sprite.CalculateEdges(), Position, sprite.Origin, 7, angle);
                        Energy -= shieldDischargeRate;
                        shieldOn = true;
                    }
                    else
                        shieldOn = false;
                }
                else
                if(maxEnergy > Energy)
                {
                    Energy += shieldRechargeRate;
                    shieldOn = false;
                }
            }
        }

        private void HandleBulletType()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                projectiles.SwitchBullets(EntityTypes.BULLET);
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
                projectiles.SwitchBullets(EntityTypes.HOMINGBULLET);
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
                    AddForce(Thrust * (new Vector2((float)Math.Cos(angle - Math.PI / 2), (float)Math.Sin(angle - Math.PI / 2))));

                if (InputHandler.isPressed(Keys.D))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(angle + Math.PI / 2), (float)Math.Sin(angle + Math.PI / 2))));
            }

            else if (ControlScheme == 2)
            {
                if (InputHandler.isPressed(Keys.S))
                    AddForce(Thrust * (new Vector2((float)Math.Cos(Math.PI / 2), (float)Math.Sin(Math.PI / 2))));

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
            if (!shieldOn && c2 is Enemy)
            {
                    Enemy e = c2 as Enemy;
                    Health -= e.Damage;
            }
            if(c2 is HealthDrop)
            {
                Health += HealthDrop.heal;
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
            Energy = maxEnergy;
            angle = 0;
            Stop();
            IsDead = false;
        }

        public bool AddPart(Part part, int pos)
        {
            return Hull.AddPart(part, pos);
        }

        public List<Part> GetParts()
        {
            return Hull.GetParts();
        }
    }
}
