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
        public List<ActivatableEntity> EntityList { get; private set; }       

        public Entities(List<Sprite> sprites, int entityCap)
        {
            this.Sprites = sprites;
            this.entityCap = entityCap;            
            EntityList = new List<ActivatableEntity>();
        }

        protected abstract /*ActivatableEntity*/ CreateEntity(int index);
        public abstract void Reset();

        protected void InitializeEntities(int index)
        {
            for (int i = 0; i < entityCap; i++)
            {
                EntityList.Insert(0, CreateEntity(index));

            }
        }
        protected void ResetEntities()
        {
            foreach (ActivatableEntity e in EntityList)
                if (e.IsActive)
                    e.Death();
        }

        protected bool ActivateEntities(Vector2 source, Vector2 target)
        {
            foreach (ActivatableEntity e in EntityList)
            {
                if (!e.IsActive)
                {
                    e.Activate(source, target);                   
                    return true;                  
                }
            }
            return false;
        }

        protected void RemoveInactiveType(ActivatableEntity type)
        {
            int tempCap = entityCap;
            for (int i = 0; i < tempCap; i++)
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
            foreach (ActivatableEntity e in EntityList)
            {
                if (e.IsActive)
                    e.Update(gameTime);
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (ActivatableEntity e in EntityList)
            {
                if (e.IsActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
