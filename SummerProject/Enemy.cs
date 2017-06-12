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
    class Enemy : Collidable
    {
        private float angle = 0;
        private Sprite sprite;
        private Player player;
        private float speed = 0.5f;
        public Enemy(Vector2 position, Sprite sprite, Player player)
        {
            Position = position;
            this.player = player;
            this.sprite = sprite;
            BoundBox = new Rectangle((int)Math.Round(Position.X), (int)Math.Round(Position.Y), sprite.spriteRect.Width, sprite.spriteRect.Height);
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
            if (dX != 0)
            {
                angle = (float)Math.Atan(dY / dX);
            }
            if (dX > 0)
                angle += (float)Math.PI;

            angle = angle % (2 * (float)Math.PI);
        }

        private void Move()
        {
            Position = new Vector2(Position.X + (float)Math.Cos(angle) * speed, Position.Y + (float)Math.Sin(angle) * speed);
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
