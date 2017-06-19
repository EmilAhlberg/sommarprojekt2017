using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            if (eventTimer.IsFinished)
            {
                ActivateEntities(source, target);
            }
        }

        public void SwitchBullets(int newType)
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
            for (int i = EntityList.Count - 1; i >= entityCap; i--)
            {
                if (!EntityList[i].IsActive)
                {
                    EntityList.Remove(EntityList[i]);
                }
            }
        }

        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEntity(Sprites[index], bulletType);
        }
    }
}
