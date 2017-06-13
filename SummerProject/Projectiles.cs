using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Projectiles
    {
        public List<Bullet> projectiles { get; private set; }
        private int bulletCap;
        private Sprite sprite;
        public Projectiles(Sprite sprite)
        {
            this.sprite = sprite;
            projectiles = new List<Bullet>();
            bulletCap = 10;
            initializeBullets();
        }

        private void initializeBullets()
        {
            for (int i = 0; i<bulletCap; i++)
            {
                projectiles.Add(new Bullet(sprite));
            }           
        }

        public void Fire(Vector2 source, Vector2 target)
        {
            foreach (Bullet b in projectiles)
            {
                if(!b.isActive )
                {
                    b.Activate(source, target);
                    break;
                }
            }
        }

        public void Update()
        {
            foreach (Bullet b in projectiles)
            {
                if (b.isActive)                
                    b.Update();
                
            }           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in projectiles)
            {
                if(b.isActive)
                    b.Draw(spriteBatch);
            }
        }
    }
}
