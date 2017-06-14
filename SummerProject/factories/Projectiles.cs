using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.factories
{
    class Projectiles : Entities
    {
        private int bulletType;
        public Projectiles(Sprite sprite, int ammoCap) : base(sprite, ammoCap, 1) //!!
        {
            bulletType = EntityTypes.BULLET_TYPE;
            InitializeEntities();
        }

        public void Fire(Vector2 source, Vector2 target)
        {
            if (EventTimer < 0)
            {
                ActivateEntities(source, target);               
            }           
        }

        public void switchBullets(int newType)
        {
            RemoveInactiveType(createEntity());
            bulletType = newType;
            InitializeEntities();
            
        }
      

        public void Update(GameTime gameTime)
        {           
            UpdateEntities(gameTime);
            RemoveAbundantType();

        }

        private void RemoveAbundantType()
        {
            for(int i = entityList.Count-1; i>=entityCap; i--)
            {
                if (!entityList[i].isActive)
                {
                    entityList.Remove(entityList[i]);
                }
            }
        }

        protected override Entity createEntity()
        {
            return EntityFactory.CreateEntity(Sprite, bulletType);
        }
    }
}
