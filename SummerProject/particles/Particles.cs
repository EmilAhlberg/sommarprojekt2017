using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public static class Particles
    {
        static List<Particle> particles;
        private const int maxParticles = 1000;

        static Particles()
        {
            particles = new List<Particle>();
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
                        CreateParticle( position, initialForce, angle, angularVelocity, Color.Gold, scale, 0.1f);
                        break;
                    }
                #endregion

                #region Health Death 8
                case 8:
                    {
                        CreateExplosion(8, position, 10, 40, 0.2f, Color.WhiteSmoke, 0, 0.5f, ttl, IDs.WRENCH);
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
                        CreateNonRotExplosion(10, position, 10, 80, 0, Color.Gold, 0, 1, ttl, IDs.BOLT);
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

            }
        }

        public static void GenerateAfterImageParticles(ISprite sprite, Vector2 position, int ID, float angle, Vector2 scale)
        {
            CreateParticle(sprite, position, Vector2.Zero, angle, 0, Color.White, scale, 0.1f, IDs.AFTERIMAGE);
        }


        public static void GenerateDeathParticles(ISprite sprite, Vector2 position, int ID, float angle, bool showDeathParticles)
        {
            
            List<Color> colors = sprite.Colors;
            if (showDeathParticles)
            {
                List<Sprite> sList = sprite.SplitSprites;
                Vector2 initialForce = 50 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                CreateParticle(sList[0], position, -initialForce, angle, 0, Color.White, Vector2.One, 1, IDs.DEATH);
                CreateParticle(sList[1], position, initialForce, angle, 0, Color.White, Vector2.One, 1, IDs.DEATH);
            }
            for(int i = 0; i < 20; i++)
                CreateExplosion(1, position, 20, 80, 0.5f, colors[SRandom.Next(0, colors.Count)], 1, 1, 1);
        }

        public static void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                if (p.IsActive)
                    p.Draw(sb, gameTime);
            }
        }

        private static void CreateExplosion(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, IDs ID = IDs.DEFAULT_PARTICLE)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                float angle = RandomFloat(2 * (float)Math.PI, 0);
                CreateTrail(angle, position, spread, baseValue, angularVelocity, color, scaleSpread, baseScale, ttl, ID);
            }
        }

        private static void CreateNonRotExplosion(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, IDs ID = IDs.DEFAULT_PARTICLE)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                Vector2 initialForce = RandomVector2(spread, baseValue);
                float randFloat = RandomFloat(scaleSpread, baseScale);
                Vector2 scale = new Vector2(randFloat, randFloat);
                CreateParticle(position, initialForce, 0, angularVelocity, color, scale, ttl, ID); 
            }
        }

        private static void CreateTrail(float angle, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, IDs ID = IDs.DEFAULT_PARTICLE)
        {
            Vector2 initialForce = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            initialForce *= RandomFloat(spread, baseValue);
            float randFloat = RandomFloat(scaleSpread, baseScale);
            Vector2 scale = new Vector2(randFloat, randFloat);
            CreateParticle(position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, ID);
        }

        private static void CreateCircle(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl, IDs ID = IDs.DEFAULT_PARTICLE)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                Vector2 initialPosition = RandomVector2(spread, baseValue);
                float randFloat = RandomFloat(scaleSpread, baseScale);
                Vector2 scale = new Vector2(randFloat, randFloat);
                CreateParticle(position + initialPosition, Vector2.Zero, (float)Math.Atan2(initialPosition.Y, initialPosition.X), angularVelocity, color, scale, ttl, ID);
            }
        }

        private static Vector2 RandomVector2(float spread, float baseValue)
        {
            Vector2 v = new Vector2(2 * SRandom.NextFloat() - 1, 2 * SRandom.NextFloat() - 1);
            v.Normalize();
            v *= RandomFloat(spread, baseValue);
            return v;
        }

        private static void CreateParticle(Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs ID = IDs.DEFAULT_PARTICLE)
        {
            bool renewed = false;
            foreach (Particle p in particles)
            {
                if (!p.IsActive)
                {
                    p.RenewParticle(position, initialForce, angle, angularVelocity, color, scale, TTL, ID);
                    renewed = true;
                    break;
                }
            }
            if (!renewed)
            {
                particles.Add(new Particle( position, initialForce, angle, angularVelocity, color, scale, TTL, ID));
            }
        }
        private static void CreateParticle(ISprite sprite, Vector2 position, Vector2 initialForce, float angle, float angularVelocity, Color color, Vector2 scale, float TTL, IDs ID = IDs.DEFAULT_PARTICLE)
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
