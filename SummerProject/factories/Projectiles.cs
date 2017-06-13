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
        public List<Entity> projectiles { get; private set; }
        private int bulletCap;
        private Sprite sprite;
        private float reloadTimer = 1f;
        private const float reloadTime = 1f;
            
             
        public Projectiles(Sprite sprite)
        {
            this.sprite = sprite;
            projectiles = new List<Entity>();
            bulletCap = 10;
            initializeBullets();
        }

        private void initializeBullets()
        {
            for (int i = 0; i<bulletCap; i++)
            {
                projectiles.Add(new Bullet(new Sprite(sprite)));
            }           
        }

        public void Fire(Vector2 source, Vector2 target)
        {
            if (reloadTimer > reloadTime)
            {
                TryToShoot(source, target);               
            }           
        }

        private void TryToShoot(Vector2 source, Vector2 target)
        {
            foreach (Entity p in projectiles)
            {
                if (!p.isActive)
                {
                    p.Activate(source, target);
                    reloadTimer = 0;
                    break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Entity p in projectiles)
            {
                if (p.isActive)                
                    p.Update(gameTime);
                
            }           
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Entity p in projectiles)
            {
                if(p.isActive)
                    p.Draw(spriteBatch, gameTime);
            }
        }
    }
}
