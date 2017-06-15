using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    public class Particle : Movable
    {
        float TTL;
        int ID;
        public bool isActive { get; set; }
        public Particle(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            Position = position;
            this.sprite = sprite;

        }

        public void Update(GameTime gameTime)
        {
            Behaviour(ID);
            TTL -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(TTL < 0)
            {
                isActive = false;
            }
        }

        public void Activate(Vector2 position, float angle, int ID)
        {
            Position = position;
            this.angle = angle;
            this.ID = ID;
            TTL = 3; //!
            isActive = true;
        } 

        private void Behaviour(int ID)
        {
            switch (ID)
            {

                case 1:
                    {
                        Speed = 10;
                        sprite.Scale = TTL/2;
                        angle += 0.1f;
                        Move();
                        break;
                    }

                default: throw new NotImplementedException();
            }
        }
    }
}