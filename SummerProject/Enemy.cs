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
    class Enemy : Drawable
    {
        private Sprite sprite;
        private Player player;
        private float speed = 0.5f;
        public Enemy(Vector2 position, Sprite sprite, Player player)
            : base(position, sprite)
        {
            Position = position;
            this.player = player;
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
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            base.CalculateAngle(dX, dY);
        }

        private void Move()
        {
            Position = new Vector2(Position.X + (float)Math.Cos(angle) * speed, Position.Y + (float)Math.Sin(angle) * speed);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
 
    }
}
