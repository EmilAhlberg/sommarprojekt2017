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
        private const float startSpeed = 0.5f;
        private Player player;

        public int health { get; set; }
        public bool isActive { get; set;}
        public Enemy(Vector2 position, Sprite sprite, Player player)
            : base(position, sprite)
        {
            Position = position;
            this.player = player;
            Speed = startSpeed;
            health = 10; //!
        }

        public void Update()
        {
            if (health < 1)
                Death();
            else
            {
                CalculateAngle();
                Move();
            }
        }
        public void Death()
        {
            isActive = false;
            Position = new Vector2(-5000, -5000); //!
            health = 10; //!
        }

        private void CalculateAngle()
        {
            float dX = Position.X - player.Position.X;
            float dY = Position.Y - player.Position.Y;
            base.CalculateAngle(dX, dY);
        }

        public override void collision(Collidable c2)
        {
            if(c2 is Bullet)
            {
                Bullet b = c2 as Bullet;
                health -= b.Damage;
            }
        }
    }
}
