using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SummerProject.collidables;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.factories
{
    public class Enemies : Entities
    {
        private Player player;
    
        public Enemies(Player player, int NbrOfEnemies) : base(NbrOfEnemies)
        {
            this.player = player;
            InitializeEntities(EntityTypes.ENEMY);
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
            ActivateEntities(source, player.Position, EntityTypes.ENEMY);
        }
        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEnemy(Sprites[EntityTypes.SPRITE[index]], player);
        }
    }
}
