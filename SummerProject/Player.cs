﻿using System;
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
        private float angle = 0;
        private Sprite sprite;
        private int reloadTime = 1000;
        private const float speed = 5f;
        public Player(Vector2 position, Sprite sprite )
            : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            Position = position;
            this.sprite = sprite;
            sprite.origin = new Vector2(sprite.spriteRect.Width / 2, sprite.spriteRect.Height / 2);
        }

        public void Update()
        {
            CalculateAngle();
            Move();           
        }

        private void CalculateAngle()
        {            
            float dX = Position.X - Mouse.GetState().X;
            float dY = Position.Y - Mouse.GetState().Y;   
            if (dX != 0)
            {
                angle = (float)Math.Atan(dY / dX);
            }               
            if (dX > 0)
                angle += (float)Math.PI;
            
            angle = angle % (2*(float)Math.PI);
        }

        private void Move()
        {            
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.S))
            {
                Position = new Vector2(Position.X - (float)Math.Cos(angle) * speed, Position.Y - (float)Math.Sin(angle) * speed);
            }
            if (ks.IsKeyDown(Keys.W))
            {
                Position = new Vector2(Position.X + (float)Math.Cos(angle) * speed, Position.Y + (float)Math.Sin(angle) * speed);
            }
            if (ks.IsKeyDown(Keys.A))
            {
                Position = new Vector2(Position.X + (float)Math.Cos(angle - Math.PI / 2) * speed, Position.Y + (float)Math.Sin(angle - Math.PI / 2) * speed);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                Position = new Vector2(Position.X - (float)Math.Cos(angle - Math.PI / 2) * speed, Position.Y - (float)Math.Sin(angle - Math.PI / 2) * speed);
            }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
            
    }
}
