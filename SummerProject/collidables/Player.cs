using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerProject.factories;
using System.Collections.Generic;
using SummerProject.collidables.parts;

namespace SummerProject.collidables
{
    public class Player : IPartCarrier
    {
        private float Thrust = EntityConstants.THRUST[EntityConstants.PLAYER];
        public int ControlScheme { get; set; } = 1; // 1-4      
        public bool IsDead { get; private set; }
        private Vector2 startPosition;
        public CompositePart Hull;
        private Projectiles projectiles;
        //public float Energy { get; set; }
        //private const float shieldDischargeRate = 0.1f;
        //private const float shieldRechargeRate = shieldDischargeRate / 10;
        //private const float maxEnergy = 100;
        //private const int shieldSize = 300;
        //private bool shieldOn;

        public Player(Vector2 position, ISprite sprite, Projectiles projectiles) 
        {
            startPosition = position;
            this.projectiles = projectiles;
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            Damage = EntityConstants.DAMAGE[EntityConstants.PLAYER];
            //Energy = maxEnergy;
            Hull = new RectangularHull(position, sprite, this);
            Hull.TurnSpeed = EntityConstants.TURNSPEED[EntityConstants.PLAYER];
            Hull.Mass = EntityConstants.MASS[EntityConstants.PLAYER];

        }

        public void Update(GameTime gameTime) //NEEDS FIX
        {
            if (!IsDead)
            {
                Hull.Color = Color.White; //Move to Respawn()
                if (ControlScheme != 4)
                    CalculateAngle();
                //Particles.GenerateParticles(Position, 4, angle);
                Move();
                HandleBulletType();
                Fire();
                if (Health <= 0 && !IsDead)
                    Death();
                //if (InputHandler.isPressed(MouseButton.RIGHT))
                //{
                //    if (Energy > 0)
                //    {
                //        //Particles.GenerateEdgeParticles(sprite.CalculateEdges(), Position, sprite.Origin, 7, angle);
                //        Energy -= shieldDischargeRate;
                //        shieldOn = true;
                //    }
                //    else
                //        shieldOn = false;
                //}
                //else
                //if(maxEnergy > Energy)
                //{
                //    Energy += shieldRechargeRate;
                //    shieldOn = false;
                //}
            }
        }

        private void HandleBulletType() //NEEDS FIX
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                projectiles.SwitchBullets(EntityTypes.BULLET);
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
                projectiles.SwitchBullets(EntityTypes.HOMINGBULLET);
        }

        private void Fire() //NEEDS FIX
        {
            if (InputHandler.isPressed(MouseButton.LEFT))
            {
                projectiles.Fire(Hull.Position, new Vector2(InputHandler.mPosition.X, InputHandler.mPosition.Y));
            }
        }

        private void CalculateAngle()
        {
            float dX = Hull.Position.X - Mouse.GetState().X;
            float dY = Hull.Position.Y - Mouse.GetState().Y;
            Hull.TurnTowardsVector(dX, dY);
        }

        protected void Move() //Change when adding engine
        {
            if (InputHandler.isPressed(Keys.D1))
                ControlScheme = 1;
            if (InputHandler.isPressed(Keys.D2))
                ControlScheme = 2;
            if (InputHandler.isPressed(Keys.D3))
                ControlScheme = 3;
            if (InputHandler.isPressed(Keys.D4))
                ControlScheme = 4;
            Hull.Thrust = 0;
            if (ControlScheme <= 1)
            {
                if (InputHandler.isPressed(Keys.S))
                    Hull.Thrust = -Thrust;

                if (InputHandler.isPressed(Keys.W))
                    Hull.Thrust += Thrust;

                if (InputHandler.isPressed(Keys.A))
                    Hull.AddForce(Thrust, Hull.angle - (float)Math.PI / 2);

                if (InputHandler.isPressed(Keys.D))
                    Hull.AddForce(Thrust, Hull.angle + (float)Math.PI / 2);
            }

            else if (ControlScheme == 2)
            {
                if (InputHandler.isPressed(Keys.S))
                    Hull.AddForce(Thrust, (float)Math.PI / 2);

                if (InputHandler.isPressed(Keys.W))
                    Hull.AddForce(Thrust, -(float)Math.PI / 2);

                if (InputHandler.isPressed(Keys.A))
                    Hull.AddForce(Thrust, (float)Math.PI);

                if (InputHandler.isPressed(Keys.D))
                    Hull.AddForce(Thrust, 0);
            }

            else if (ControlScheme == 3)
            {
                if (InputHandler.isPressed(MouseButton.RIGHT))
                    Hull.Thrust = Thrust;

            }
            else if (ControlScheme == 4)
            {
                Hull.Thrust = 0;
                if (InputHandler.isPressed(Keys.S))
                    Hull.Thrust = -Thrust;

                if (InputHandler.isPressed(Keys.W))
                    Hull.Thrust += Thrust;

                if (InputHandler.isPressed(Keys.A))
                    Hull.angle -= 0.1f;

                if (InputHandler.isPressed(Keys.D))
                    Hull.angle += 0.1f;
            }
            Hull.Move();
        }

        public void Collision(Collidable c2) //NEEDS FIX
        {
            if (/**!shieldOn && */c2 is Enemy)
            {
                    Enemy e = c2 as Enemy;
                    Health -= e.Damage;
            }
            if(c2 is HealthDrop)
            {
                Health += HealthDrop.heal;
            }
        }

        public override void Death() //NEEDS FIX !!!TODO!!! Fix particles for parts
        {
            IsDead = true;
            //Particles.GenerateParticles(Position, 3, angle);
            Hull.Color = Color.Transparent;
        }

        public void Reset() //NEEDS FIX
        {
            Health = EntityConstants.HEALTH[EntityConstants.PLAYER];
            //Energy = maxEnergy;
            Hull.angle = 0;
            Hull.Position = startPosition;
            Hull.Stop();
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
