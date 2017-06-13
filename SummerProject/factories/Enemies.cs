using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerProject
{
    class Enemies
    {
        public List<Enemy> EnemyList { get; private set; }
        private float spawnDelay = -0.2f;
        private Sprite sprite;
        private Player player;
        public Enemies (Sprite sprite, Player player, int NbrOfEnemies)
        {
            this.sprite = sprite;
            this.player = player;
            EnemyList = new List<Enemy>();
            InitEnemy(NbrOfEnemies);
        }
        private void InitEnemy(int num)
        {
            for (int i = 0; i < num; i++)
            {
                AddEnemy(new Enemy(new Vector2(-5000, -5000), new Sprite(sprite), player));
            }
        }
        public void AddEnemy(Enemy enemy)
        {
            EnemyList.Add(enemy);
        }

        public void Spawn(Vector2 pos, GameTime gt)
        {
            foreach (Enemy e in EnemyList)
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
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Spawn(new Vector2(250, 250), gameTime);
            foreach (Enemy e in EnemyList)
            {
                if (e.isActive)
                    e.Update();

            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Enemy e in EnemyList)
            {
                if (e.isActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
