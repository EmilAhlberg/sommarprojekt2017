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

              
        public int Damage { get; set; }
        public Bullet(Sprite sprite) : base(Vector2.Zero, sprite)
        {
            this.sprite = sprite;
            Damage = 1; //!   
        }

        public void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
            Move();
        }

        private void UpdateTimer(GameTime gameTime)
        {
            if (isActive)
                despawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (despawnTimer > despawnTime)
            {
                isActive = false;
                despawnTimer = 0;
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
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y + (float)Math.Sin(angle));
        }

        public override void collision(Collidable c2)
        {
            if(c2 is Enemy || c2 is Wall)
            {
                isActive = false;
            }
        }
    }
}
