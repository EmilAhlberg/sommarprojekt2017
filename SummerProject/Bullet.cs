using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Bullet : Collidable
    {        
        //private Sprite sprite;
        private float despawnTimer = 0f;
        private const float despawnTime = 7f;
        public bool isActive {get; set;}       

        public Bullet(Sprite sprite) : base(Vector2.Zero, sprite)
        {
            this.sprite = sprite;        
        }

        public void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y + (float)Math.Sin(angle));
        }

        private void UpdateTimer(GameTime gameTime)
        {
            despawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (despawnTimer > despawnTime)
            {
                isActive = false;
            }
        }

        public void Activate(Vector2 source, Vector2 target)
        {
            Position = source;
            float dX = Position.X - target.X;
            float dY = Position.Y - target.Y;
            CalculateAngle(dX, dY);
            isActive = true;
        }

        protected override void Move()
        {
            
        }
    }
}
