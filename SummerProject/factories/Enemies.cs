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
        public Enemies (List<Sprite> sprites, Player player, int NbrOfEnemies) : base(sprites, NbrOfEnemies, 0.5f)
        {         
            this.player = player;
            InitializeEntities(0);
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


        protected override AIEntity CreateEntity(int index)
        {
            return EntityFactory.CreateEntity(sprites[index], player);
        }
    }
}
