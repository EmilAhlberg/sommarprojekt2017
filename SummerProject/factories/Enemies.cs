using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class Enemies : Entities
    {             
        private Player player;
        public Enemies (Sprite sprite, Player player, int NbrOfEnemies) : base(sprite, NbrOfEnemies, 0.5f)
        {         
            this.player = player;
            InitializeEntities();
        }
     
        public void Update(GameTime gameTime)
        {
            Spawn(new Vector2(250, 250), player.Position); //!
            UpdateEntities(gameTime);
        }

        public void Spawn(Vector2 source, Vector2 target)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && EventTimer < 0)
                ActivateEntities(source, target);
        }


        protected override AIEntity createEntity()
        {
            return EntityFactory.CreateEntity(Sprite, player);
        }
    }
}
