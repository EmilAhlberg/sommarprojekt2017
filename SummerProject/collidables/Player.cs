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
        //private const bool FRICTIONFREEACCELERATION = true;
        private new float Thrust { get; } = EntityConstants.THRUST[EntityConstants.PLAYER];
        public int ControlScheme { get; set; } = 2; // 1-4      
        public float Energy { get; set; }
        private const float shieldDischargeRate = 3f;
        private const float shieldRechargeRate = shieldDischargeRate / 10;
        private const float startingEnergy = 100f;
        public float maxEnergy { get; private set; }
        public float maxHealth { get; private set; }
        private const float maxEnergyCap = 300;
        private const float maxHealthCap = 15;
        private const int shieldSize = 300;
        //private bool shieldOn;
        private Projectiles projectiles;
        private Vector2 startPosition;
        private bool toggleGun;
        private bool toggleSprayGun;

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
            Hull.Mass = EntityConstants.MASS[EntityConstants.PLAYER];
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.PLAYER];
            Position = position;
            toggleGun = true;
            toggleSprayGun = true;
        }

        public override void Update(GameTime gameTime) //NEEDS FIX
        {
            if (IsActive)
            {
                Hull.Color = Color.White; //Move to Respawn()
                //if (ControlScheme != 4)
                //    CalculateAngle();
                Move();
                base.Update(gameTime);
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
                HandleBulletToggle();
            }
        }

        private void Fire() //Change when adding gun
        {
            if (InputHandler.isPressed(MouseButton.LEFT))
            {
                if (toggleGun)
                    Hull.TakeAction(typeof(GunPart));
                if (toggleSprayGun)
                    Hull.TakeAction(typeof(SprayGunPart));
                Hull.TakeAction(typeof(MineGunPart));
            }
        }

        protected override void CalculateAngle()
        {
            float dX = Hull.Position.X - InputHandler.mPosition.X;
            float dY = Hull.Position.Y - InputHandler.mPosition.Y;
            Hull.TurnTowardsVector(dX, dY);
        }

        public override void Move() //Change when adding engine
        {
            #region Controls
            Vector2 directionVector = Vector2.Zero;
            if (ControlScheme <= 1)
            {
                if (InputHandler.isPressed(Keys.S))
                    directionVector += new Vector2((float)Math.Cos(-Angle), (float)Math.Sin(-Angle));

                if (InputHandler.isPressed(Keys.W))
                    directionVector += new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

                if (InputHandler.isPressed(Keys.A))
                    directionVector += new Vector2((float)Math.Cos(Angle-Math.PI/2), (float)Math.Sin(Angle-Math.PI/2));

                if (InputHandler.isPressed(Keys.D))
                    directionVector += new Vector2((float)Math.Cos(Angle+Math.PI/2), (float)Math.Sin(Angle+Math.PI/2));
            }

            else if (ControlScheme == 2)
            {
                if (InputHandler.isPressed(Keys.S))
                    directionVector += new Vector2((float)Math.Cos(Math.PI / 2), (float)Math.Sin(Math.PI / 2));

                if (InputHandler.isPressed(Keys.W))
                    directionVector += new Vector2((float)Math.Cos(-Math.PI / 2), (float)Math.Sin(-Math.PI / 2));

                if (InputHandler.isPressed(Keys.A))
                    directionVector += new Vector2((float)Math.Cos(Math.PI), (float)Math.Sin(Math.PI));

                if (InputHandler.isPressed(Keys.D))
                    directionVector += new Vector2((float)Math.Cos(0), (float)Math.Sin(0));
            }

            else if (ControlScheme == 3)
            {
                if (InputHandler.isPressed(MouseButton.LEFT))
                    directionVector += new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

            }
            //else if (ControlScheme == 4)
            //{
            //    if (InputHandler.isPressed(Keys.S))
            //        directionVector += new Vector2((float)Math.Cos(-Angle), (float)Math.Sin(-Angle));

            //    if (InputHandler.isPressed(Keys.W))
            //        directionVector += new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));

            //    if (InputHandler.isPressed(Keys.A))
            //        Angle -= 0.15f;

            //    if (InputHandler.isPressed(Keys.D))
            //        Angle += 0.15f;
            //}
            #endregion
            //if (FRICTIONFREEACCELERATION)
            //{
            //    if (base.Thrust != 0)
            //        friction = 0;
            //}
            //base.Move();
            //friction = EntityConstants.FRICTION[EntityConstants.PLAYER];
            if (directionVector != Vector2.Zero)
            {
                Hull.ThrusterAngle = (float)Math.Atan2(directionVector.Y, directionVector.X);
                base.Move();
            }
        }

        private void HandleBulletToggle()
        {
            if (InputHandler.isJustPressed(Keys.D1))
                toggleGun = !toggleGun;
            if (InputHandler.isJustPressed(Keys.D2))
                toggleSprayGun = !toggleSprayGun;
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

                if (/*!shieldOn && */c2 is Projectile)
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
