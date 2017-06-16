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
    public class Projectiles : Entities
    {
        private int bulletType;
        private const float reloadTime = 0.3f;
        public Projectiles(List<Sprite> sprites, int ammoCap) : base(sprites, ammoCap, reloadTime) //!!
        {
            bulletType = EntityTypes.BULLET;
            InitializeEntities(0);
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
            RemoveInactiveType(CreateEntity(bulletType));
            bulletType = newType;
            InitializeEntities(newType);            
        }
      

        public void Update(GameTime gameTime)
        {           
            UpdateEntities(gameTime);
            RemoveAbundantType(); //cleans the entityList from eventual bullets of 'old type'
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

        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEntity(sprites[index], bulletType);
        }
    }
}
