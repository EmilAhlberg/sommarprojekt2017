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

        public Projectiles(int ammoCap) : base(ammoCap)
        {
            bulletType = EntityTypes.BULLET;
            reloadTimer = new Timer(reloadTime);
            InitializeEntities(0);
            InitializeEntities(EntityTypes.EVILBULLET);
            Enemy.projectiles = this; //! Hmmmmm
        }

        public bool Fire(Vector2 source, Vector2 target)
        {
            if (reloadTimer.IsFinished)
            {
                if (ActivateEntities(source, target, bulletType))
                {
                    reloadTimer.Reset();
                    return true; //bool type: only because of players shotsFiredTrait
                }                
            }
            return false;
        }

        public void EvilFire(Vector2 source, Vector2 target)
        {
            ActivateEntities(source, target, EntityTypes.EVILBULLET);
        }

        public void SwitchBullets(int newType)
        {
            //RemoveInactiveType(CreateEntity(bulletType));
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
            //RemoveAbundantType(); //cleans the entityList from eventual bullets of 'old type'
            reloadTimer.CountDown(gameTime);
        }

        //private void RemoveAbundantType()
        //{
        //    for (int i = EntityDic.Count - 1; i >= entityCap; i--)
        //    {
        //        if (!EntityDic[i].IsActive)
        //        {
        //            EntityDic.Remove(EntityDic[i]);
        //        }
        //    }
        //}

        protected override AIEntity CreateEntity(int type)
        {
            return EntityFactory.CreateEntity(Sprites[EntityTypes.SPRITE[type]], type); //! LOL
        }
    }
}
