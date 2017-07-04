﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public static class Particles
    {
        static List<Particle> particles;
        static List<Sprite> spriteList;
        private const int maxParticles = 1000;

        static Particles()
        {
            particles = new List<Particle>();
            spriteList = new List<Sprite>();
            spriteList.Add(new Sprite());
        }

        public static void AddSprite(Sprite s)
        {
            spriteList.Add(new Sprite(s));
        }

        public static void Reset()
        {
            foreach (Particle p in particles)
            {
                if (p.IsActive)
                {
                    p.IsActive = false;
                }
            }      
        }

        public static void Update(GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                if (p.IsActive)
                { 
                    p.Update(gameTime);
                    if (WindowSize.IsOutOfBounds(p.Position))
                    {
                        p.IsActive = false;
                    }   
                }
            }
        }

        public static void GenerateParticles(List<Vector2> edges, Vector2 position, Vector2 origin, int ID, float angle = 0, Color? color = null)
        { 
            switch (ID)
            {
                #region Shield Visuals 7
                case 7:
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 currentEdge = edges[(int)(SRandom.NextFloat() * edges.Count)];
                        currentEdge = Vector2.Transform(currentEdge, Matrix.CreateRotationZ(angle));
                        GenerateParticles(currentEdge + position, 7, angle);
                    }
                    break;
                #endregion
                #region Red Burning 13
                case 13:
                    {
                        if (SRandom.NextFloat() < 0.2f)
                        {
                            Vector2 currentEdge = edges[(int)(SRandom.NextFloat() * edges.Count)];
                            currentEdge = Vector2.Transform(currentEdge, Matrix.CreateRotationZ(angle));
                            GenerateParticles(currentEdge + position, 13, angle);
                        }
                        break;
                    }
                #endregion
                #region Yellow Bar 15
                case 15:
                    {
                        if (color.HasValue)
                        {
                            if (SRandom.NextFloat() < 0.4f)
                            {
                                Vector2 currentEdge = edges[(int)(SRandom.NextFloat() * edges.Count)];
                                CreateExplosion(1, currentEdge + position, 10, 40, 0.5f, color.Value, 0.5f, 0.5f, 0.5f);
                            }
                        }
                        break;
                    }
                    #endregion
            }
        }

        public static void GenerateParticles(Vector2 position, int ID, float angle = 0, Color? color = null)
        {
            Vector2 initialForce = Vector2.Zero;
            float angularVelocity = 0;
            if (!color.HasValue)
                color = Color.White;
            Vector2 scale = new Vector2(1,1);
            float ttl = 1;

            switch (ID)
            {
                #region Nothing 1
                case 1:
                    {
                        CreateParticle(new Sprite(spriteList[0]), position, initialForce, angle, angularVelocity, color.Value, scale, ttl, 1);
                        break;
                    }
                #endregion

                #region Enemy Explosion 2
                case 2:
                    {
                        initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        CreateParticle(new Sprite(spriteList[1]), position, initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[2]), position, -initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        if (color == Color.White)
                            color = Color.DarkViolet;
                        CreateExplosion(10, position, 10, 80, 0.2f, color.Value, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region Player Explosion 3
                case 3:
                    {            
                        initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        CreateParticle(new Sprite(spriteList[3]), position, initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[4]), position, -initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        CreateExplosion(100, position, 40, 100, 0.2f, Color.CornflowerBlue, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region Thruster Trail 4
                case 4:
                    {
                        CreateTrail(angle, position, 5, 40, 0, color.Value, 0, 2, 0.3f);
                        break;
                    }
                #endregion

                #region Bullet Explosion 5
                case 5:
                    {
                        if (color == Color.White)
                            color = Color.CornflowerBlue;
                        CreateExplosion(10, position, 10, 40, 0.5f, color.Value, 1, 0.5f, ttl);
                        break;
                    }
                #endregion

                #region Bullet Trail 6
                case 6:
                    {
                        if (color == Color.White)
                            color = Color.CornflowerBlue;
                        CreateTrail(angle, position, 1, 20, 0, color.Value, 0, 1, 0.3f);
                        break;
                    }
                #endregion

                #region Shield Visuals 7
                case 7:
                    {
                        CreateParticle(new Sprite(spriteList[0]), position, initialForce, angle, angularVelocity, Color.Gold, scale, 0.1f, 1);
                        break;
                    }
                #endregion

                #region Health Death 8
                case 8:
                    {
                        CreateExplosion(8, position, 10, 40, 0.2f, Color.WhiteSmoke, 0, 0.5f, ttl, 9);
                        break;
                    }
                #endregion

                #region Explosion Drop 9
                case 9:
                    {
                        CreateExplosion(10, position, 10, 750 / 2, 0, Color.DodgerBlue, 1, 0.5f, ttl);
                        break;
                    }
                #endregion

                #region Energy Death 12
                case 12:
                    {
                        CreateNonRotExplosion(10, position, 10, 80, 0, Color.Gold, 0, 1, ttl, 10);
                        break;
                    }
                #endregion

                #region Red Burning 13
                case 13:
                    {
                        CreateExplosion(1, position, 10, 40, 0.5f, Color.MonoGameOrange, 1, 0.5f, ttl);
                        break;
                    }
                #endregion

                #region Yellow Bar 15
                #endregion

                #region EnemyShoot Explosion 16
                case 16:
                    {
                        initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        CreateParticle(new Sprite(spriteList[5]), position, initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[6]), position, -initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        if (color == Color.White)
                            color = Color.DarkViolet;
                        CreateExplosion(10, position, 10, 80, 0.2f, color.Value, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region EnemySpeed Explosion 17
                case 17:
                    {
                        initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        CreateParticle(new Sprite(spriteList[7]), position, initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[8]), position, -initialForce, angle, angularVelocity, color.Value, scale, ttl, 2);
                        if (color == Color.White)
                            color = Color.DarkViolet;
                        CreateExplosion(10, position, 10, 80, 0.2f, color.Value, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region Asteroid Explosion 17
                case 18:
                    {
                        initialForce = 100 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        CreateParticle(new Sprite(spriteList[11]), position, initialForce, angle, 0.1f, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[12]), position, Vector2.Transform(initialForce, Matrix.CreateRotationZ(2* (float)Math.PI/3)), angle, 0.1f, color.Value, scale, ttl, 2);
                        CreateParticle(new Sprite(spriteList[13]), position, Vector2.Transform(initialForce, Matrix.CreateRotationZ(4 * (float)Math.PI / 3)), angle, 0.1f, color.Value, scale, ttl, 2);
                        CreateExplosion(30, position, 40, 80, 0.2f, Color.SaddleBrown, 1.5f, 1, ttl);
                        CreateExplosion(10, position, 40, 80, 0.2f, Color.Violet, 0.5f, 0.5f, ttl);
                        break;
                    }
                    #endregion
            }
        }

        public static void GenerateParticles(ISprite sprite, Vector2 position, int ID, float angle = 0, Color? color = null)
        {
            List<Sprite> sList = sprite.SplitSprites;
            List<Color> colors = sprite.Colors;
            Vector2 initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            CreateParticle(sList[0], position, -initialForce, angle, 0, Color.White, Vector2.One, 1, 2);
            CreateParticle(sList[1], position, initialForce, angle, 0, Color.White, Vector2.One, 1, 2);
            for(int i = 0; i < 20; i++)
                CreateExplosion(1, position, 20, 80, 0.2f, colors[SRandom.Next(0, colors.Count)], 1, 1, 1);
        }

        public static void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                if (p.IsActive)
                    p.Draw(sb, gameTime);
            }
        }

        private static void CreateExplosion(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, int spriteIndex = 0)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                float angle = RandomFloat(2 * (float)Math.PI, 0);
                CreateTrail(angle, position, spread, baseValue, angularVelocity, color, scaleSpread, baseScale, ttl, spriteIndex);
            }
        }

        private static void CreateNonRotExplosion(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, int spriteIndex = 0)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                Vector2 initialForce = RandomVector2(spread, baseValue);
                float randFloat = RandomFloat(scaleSpread, baseScale);
                Vector2 scale = new Vector2(randFloat, randFloat);
                CreateParticle(new Sprite(spriteList[spriteIndex]), position, initialForce, 0, angularVelocity, color, scale, ttl, 1); 
            }
        }

        private static void CreateTrail(float angle, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, int spriteIndex = 0)
        {
            Vector2 initialForce = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            initialForce *= RandomFloat(spread, baseValue);
            float randFloat = RandomFloat(scaleSpread, baseScale);
            Vector2 scale = new Vector2(randFloat, randFloat);
            CreateParticle(new Sprite(spriteList[spriteIndex]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1);
        }

        private static void CreateCircle(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, int spriteIndex = 0)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                Vector2 initialPosition = RandomVector2(spread, baseValue);
                float randFloat = RandomFloat(scaleSpread, baseScale);
                Vector2 scale = new Vector2(randFloat, randFloat);
                CreateParticle(new Sprite(spriteList[spriteIndex]), position + initialPosition, Vector2.Zero, (float)Math.Atan2(initialPosition.Y, initialPosition.X), angularVelocity, color, scale, ttl, 1);
            }
        }

        private static Vector2 RandomVector2(float spread, float baseValue)
        {
            Vector2 v = new Vector2(2 * SRandom.NextFloat() - 1, 2 * SRandom.NextFloat() - 1);
            v.Normalize();
            v *= RandomFloat(spread, baseValue);
            return v;
        }

        private static void CreateParticle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, int ID)
        {
            bool renewed = false;
            foreach (Particle p in particles)
            {
                if (!p.IsActive)
                {
                    p.RenewParticle(sprite, position, initialForce, angle, angularVelocity, color, scale, TTL, ID);
                    renewed = true;
                    break;
                }
            }
            if (!renewed)
            {
                particles.Add(new Particle(sprite, position, initialForce, angle, angularVelocity, color, scale, TTL, ID));
            }
        }

        private static float RandomFloat(float spread, float baseValue)
        {
            return SRandom.NextFloat() * spread + baseValue;
        }
    }
}
