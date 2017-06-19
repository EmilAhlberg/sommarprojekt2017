﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SummerProject
{
    public class Particle : Movable
    {
        Timer currentTTL;
        float TTL;
        int ID;
        float baseScale;
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
            baseScale = scale;
            this.sprite.Scale = baseScale;
            TTL = ttl;
            currentTTL = new Timer(TTL);
            this.ID = ID;
            isActive = true;
        }


        public void Update(GameTime gameTime)
        {
            Behaviour(ID);
            currentTTL.CountDown(gameTime);
            if(currentTTL.IsFinished)
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
                        sprite.Scale = 4 * baseScale * currentTTL.currentTime /TTL;
                        Velocity -= Velocity * ((TTL - currentTTL.currentTime) * 0.1f); //!!!
                        sprite.MColor = new Color((float)sprite.MColor.R / 255, (float)sprite.MColor.G / 255, (float)sprite.MColor.B / 255, currentTTL.currentTime / TTL);
                        angle += angularVelocity;
                        break;
                    }
                case 2:
                    {
                        Velocity -= Velocity * ((TTL - currentTTL.currentTime) * 0.1f); //!!!
                        sprite.MColor = new Color(1, 1, 1, (currentTTL.currentTime / TTL));                     
                        break;
                    }

                default: throw new NotImplementedException();
            }
            Move();
        }
    }
}