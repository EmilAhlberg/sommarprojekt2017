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
        private float angle = 0;
        private Sprite sprite;
        public Player(Vector2 position, Sprite sprite )
        {
            Position = position;
            this.sprite = sprite;
            BoundBox = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), sprite.spriteRect.Width, sprite.spriteRect.Height);
        }

        public void Update()
        {
            //CalculateAngle();
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
            float dX = 0;
            float dY = 0;

            if (ks.IsKeyDown(Keys.Down))
            {
                Position = new Vector2(Position.X - (float)Math.Sin(angle), Position.Y - (float)Math.Cos(angle));
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                Position = new Vector2(Position.X + (float)Math.Sin(angle), Position.Y + (float)Math.Cos(angle));
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X + (float)Math.Sin(angle-Math.PI/2), Position.Y + (float)Math.Sin(angle - Math.PI / 2));
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X - (float)Math.Sin(angle - Math.PI / 2), Position.Y - (float)Math.Sin(angle - Math.PI / 2));
            }
            BoundBox = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), BoundBox.Width, BoundBox.Height);
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
            
    }
}
