using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public class Projectiles : Entities
    {

        public Projectiles(int ammoCap) : base(ammoCap)
        {
            InitializeEntities(EntityTypes.BULLET);
            InitializeEntities(EntityTypes.HOMINGBULLET);
            InitializeEntities(EntityTypes.SPRAYBULLET);
            InitializeEntities(EntityTypes.EVILBULLET);
            InitializeEntities(EntityTypes.CHARGINGBULLET);
            //Enemy.projectiles = this; //! Hmmmmm
        }

        public bool Fire(Vector2 source, Vector2 target, int bulletType)
        {

                if (ActivateEntities(source, target, bulletType))
                {
                    return true; //bool type: only because of players shotsFiredTrait
                }
            return false;

        }

        public void FireSpecificBullet (Vector2 source, Vector2 target, Projectile bullet)
        {
            EntityDic[EntityTypes.CHARGINGBULLET].Add(bullet);
            bullet.Activate(source, target);
        }


        public void EvilFire(Vector2 source, Vector2 target)
        {
            ActivateEntities(source, target, EntityTypes.EVILBULLET);
        }

        public override void Reset()
        {
            ResetEntities();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
        }
        public override ActivatableEntity CreateEntity(int type)
        {
            return EntityFactory.CreateEntity(Sprites[EntityTypes.SPRITE[type]], type); //! LOL
        }
    }
}
