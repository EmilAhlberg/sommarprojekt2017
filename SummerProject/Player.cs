using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerProject
{
    class Player : Collidable
    {
        private const float startSpeed = 5f;

        private Projectiles projectiles;

        public Player(Vector2 position, Sprite sprite, Projectiles projectiles)
            : base(position, sprite)
        {
            Position = position;
            this.projectiles = projectiles;
            
            Speed = startSpeed;
        }

        public void Update(GameTime gameTime)
        {
            CalculateAngle();
            Move();
            Fire();        
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
                Position = new Vector2(Position.X - (float)Math.Cos(angle) * Speed, Position.Y - (float)Math.Sin(angle) * Speed);
            }
            if (ks.IsKeyDown(Keys.W))
            {
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

        }
    }
}
