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
        private const int maxParticles = 100;

        static Particles()
        {
            particles = new List<Particle>();
            for(int i = 0; i < maxParticles; i++)
            {
                particles.Add(new Particle(Vector2.Zero, new Sprite())); //!
            }
        }

        public static void AddSprite(Sprite s)
        {
            foreach(Particle p in particles)
                p.AddSprite(new Sprite(s));
        }

        public static void Update(GameTime gameTime)
        {
            foreach (Particle p in particles) //flattens the lists
            {
                if(p.isActive)
                    p.Update(gameTime);
            }
        }

        public static void CreateParticle(Vector2 position, int ID, float angle = 0)
        {
            foreach(Particle p in particles)
            {
                if (!p.isActive)
                {
                    p.Activate(position, angle, 1, 1, ID);
                    break;
                }
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
