using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using System.Linq;

namespace SummerProject.factories
{
    public abstract class Entities
    {
        public List<Sprite> Sprites { get; }
        protected int entityCap;
        public Dictionary<int, List<AIEntity>> EntityDic { get; private set; }       

        public Entities(List<Sprite> sprites, int entityCap)
        {
            this.Sprites = sprites;
            this.entityCap = entityCap;            
            EntityDic = new Dictionary<int, List<AIEntity>>();
        }

        protected abstract AIEntity CreateEntity(int spriteIndex, int index);
        public abstract void Reset();

        protected void InitializeEntities(int type)
        {
            if (!EntityDic.ContainsKey(type))
            {
                EntityDic[type] = new List<AIEntity>();
                for (int i = 0; i < entityCap; i++)
                {
                    EntityDic[type].Insert(0, CreateEntity(EntityTypes.SPRITE[type], type));
                }
            }
        }

        protected void ResetEntities()
        {
            foreach (AIEntity e in GetValues())
                if (e.IsActive)
                    e.Death();
        }

        public List<AIEntity> GetValues()
        {
            return EntityDic.Values.SelectMany(e => e).ToList();
        }

        public bool ActivateEntities(Vector2 source, Vector2 target, int type)
        {
            foreach (AIEntity e in EntityDic[type])
            {
                if (!e.IsActive)
                {
                    e.Activate(source, target);                   
                    return true;                  
                }
            }
            return false;
        }

        //protected void RemoveInactiveType(AIEntity type)
        //{
        //    int tempCap = entityCap;
        //    for (int i = 0; i < tempCap; i++)
        //    {
        //        if (EntityDic[i].GetType().Equals(type.GetType()) && !EntityDic[i].IsActive)
        //        {
        //            EntityDic.Remove(EntityDic[i]);
        //            i--;
        //            tempCap--;
        //        }
        //    }
        //}

        protected void UpdateEntities(GameTime gameTime)
        {
            foreach (AIEntity e in GetValues())
            {
                if (e.IsActive)
                    e.Update(gameTime);
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AIEntity e in GetValues())
            {
                if (e.IsActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
