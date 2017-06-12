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
        private Sprite sprite;
        private float angle;
        public Boolean isActive {get; set;}
        public Bullet( Sprite sprite)
        {
            this.sprite = sprite;           
        }

        public void Update()
        {
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y +(float) Math.Sin(angle));            
        }

        public void Activate(Vector2 source, Vector2 target)
        {
            Position = source;
            calculateAngle(target);
            isActive = true;
        }
        //
        // CalculateAngle duplicated in Player.
        //
        private void calculateAngle(Vector2 target)
        {
            float dX = Position.X - target.X;
            float dY = Position.Y - target.Y;
            if (dX != 0)
            {
                angle = (float)Math.Atan(dY / dX);
            }
            if (dX > 0)
                angle += (float)Math.PI;

            angle = angle % (2 * (float)Math.PI);
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = Position;            
            sprite.Draw(spriteBatch);
        }

    }
}
