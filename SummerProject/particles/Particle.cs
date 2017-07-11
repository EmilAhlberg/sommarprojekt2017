using System;
using Microsoft.Xna.Framework;
using SummerProject.factories;

namespace SummerProject
{
    public class Particle : Movable
    {
        Timer currentTTL;
        float TTL;
        IDs id;
        Vector2 baseScale;
        Color baseColor;
        public bool IsActive { get; set; }
        private float angularVelocity;

        public Particle(Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs id = IDs.DEFAULT) : base(position, id)
        {       
            Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            baseColor = color;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.id = id;
            IsActive = true;
            Behaviour();
            if (id == IDs.AFTERIMAGE)
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public Particle(Sprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs id = IDs.DEFAULT) : base(position, id)
        {
            Sprite = new Sprite(sprite);
            Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            baseColor = color;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.id = id;
            IsActive = true;
            Behaviour();
            if (id == IDs.AFTERIMAGE)
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public void RenewParticle(Sprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs id)
        {
            Sprite = new Sprite(sprite);
            if (Sprite is Sprite)
                Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! If drawables constructor changes, so must this
            Stop();
            Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            baseColor = color;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.id = id;
            IsActive = true;
            Behaviour();
            if (id == IDs.AFTERIMAGE)
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public void RenewParticle(Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs id)
        {
            this.Sprite = SpriteHandler.GetSprite((int)id);
            if (Sprite is Sprite)
                Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! If drawables constructor changes, so must this
            Stop();
            Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2); //! hmmm
            Position = position;
            AddForce(initialForce);
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            baseColor = color;
            this.Sprite.MColor = color;
            baseScale = scale;
            this.Sprite.Scale = baseScale;
            currentTTL = new Timer(TTL);
            this.TTL = TTL;
            this.id = id;
            IsActive = true;
            Behaviour();
           if (id == IDs.AFTERIMAGE) 
                currentTTL.AddTime(-currentTTL.currentTime / 2);
        }

        public void Update(GameTime gameTime)
        {
            Behaviour();
            currentTTL.CountDown(gameTime);
            if (currentTTL.IsFinished || WindowSize.IsOutOfBounds(Position))
            {
                IsActive = false;
            }
        }

        private void Behaviour()
        {
            switch (id)
            {
                case IDs.DEFAULT_PARTICLE:
                case IDs.BOLT:
                case IDs.MONEY:
                case IDs.WRENCH:
                    {
                        Sprite.Scale = 4*baseScale * currentTTL.currentTime / TTL;
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, (float)baseColor.A / 255 * currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                case IDs.DEATH:
                    {
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, (float)baseColor.A / 255 * currentTTL.currentTime / TTL);
                        angle += angularVelocity * currentTTL.currentTime / TTL;

                        break;
                    }
                case IDs.AFTERIMAGE:
                    {
                        Sprite.MColor = new Color((float)Sprite.MColor.R / 255, (float)Sprite.MColor.G / 255, (float)Sprite.MColor.B / 255, (float)baseColor.A / 255 * currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                default: throw new NotImplementedException();
            }
            Move();
        }
    }
}