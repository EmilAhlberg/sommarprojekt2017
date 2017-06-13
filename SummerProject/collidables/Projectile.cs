using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    abstract class Projectile : Collidable
    {
        private float despawnTimer = 7f;
        private const float despawnTime = 7f; //!!!!!!!
        public bool isActive { get; set; }
        public int Damage {get; set; }

        public Projectile(Sprite sprite) : base (Vector2.Zero, sprite)
        {
        }

        public void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
            Move();
        }

        public void Activate(Vector2 source, Vector2 target)
        {
            Position = source;
            float dX = Position.X - target.X;
            float dY = Position.Y - target.Y;
            CalculateAngle(dX, dY);
            isActive = true;
        }

        protected void UpdateTimer(GameTime gameTime)
        {
            if (isActive)
            {
                despawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (despawnTimer < 0)
                    Death();
            }
        }

        public void Death()
        {
            despawnTimer = despawnTime;
            isActive = false;
            Position = new Vector2(-4000, -4000); //!
        }

        public override abstract void Collision(Collidable c2);       

        protected override abstract void Move();
      
    }
}
