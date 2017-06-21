using System;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public class Particle : Movable
    {
        Timer currentTTL;
        float TTL;
        int ID;
        float baseScale;
        public bool IsActive { get; set; }
        private float angularVelocity;

        public Particle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, float scale, float TTL, int ID) : base(position, sprite)
        {
            sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.sprite.MColor = color;
            baseScale = scale;
            this.sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.ID = ID;
            IsActive = true;
            Thrust = 0;
        }

        public void RenewParticle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, float scale, float TTL, int ID)
        {

            this.sprite = sprite;
            if (sprite is Sprite)
                sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! If drawables constructor changes, so must this

            sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.sprite.MColor = color;
            baseScale = scale;
            this.sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.ID = ID;
            IsActive = true;
            Thrust = 0;
        }

        public void Update(GameTime gameTime)
        {
            Behaviour(ID);
            currentTTL.CountDown(gameTime);
            if (currentTTL.IsFinished)
            {
                IsActive = false;
            }
        }

        private void Behaviour(int ID)
        {
            switch (ID)
            {
                case 1:
                    {
                        sprite.Scale = 4 * baseScale * currentTTL.currentTime / TTL;
                        sprite.MColor = new Color((float)sprite.MColor.R / 255, (float)sprite.MColor.G / 255, (float)sprite.MColor.B / 255, currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                case 2:
                    {
                        sprite.MColor = new Color(1, 1, 1, (currentTTL.currentTime / TTL));
                        break;
                    }
                default: throw new NotImplementedException();
            }
            Move();
        }
    }
}