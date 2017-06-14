using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class Particles
    {
        List<Particle> particles;
        List<Sprite> spriteList;
        private const int maxParticles = 100;

        public Particles(Sprite baseSprite)
        {
            particles = new List<Particle>();
            for(int i = 0; i < maxParticles; i++)
            {
                particles.Add(new Particle(Vector2.Zero, new Sprite(baseSprite)));
            }
        }

        public void addSprite(Sprite s)
        {
            spriteList.Add(s);
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Particle p in particles) //flattens the lists
            {
                if(p.isActive)
                    p.Update(gameTime);
            }
        }

        public void CreateParticle(Vector2 position, int ID, float angle = 0)
        {
            foreach(Particle p in particles)
            {
                if (!p.isActive)
                {
                    p.Activate(position, angle, ID);
                    break;
                }
            }
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (Particle p in particles) //flattens the lists
            {
                if (p.isActive)
                    p.Draw(sb, gameTime);
            }
        }
    }
}
