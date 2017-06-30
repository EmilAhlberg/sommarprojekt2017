using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public class Projectiles : Entities
    {
        private const float reloadTime = 0.3f;
        private int bulletType;
        private Timer reloadTimer;

        public Projectiles(List<Sprite> sprites, int ammoCap) : base(sprites, ammoCap)
        {
            bulletType = EntityTypes.BULLET;
            reloadTimer = new Timer(reloadTime);
            InitializeEntities(0);
        }

        public void Fire(Vector2 source, Vector2 target)
        {
            if (reloadTimer.IsFinished)
            {
                if (ActivateEntities(source, target))
                    reloadTimer.Reset();
            }
        }

        public void SwitchBullets(int newType)
        {
            RemoveInactiveType(CreateEntity(bulletType));
            bulletType = newType;
            InitializeEntities(newType);
        }

        public override void Reset()
        {
            reloadTimer.Reset();
            ResetEntities();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
            RemoveAbundantType(); //cleans the entityList from eventual bullets of 'old type'
            reloadTimer.CountDown(gameTime);
        }

        private void RemoveAbundantType()
        {
            //for (int i = EntityList.Count - 1; i >= entityCap; i--)
            //{
            //    if (!EntityList[i].IsActive)
            //    {
            //        EntityList.Remove(EntityList[i]);
            //    }
            //}
        }

        protected override ActivatableEntity CreateEntity(int index)
        {
            return EntityFactory.CreateProjectile(Sprites[index], bulletType);
        }
    }
}
