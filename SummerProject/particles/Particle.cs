using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SummerProject
{
    public class Particle : Movable
    {
        float TTL;
        int ID;
        public bool isActive { get; set; }
        List<ISprite> sprites;
        public Particle(Vector2 position, ISprite sprite) : base(position, sprite)
        {
            Position = position;
            this.sprite = sprite;
            sprites = new List<ISprite>();
            sprites.Add(sprite);
        }

        public void AddSprite(Sprite s)
        {
            s.Origin = new Vector2(s.SpriteRect.Width / 2, s.SpriteRect.Height / 2); //! hmmm
            sprites.Add(s);
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

        public void Activate(Vector2 position, float angle, float speed, int ttl, int ID)
        {
            Position = position;
            Speed = speed;
            this.angle = angle;
            
            this.ID = ID;
            TTL = ttl; //!
            isActive = true;
        } 

        private void Behaviour(int ID)
        {
            switch (ID)
            {

                case 1:
                    {
                        sprite = sprites[0];
                        Speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 10; //!!!
                        sprite.Scale = 10*TTL;
                        angle += 0.1f;
                        Move();
                        break;
                    }
                case 2:
                    {
                        sprite = sprites[1];
                        Speed = Speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 3; //!!!

                        Move();
                        break;
                    }
                case 3:
                    {
                        sprite = sprites[2];
                        Speed = Speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * -3; //!!!
                        Move();
                        break;
                    }

                default: throw new NotImplementedException();
            }
        }
    }
}