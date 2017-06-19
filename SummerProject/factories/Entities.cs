using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public abstract class Entities
    {
        public List<Sprite> sprites { get; }
        protected int entityCap;
        public List<AIEntity> entityList { get; private set; }
        protected Timer eventTimer { get; set; }

        public Entities(List<Sprite> sprites, int entityCap, float eventTime)
        {
            this.sprites = sprites;
            this.entityCap = entityCap;
            eventTimer = new Timer(eventTime);
            entityList = new List<AIEntity>();
        }

        protected abstract AIEntity CreateEntity(int index);

        protected void InitializeEntities(int index)
        {            
            for (int i = 0; i < entityCap; i++)
            {
                entityList.Insert(0, CreateEntity(index));
              
            }
        }

        protected void ActivateEntities(Vector2 source, Vector2 target)
        {
            foreach (AIEntity e in entityList)
            {
                if (!e.isActive)
                {
                    e.Activate(source, target);
                    eventTimer.Reset();
                    break;
                }                 
            }
        }

        protected void RemoveInactiveType(AIEntity type)
        {
            int tempCap = entityCap;
            for(int i = 0; i<tempCap; i++)
            {
                if (entityList[i].GetType().Equals(type.GetType()) && !entityList[i].isActive)
                {
                    entityList.Remove(entityList[i]);
                    i--;
                    tempCap--;
                }
            }
        }

        protected void UpdateEntities(GameTime gameTime)
        {
            foreach (AIEntity e in entityList)
            {
                if (e.isActive)
                    e.Update(gameTime);
            }
            eventTimer.CountDown(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AIEntity e in entityList)
            {
                if (e.isActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
