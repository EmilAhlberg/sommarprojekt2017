using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;
using System.Collections.Generic;
using SummerProject.collidables.parts;
using SummerProject.achievements;
using SummerProject.collidables.parts.guns;
using SummerProject.collidables.bullets;

namespace SummerProject.collidables
{
    public class Player : PartController, IPartCarrier
    {
        //private const bool FRICTIONFREEACCELERATION = true;
        public int ControlScheme { get; set; } = 2; // 1-4      
        private const float shieldDischargeRate = 3 / 2f;
        private const float shieldRechargeRate = shieldDischargeRate / 5;
        private Projectiles projectiles;
        public Vector2 StartPosition { get; private set; }

        public Player(Vector2 position, Projectiles projectiles, IDs id = IDs.DEFAULT) : base(position, false, id)
        {
            StartPosition = position;
            this.projectiles = projectiles;
            //AddBoundBox(new RotRectangle(new Rectangle((int)Position.X, (int)Position.Y, shieldSize, shieldSize), angle)); // shield
            Position = position;
        }

        public void Update(GameTime gameTime, bool cutScene) //NEEDS FIX
        {
            if (IsActive)
            {
                Hull.Color = Color.White; //Move to Respawn()
                //if (ControlScheme != 4)
                //    CalculateAngle();
                base.Update(gameTime, cutScene);
                if (!cutScene)
                    Fire();
                //if (Health <= 0 && IsActive)
                //    Death();

                if (Health <= maxHealth / 4)
                {
                    Particles.GenerateParticles(Hull.Sprite.Edges, Position, Hull.Sprite.Origin, 13, Angle);
                }
            }
        }

        private void Fire() //Change when adding gun
        {
            if (InputHandler.isPressed(MouseButton.LEFT))
            {
                Hull.TakeAction(typeof(GunPart));
                Hull.TakeAction(typeof(SprayGunPart));
                Hull.TakeAction(typeof(MineGunPart));
                Hull.TakeAction(typeof(ChargingGunPart));
                Hull.TakeAction(typeof(GravityGunPart));
            }
            if (InputHandler.isPressed(MouseButton.RIGHT))
            {
                if (Energy > 0)
                {
                    if (InputHandler.isJustPressed(MouseButton.RIGHT))
                        SoundHandler.PlaySoundEffect((int)IDs.SLOWMOSOUND);
                    Energy -= shieldDischargeRate;
                    Movable.MOTIONSPEED = 0.5f;
                }
                else
                {
                    if (Movable.MOTIONSPEED != 1)
                        SoundHandler.PlaySoundEffect((int)IDs.REVERSESLOWMOSOUND);
                    Energy = 0;
                    Movable.MOTIONSPEED = 1;
                }
            }
            else
            {
                if (Movable.MOTIONSPEED != 1)
                    SoundHandler.PlaySoundEffect((int)IDs.REVERSESLOWMOSOUND);
                Movable.MOTIONSPEED = 1;
                if (maxEnergy > Energy)
                {
                    Energy += shieldRechargeRate;
                }
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
                    directionVector += new Vector2((float)Math.Cos(Angle - Math.PI / 2), (float)Math.Sin(Angle - Math.PI / 2));

                if (InputHandler.isPressed(Keys.D))
                    directionVector += new Vector2((float)Math.Cos(Angle + Math.PI / 2), (float)Math.Sin(Angle + Math.PI / 2));
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
                Hull.TakeAction(typeof(EnginePart));
            }
        }

        internal void MoveTowardsPoint(Vector2 point)
        {
            float dX = Hull.Position.X - point.X;
            float dY = Hull.Position.Y - point.Y;
            Hull.TurnTowardsVector(dX, dY);

            Vector2 directionVector = Vector2.Zero;
            directionVector += new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            Hull.ThrusterAngle = (float)Math.Atan2(directionVector.Y, directionVector.X);
            Hull.TakeAction(typeof(EnginePart));

        }

        public override void Death() //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            IsActive = false;
            SoundHandler.PlaySoundEffect((int)IDs.PLAYERDEATHSOUND);
            base.Death();
            Hull.Color = Color.Transparent;
        }


        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            Hull.ResetParts();
            Hull.Color = Color.White;
            Hull.Angle = 0;
            Hull.Position = Position;
            Hull.Stop();
        }

        protected override void HandleCollision(ICollidable c2)
        {
            if (c2 is Enemy)
            {
                Enemy e = c2 as Enemy;
                e.Health -= Damage;
            }
            else if (c2 is ChargingBullet)
            {

            }
            else if (c2 is Entity)
            {
                Entity e = c2 as Entity;
                e.Health -= Damage;
            }
        }
    }
}
