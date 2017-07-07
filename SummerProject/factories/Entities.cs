using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using System.Linq;
using System;

namespace SummerProject.factories
{
    public abstract class Entities
    {   
        protected int entityCap;
        public Dictionary<int, List<Collidable>> EntityDic { get; private set; }       

        public Entities(int entityCap)
        {
            this.entityCap = entityCap;            
            EntityDic = new Dictionary<int, List<Collidable>>();
        }

        public abstract ActivatableEntity CreateEntity(int index);
        public abstract void Reset();

        protected void InitializeEntities(int type)
        {
            if (!EntityDic.ContainsKey(type))
            {
                EntityDic[type] = new List<Collidable>();
                for (int i = 0; i < entityCap; i++)
                {
                    EntityDic[type].Insert(0, CreateEntity(type));
                }
            }
        }

        protected void ResetEntities()
        {
            foreach (ActivatableEntity e in GetValues())
                if (e.IsActive)
                    e.Death();
        }

        public List<Collidable> GetValues()
        {
            return EntityDic.Values.SelectMany(e => e).ToList();
        }

        public Entity GetEntity(int id)
        {
            List<Collidable> colList = new List<Collidable>();
            EntityDic.TryGetValue(id, out colList);
            if (colList.Count == 0)
                throw new NotImplementedException();
            foreach (Collidable c in colList)
            {
                if (c is ActivatableEntity)
                {
                    ActivatableEntity e = c as ActivatableEntity;
                    if (!e.IsActive && !e.IsBusy)
                    {
                        e.IsBusy = true;
                        return e;
                    }
                }
            }
            return null;
        }

        public bool ActivateEntities(Vector2 source, Vector2 target, int type)
        {
            foreach (ActivatableEntity e in EntityDic[type])
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
            foreach (ActivatableEntity e in GetValues())
            {
                if (e.IsActive)
                    e.Update(gameTime);
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (ActivatableEntity e in GetValues())
            {
                if (e.IsActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
