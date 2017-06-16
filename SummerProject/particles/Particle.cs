using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SummerProject
{
    public class Particle : Movable
    {
        float currentTTL;
        float TTL;
        int ID;
        public bool isActive { get; set; }
        private float angularVelocity;



        public Particle(ISprite sprite, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float scale, float ttl, int ID) : base(position, sprite)
        {
            sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            Velocity = velocity;
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.sprite.MColor = color;
            this.sprite.Scale = scale;
            TTL = ttl;
            currentTTL = TTL;
            this.ID = ID;
            isActive = true;
        }


        public void Update(GameTime gameTime)
        {
            Behaviour(ID);
            currentTTL -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(currentTTL < 0)
            {
                isActive = false;
            }
        }

        private void Behaviour(int ID)
        {
            switch (ID)
            {

                case 1:
                    {
                        sprite.Scale = 6 * currentTTL;
                        sprite.MColor = new Color(currentTTL, currentTTL/3, 0, currentTTL);
                        angle += angularVelocity;
                        break;
                    }
                case 2:
                    {
                        Velocity -= Velocity * ((TTL - currentTTL) * 0.1f); //!!!
                        sprite.MColor = new Color(currentTTL, currentTTL, currentTTL, currentTTL);                     
                        break;
                    }
                case 3:
                    {
                        Velocity -= Velocity * ((TTL - currentTTL) * 0.1f); //!!!
                        sprite.MColor = new Color(currentTTL, currentTTL, currentTTL, currentTTL);
                        break;
                    }

                default: throw new NotImplementedException();
            }
            Move();
        }
    }
}