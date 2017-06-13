using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Enemies
    {
        private List<Enemy> enemyList;
        private float spawnDelay = -0.2f;
        private Sprite sprite;
        private Player player;
        public Enemies (Sprite sprite, Player player, int NbrOfEnemies)
        {
            this.sprite = sprite;
            this.player = player;
            enemyList = new List<Enemy>();
            InitEnemy(NbrOfEnemies);
        }
        private void InitEnemy(int num)
        {
            for (int i = 0; i < num; i++)
            {
                AddEnemy(new Enemy(new Vector2(-5000, -5000), sprite, player));
            }
        }
        public void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }
        public List<Enemy> getEnemyList()
        {
            return enemyList;
        }
        public void Spawn(Vector2 pos, GameTime gt)
        {
            foreach (Enemy e in enemyList)
            {
                if (spawnDelay < 0 && !e.isActive)
                {
                    e.isActive = true;
                    e.Position = pos;
                    spawnDelay = 0.5f;
                    break;
                }
            }
            spawnDelay -= (float)gt.ElapsedGameTime.TotalSeconds;
        }
        public void Update()
        {
            foreach (Enemy e in enemyList)
            {
                if (e.isActive)
                    e.Update();

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy e in enemyList)
            {
                if (e.isActive)
                    e.Draw(spriteBatch);
            }
        }
    }
}
