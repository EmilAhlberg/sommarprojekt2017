using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (particles[i].isActive)
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
            Vector2 velocity = Vector2.Zero;
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
                        particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, angle, angularVelocity, color, scale, ttl, 1));
                        break;
                    }
                #endregion

                #region Enemy Explosion
                case 2:
                    {
                        velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[1]), position, velocity, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[2]), position, -velocity, angle, angularVelocity, color, scale, ttl, 2));
                        for(int i = 0; i < 10; i++)
                        {
                            velocity = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            velocity.Normalize();
                            velocity *= 2*((float)rand.NextDouble()+1);
                            color = Color.MonoGameOrange;
                            angularVelocity = 0.2f;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, (float)Math.Atan2(velocity.Y, velocity.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Player Explosion
                case 3:
                    {
                        velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        particles.Add(new Particle(new Sprite(spriteList[3]), position, velocity, angle, angularVelocity, color, scale, ttl, 2));
                        particles.Add(new Particle(new Sprite(spriteList[4]), position, -velocity, angle, angularVelocity, color, scale, ttl, 2));
                        for (int i = 0; i < 100; i++)
                        {
                            velocity = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            velocity.Normalize();
                            velocity *= 4*((float)rand.NextDouble() + 1);
                            color = Color.CornflowerBlue;
                            angularVelocity = 0.1f;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, (float)Math.Atan2(velocity.Y, velocity.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Thruster Trail
                case 4:
                    {
                        velocity = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        {
                            velocity.Normalize();
                            ttl = 0.3f;
                            velocity *= (float)rand.NextDouble() + 1;
                            color = Color.MonoGameOrange;
                            scale = 2;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, (float)Math.Atan2(velocity.Y, velocity.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Bullet Explosion
                case 5:
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            velocity = new Vector2(2 * (float)rand.NextDouble() - 1, 2 * (float)rand.NextDouble() - 1);
                            velocity.Normalize();
                            velocity *= ((float)rand.NextDouble() + 1);
                            color = Color.CornflowerBlue;
                            angularVelocity = 0.5f;
                            scale = 1;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, (float)Math.Atan2(velocity.Y, velocity.X), angularVelocity, color, scale, ttl, 1));
                        }
                        break;
                    }
                #endregion

                #region Bullet Trail
                case 6:
                    {
                        velocity = -new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                        {
                            velocity.Normalize();
                            ttl = 0.3f;
                            velocity *= (float)rand.NextDouble() + 1;
                            color = Color.CornflowerBlue;
                            scale = 1;
                            particles.Add(new Particle(new Sprite(spriteList[0]), position, velocity, (float)Math.Atan2(velocity.Y, velocity.X), angularVelocity, color, scale, ttl, 1));
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
                if (p.isActive)
                    p.Draw(sb, gameTime);
            }
        }
    }
}
