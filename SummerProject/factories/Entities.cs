using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.factories
{
    abstract class Entities
    {
        public Sprite Sprite { get; }
        private int entityCap;
        public List<Entity> entityList { get; private set; }
        public float EventTimer { get; set; }
        private float eventTime;

        public Entities(Sprite sprite, int entityCap, float eventTime)
        {
            Sprite = sprite;
            this.entityCap = entityCap;
            this.eventTime = eventTime;
            EventTimer = 0;                     
        }

        protected abstract Entity createEntity();

        protected void InitializeEntities()
        {
            entityList = new List<Entity>();
            for (int i = 0; i < entityCap; i++)
            {
                entityList.Add(createEntity());
            }
        }

        protected void ActivateEntities(Vector2 source, Vector2 target)
        {
            foreach (Entity e in entityList)
            {
                if (!e.isActive)
                {
                    e.Activate(source, target);
                    EventTimer = eventTime;
                    break;
                }                 
            }
        }

        protected void UpdateEntities(GameTime gameTime)
        {
            foreach (Entity e in entityList)
            {
                if (e.isActive)
                    e.Update(gameTime);
            }
            EventTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Entity e in entityList)
            {
                if (e.isActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
