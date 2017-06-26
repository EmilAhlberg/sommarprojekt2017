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
    
        public Enemies(List<Sprite> sprites, Player player, int NbrOfEnemies) : base(sprites, NbrOfEnemies)
        {
            this.player = player;
            InitializeEntities(0);
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
            ActivateEntities(source, player.Position, 0);
        }
        protected override AIEntity CreateEntity(int spriteIndex, int index)
        {
            return EntityFactory.CreateEnemy(Sprites[index], player);
        }
    }
}
