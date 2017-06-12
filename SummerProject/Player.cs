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
    class Player
    {
        private Vector2 position;
        private float angle = 0;
        private Sprite sprite;
        public Player(Vector2 position, Sprite sprite )
        {
            this.position = position;
            this.sprite = sprite;
        }

        public void Update()
        {
            CalculateAngle();
            Move();           
        }

        private void CalculateAngle()
        {            
            float dX = position.X - Mouse.GetState().X;
            float dY = position.Y - Mouse.GetState().Y;   
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
                position.Y -= 1.0f * (float)Math.Sin(angle);
                position.X -= 1.0f * (float)Math.Cos(angle);
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y += 1.0f * (float)Math.Sin(angle);
                position.X += 1.0f * (float)Math.Cos(angle);
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                position.Y += 1.0f * (float)Math.Sin(angle-Math.PI/2);
                position.X += 1.0f * (float)Math.Cos(angle-Math.PI/2);
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.Y -= 1.0f * (float)Math.Sin(angle - Math.PI/2);
                position.X -= 1.0f * (float)Math.Cos(angle - Math.PI/2);
            }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
            
    }
}
