using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public static class Particles
    {
        static List<Particle> particles;
        static Random rand;
        static List<Sprite> spriteList;
        private const int maxParticles = 100;

        static Particles()
        {
            particles = new List<Particle>();
            spriteList = new List<Sprite>();
            spriteList.Add(new Sprite());
            rand = new Random();
        }

        public static void AddSprite(Sprite s)
        {
            spriteList.Add(new Sprite(s));
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].IsActive)
                {
                    particles[i].Update(gameTime);
                }
                else
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void GenerateParticles(Vector2 position, int ID, float angle = 0)
        {
            Vector2 initialForce = Vector2.Zero; //TODO: FIX
            float angularVelocity = 0;
            Color color = Color.White;
            float scale = 1;
            float ttl = 1;

            switch (ID)
            {
                #region Nothing
                case 1:
                    {
                        particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, angle, angularVelocity, color, scale, ttl, 1));
                        break;
                    }
                #endregion

                #region Enemy Explosion
                case 2:
                    {
                        initialForce = 30 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[1]), position, initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[2]), position, -initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        CreateExplosion(10, position, 10, 40, 0.2f, Color.MonoGameOrange, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region Player Explosion
                case 3:
                    {
                        initialForce = 30 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[3]), position, initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[4]), position, -initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        CreateExplosion(100, position, 20, 80, 0.2f, Color.CornflowerBlue, 1, 1, ttl);
                        break;
                    }
                #endregion

                #region Thruster Trail
                case 4:
                    {
                        CreateTrail(angle, position, 5, 40, 0, Color.MonoGameOrange, 0, 2, 0.3f);
                        break;
                    }
                #endregion

                #region Bullet Explosion
                case 5:
                    {
                        CreateExplosion(10, position, 10, 20, 0.5f, Color.CornflowerBlue, 1, 0.5f, ttl);
                        break;
                    }
                #endregion

                #region Bullet Trail
                case 6:
                    {
                        CreateTrail(angle, position, 1, 20, 0, Color.CornflowerBlue, 0, 1, 0.3f);
                        break;
                    }
                #endregion

                #region Shield Visuals
                case 7:
                    {
                        CreateExplosion(1, position, 0, 40, 0, Color.Yellow, 0, 1f, ttl);
                        break;
                    }
                    #endregion
            }
        }

        public static void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                if (p.IsActive)
                    p.Draw(sb, gameTime);
            }
        }

        private static void CreateExplosion(int nbrOfParticles, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl)
        {
            for (int i = 0; i < nbrOfParticles; i++)
            {
                Vector2 initialForce = RandomVector2(spread, baseValue);
                float scale = RandomFloat(scaleSpread, baseScale);
                particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
            }
        }

        private static void CreateTrail(float angle, Vector2 position, float spread, float baseValue, float angularVelocity, Color color, float scaleSpread, float baseScale, float ttl)
        {
            Vector2 initialForce = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            initialForce *= RandomFloat(spread, baseValue);
            float scale = RandomFloat(scaleSpread, baseScale);
            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
        }

        private static Vector2 RandomVector2(float spread, float baseValue)
        {
            Vector2 v = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
            v.Normalize();
            v *= RandomFloat(spread, baseValue);
            return v;
        }

        private static float RandomFloat(float spread, float baseValue)
        {
            return (float)rand.NextDouble() * spread + baseValue;
        }
    }
}
