using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public abstract class Entities
    {
        public List<Sprite> Sprites { get; }
        protected int entityCap;
        public List<AIEntity> EntityList { get; private set; }
        public float EventTimer { get; set; }
        protected float eventTime;

        public Entities(List<Sprite> sprites, int entityCap, float eventTime)
        {
            this.Sprites = sprites;
            this.entityCap = entityCap;
            this.eventTime = eventTime;
            EventTimer = 0;
            EntityList = new List<AIEntity>();
        }

        protected abstract AIEntity CreateEntity(int index);

        protected void InitializeEntities(int index)
        {            
            for (int i = 0; i < entityCap; i++)
            {
                EntityList.Insert(0, CreateEntity(index));
              
            }
        }

        protected void ActivateEntities(Vector2 source, Vector2 target)
        {
            foreach (AIEntity e in EntityList)
            {
                if (!e.IsActive)
                {
                    e.Activate(source, target);
                    EventTimer = eventTime;
                    break;
                }                 
            }
        }

        protected void RemoveInactiveType(AIEntity type)
        {
            int tempCap = entityCap;
            for(int i = 0; i<tempCap; i++)
            {
                if (EntityList[i].GetType().Equals(type.GetType()) && !EntityList[i].IsActive)
                {
                    EntityList.Remove(EntityList[i]);
                    i--;
                    tempCap--;
                }
            }
        }

        protected void UpdateEntities(GameTime gameTime)
        {
            foreach (AIEntity e in EntityList)
            {
                if (e.IsActive)
                    e.Update(gameTime);
            }
            EventTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AIEntity e in EntityList)
            {
                if (e.IsActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
