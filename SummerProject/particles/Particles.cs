using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SummerProject
{
    public static class Particles
    {
        static List<Particle> particles;
        static List<Sprite> spriteList;
        private const int maxParticles = 100;

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
            Random rand = new Random();

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
                        initialForce = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[1]), position, initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[2]), position, -initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        for (int i = 0; i < 10; i++)
                        {
                            initialForce = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            initialForce.Normalize();
                            initialForce *= 2 * ((float)rand.NextDouble() + 1);
                            color = Color.MonoGameOrange;
                            angularVelocity = 0.2f;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Player Explosion
                case 3:
                    {
                        initialForce = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[3]), position, initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[4]), position, -initialForce, angle, angularVelocity, color, scale, ttl, 2));
                        for (int i = 0; i < 100; i++)
                        {
                            initialForce = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            initialForce.Normalize();
                            initialForce *= 4 * ((float)rand.NextDouble() + 1);
                            color = Color.CornflowerBlue;
                            angularVelocity = 0.1f;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Thruster Trail
                case 4:
                    {
                        initialForce = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        {
                            initialForce.Normalize();
                            ttl = 0.3f;
                            initialForce *= (float)rand.NextDouble() + 1;
                            color = Color.MonoGameOrange;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Bullet Explosion
                case 5:
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            initialForce = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            initialForce.Normalize();
                            initialForce *= ((float)rand.NextDouble() + 1);
                            color = Color.CornflowerBlue;
                            angularVelocity = 0.5f;
                            scale = 1;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Bullet Trail
                case 6:
                    {
                        initialForce = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        {
                            initialForce.Normalize();
                            ttl = 0.3f;
                            initialForce *= (float)rand.NextDouble() + 1;
                            color = Color.CornflowerBlue;
                            scale = 1;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, initialForce, (float)Math.Atan2(initialForce.Y, initialForce.X), angularVelocity, color, scale, ttl, 1));
                        }
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
    }
}
