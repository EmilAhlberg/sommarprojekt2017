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
            InitializeEntities((int)IDs.DEFAULT_ENEMY);
            InitializeEntities((int)IDs.ENEMYSHOOT);
            InitializeEntities((int)IDs.ENEMYSPEED);
            InitializeEntities((int)IDs.ENEMYASTER);
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
            int type = (int)IDs.DEFAULT_ENEMY;
            if (rnd < Difficulty.CAN_SHOOT_RISK) //! chance of being able to shoot
                type = (int)IDs.ENEMYSHOOT;
            else if (rnd < Difficulty.IS_SPEEDY_RISK) //! chance of being shupeedo
                type = (int)IDs.ENEMYSPEED;
            else if (rnd < Difficulty.IS_ASTEROID_RISK) //! chance of being ASTEROIIIID
                type = (int)IDs.ENEMYASTER;
            ActivateEntities(source, player.Position, type);
        }
        public override IActivatable CreateEntity(int type)
        {
            return EntityFactory.CreateEnemy(SpriteHandler.GetSprite(type), player, type);
        }
    }
}
