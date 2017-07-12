using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace SummerProject.factories
{
    public abstract class Entities
    {
        public Dictionary<int, List<IActivatable>> EntityDic { get; private set; }       

        public Entities()
        {      
            EntityDic = new Dictionary<int, List<IActivatable>>();
        }

        public abstract IActivatable CreateEntity(int index);
        public abstract void Reset();

        protected void InitializeEntities(int type, int amountOfEntities)
        {
            if (!EntityDic.ContainsKey(type))
            {
                EntityDic[type] = new List<IActivatable>();
                for (int i = 0; i < amountOfEntities; i++)
                {
                    IActivatable c = CreateEntity(type);
                    EntityDic[type].Insert(0, c);
                }
            }
        }

        protected void AddExtraEntities(int type, int amountOfEntities)
        {
            for (int i = EntityDic.Count; i < amountOfEntities + EntityDic.Count; i++)
            {
                IActivatable c = CreateEntity(type);
                EntityDic[type].Insert(0, c);
            }
        }

        protected void ResetEntities()
        {
            foreach (IActivatable e in GetValues())
                if (e.IsActive)
                    e.Death();
        }

        public List<IActivatable> GetValues()
        {
            return EntityDic.Values.SelectMany(e => e).ToList();
        }

        public IActivatable GetEntity(int id)
        {
            List<IActivatable> colList = new List<IActivatable>();
            EntityDic.TryGetValue(id, out colList);
            if (colList.Count == 0)
                throw new NotImplementedException();
            foreach (IActivatable e in colList)
            {
                if (!e.IsActive && !e.IsBusy)
                {
                    e.IsBusy = true;
                    return e;
                }
            }
            return colList[0];
        }

        public bool ActivateEntities(Vector2 source, Vector2 target, int type)
        {
            foreach (IActivatable e in EntityDic[type])
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
            foreach (IActivatable e in GetValues())
            {
                if (e.IsActive)
                    e.Update(gameTime);
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IActivatable e in GetValues())
            {
                if (e.IsActive)
                    e.Draw(spriteBatch, gameTime);
            }
        }
    }
}
