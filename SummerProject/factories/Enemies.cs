using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.wave;

namespace SummerProject.factories
{
    public class Enemies : Entities
    {
        private Player player;
    
        public Enemies(Player player, int NbrOfEnemies) : base(NbrOfEnemies)
        {
            this.player = player;
            InitializeEntities(EntityTypes.ENEMY);
            InitializeEntities(EntityTypes.ENEMYSHOOT);
            InitializeEntities(EntityTypes.ENEMYSPEED);
            InitializeEntities(EntityTypes.ENEMYASTER);
        }

        public void Update(GameTime gameTime)
        {
            UpdateEntities(gameTime);
        }

        public override void Reset()
        {
            ResetEntities();
        }
        public void Spawn(Vector2 source)
        {
            float rnd = SRandom.NextFloat();
            int type = EntityTypes.ENEMY;
            if (rnd < Difficulty.CAN_SHOOT_RISK) //! chance of being able to shoot
                type = EntityTypes.ENEMYSHOOT;
            else if (rnd < Difficulty.IS_SPEEDY_RISK) //! chance of being shupeedo
                type = EntityTypes.ENEMYSPEED;
            else if (rnd < Difficulty.IS_ASTEROID_RISK) //! chance of being ASTEROIIIID
                type = EntityTypes.ENEMYASTER;
            ActivateEntities(source, player.Position, type);
        }
        protected override ActivatableEntity CreateEntity(int type)
        {
            return EntityFactory.CreateEnemy(Sprites[EntityTypes.SPRITE[type]], player, type);
        }
    }
}
