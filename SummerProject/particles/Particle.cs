using System;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    public class Particle : Movable
    {
        Timer currentTTL;
        float TTL;
        int ID;
        Vector2 baseScale;
        public bool IsActive { get; set; }
        private float angularVelocity;

        public Particle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, int ID) : base(position, sprite)
        {
            sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.ID = ID;
            IsActive = true;
            Behaviour(ID);
            if (ID == 3)
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public void RenewParticle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, int ID)
        {

            this.Sprite = sprite;
            if (sprite is Sprite)
                sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! If drawables constructor changes, so must this
            Stop();
            sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.ID = ID;
            IsActive = true;
            if (ID == 3)
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public void Update(GameTime gameTime)
        {
            Behaviour(ID);
            currentTTL.CountDown(gameTime);
            if (currentTTL.IsFinished || WindowSize.IsOutOfBounds(Position))
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
                        Sprite.Scale = 4*baseScale * currentTTL.currentTime / TTL;
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                case 2:
                    {
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, currentTTL.currentTime / TTL);
                        angle += angularVelocity * currentTTL.currentTime / TTL;

                        break;
                    }
                case 3:
                    {
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                default: throw new NotImplementedException();
            }
            Move();
        }
    }
}