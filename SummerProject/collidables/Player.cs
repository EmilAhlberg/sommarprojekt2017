using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;
using System.Collections.Generic;
using SummerProject.collidables.parts;
using SummerProject.achievements;

namespace SummerProject.collidables
{
    public class Player : PartController, IPartCarrier
    {
        private const bool FRICTIONFREEACCELERATION = true;
        private new float Thrust { get; } = EntityConstants.THRUST[EntityConstants.PLAYER];
        public int ControlScheme { get; set; } = 2; // 1-4      
        public float Energy { get; set; }
        private const float shieldDischargeRate = 3f;
        private const float shieldRechargeRate = shieldDischargeRate / 10;
        private const float startingEnergy = 100f;
        public float maxEnergy { get; private set; }
        public int maxHealth { get; private set; }
        private const float maxEnergyCap = 300;
        private const int maxHealthCap = 15;
        private const int shieldSize = 300;
        private bool shieldOn;
        private Projectiles projectiles;
        private Vector2 startPosition;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles) : base(position, sprite)
        {
            startPosition = position;
            Energy = startingEnergy;
            this.projectiles = projectiles;
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            Damage = EntityConstants.DAMAGE[EntityConstants.PLAYER];
            TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.PLAYER];
            Mass = EntityConstants.MASS[EntityConstants.PLAYER];
            friction = EntityConstants.FRICTION[EntityConstants.PLAYER];
            maxHealth = Health;
            maxEnergy = Energy;
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle)); // shield
            Hull.Thrust = EntityConstants.THRUST[EntityConstants.PLAYER];
            Hull.Mass = EntityConstants.MASS[EntityConstants.PLAYER];
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.PLAYER];
            Position = position;
        }

        public override void Update(GameTime gameTime) //NEEDS FIX
        {
            if (IsActive)
            {
                Hull.Color = Color.White; //Move to Respawn()
                //if (ControlScheme != 4)
                //    CalculateAngle();
                base.Update(gameTime);
                Move();
                HandleBulletType();
                Fire();
                //if (Health <= 0 && IsActive)
                //    Death();
                //if (InputHandler.isPressed(MouseButton.RIGHT))
                //{
                //    if (Energy > 0)
                //    {
                //        Particles.GenerateParticles(sprite.Edges, Position, sprite.Origin, 7, angle);
                //        Energy -= shieldDischargeRate;
                //        shieldOn = true;
                //    }
                //    else
                //    {
                //        Energy = 0;
                //        shieldOn = false;
                //    }
                //}
                //else
                //{
                //    shieldOn = false;
                //    if (maxEnergy > Energy)
                //    {
                //        Energy += shieldRechargeRate;
                //    }
                //}
                if (Health <= 2)
                {
                    Particles.GenerateParticles(Sprite.Edges, Position, Sprite.Origin, 13, Angle);
                }
            }
        }

        private void HandleBulletType() //Change when adding gun
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                projectiles.SwitchBullets(EntityTypes.BULLET);
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
                projectiles.SwitchBullets(EntityTypes.HOMINGBULLET);
        }

        private void Fire() //Change when adding gun
        {
            if (InputHandler.isPressed(MouseButton.LEFT))
            {
                Hull.TakeAction(typeof(GunPart));
            }
        }

        protected override void CalculateAngle()
        {
            float dX = Hull.Position.X - Mouse.GetState().X;
            float dY = Hull.Position.Y - Mouse.GetState().Y;
            Hull.TurnTowardsVector(dX, dY);
        }

        public override void Move() //Change when adding engine
        {
            //if (InputHandler.isPressed(Keys.D1))
            //    ControlScheme = 1;
            //if (InputHandler.isPressed(Keys.D2))
            //    ControlScheme = 2;
            //if (InputHandler.isPressed(Keys.D3))
            //    ControlScheme = 3;
            //if (InputHandler.isPressed(Keys.D4))
            //    ControlScheme = 4;
            base.Thrust = 0;
            #region Controls
            if (ControlScheme <= 1)
            {
                if (InputHandler.isPressed(Keys.S))
                    base.Thrust = -Thrust;

                if (InputHandler.isPressed(Keys.W))
                    base.Thrust += Thrust;

                if (InputHandler.isPressed(Keys.A))
                    AddForce(Thrust, Angle - (float)Math.PI / 2);

                if (InputHandler.isPressed(Keys.D))
                    AddForce(Thrust, Angle + (float)Math.PI / 2);
            }

            else if (ControlScheme == 2)
            {
                if (InputHandler.isPressed(Keys.S))
                    AddForce(Thrust, (float)Math.PI / 2);

                if (InputHandler.isPressed(Keys.W))
                    Hull.TakeAction(typeof(EnginePart));

                if (InputHandler.isPressed(Keys.A))
                    AddForce(Thrust, (float)Math.PI);

                if (InputHandler.isPressed(Keys.D))
                    AddForce(Thrust, 0);
            }

            else if (ControlScheme == 3)
            {
                if (InputHandler.isPressed(MouseButton.RIGHT))
                    base.Thrust = Thrust;

            }
            //else if (ControlScheme == 4)
            //{
            //    base.Thrust = 0;
            //    if (InputHandler.isPressed(Keys.S))
            //        base.Thrust = -Thrust;

            //    if (InputHandler.isPressed(Keys.W))
            //        base.Thrust += Thrust;

            //    if (InputHandler.isPressed(Keys.A))
            //        Angle -= 0.1f;

            //    if (InputHandler.isPressed(Keys.D))
            //        Angle += 0.1f;
            //}
            #endregion
            if (FRICTIONFREEACCELERATION)
            {
                if (base.Thrust != 0)
                    friction = 0;
            }
            base.Move();
            friction = EntityConstants.FRICTION[EntityConstants.PLAYER];
        }

        public override void Collision(Collidable c2)
        {

            if (/*!shieldOn && */c2 is Enemy)
            {
                Enemy e = c2 as Enemy;
                Health -= e.Damage;
            }

                if (c2 is HealthDrop)
                {
                    if (Health == maxHealth && maxHealth < maxHealthCap)
                        maxHealth++;
                    Health += ((HealthDrop)c2).Heal;
                    if (Health > maxHealthCap)
                        Health = maxHealthCap;
                    if (Health > maxHealth)
                        maxHealth = Health;
                }
                if (c2 is EnergyDrop)
                {
                    if (maxEnergy < maxEnergyCap)
                        maxEnergy += EnergyDrop.charge;
                    Energy = maxEnergy;
                }

                if (!shieldOn && c2 is Projectile)
                {
                    Projectile b = c2 as Projectile;
                    if (b.IsEvil)
                        Health -= b.Damage;
                }
                //if (c2 is HealthDrop)
                //{
                //    Health += HealthDrop.heal;
                //}

        }

        public override void Death() //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            IsActive = false;
            base.Death();
            Hull.Color = Color.Transparent;
        }

        public void Reset() //NEEDS FIX
        {
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            maxEnergy = startingEnergy;
            maxHealth = Health;
            Energy = maxEnergy;
            Hull.Color = Color.White;
            IsActive = true;
            Hull.Angle = 0;
            Hull.Position = startPosition;
            Hull.Stop();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            throw new NotImplementedException();
        }
    }
}
