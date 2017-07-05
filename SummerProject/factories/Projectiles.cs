using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    public class Projectiles : Entities
    {

        public Projectiles(int ammoCap) : base(ammoCap)
        {
            InitializeEntities((int)IDs.DEFAULT_BULLET);
            InitializeEntities((int)IDs.HOMINGBULLET);
            InitializeEntities((int)IDs.SPRAYBULLET);
            InitializeEntities((int)IDs.EVILBULLET);
            InitializeEntities((int)IDs.MINEBULLET);
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

        public void EvilFire(Vector2 source, Vector2 target)
        {
            ActivateEntities(source, target, (int)IDs.EVILBULLET);
        }

        public override void Reset()
        {
            ResetEntities();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
        }
        protected override ActivatableEntity CreateEntity(int type)
        {
            return EntityFactory.CreateEntity(SpriteHandler.Sprites[SpriteHandler.SPRITE[type]], type); //! LOL
        }
    }
}
