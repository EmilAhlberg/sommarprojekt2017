﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Projectiles : factories.Entities
    {
  
        public Projectiles(Sprite sprite, int ammoCap) : base(sprite, ammoCap, 1) //!!
        {
            InitializeEntities();
        }

        public void Fire(Vector2 source, Vector2 target)
        {
            if (EventTimer < 0)
            {
                ActivateEntities(source, target);               
            }           
        }
      

        public void Update(GameTime gameTime)
        {           
            UpdateEntities(gameTime);  
        }      

        protected override Entity createEntity()
        {
            return new Bullet(Sprite);
        }
    }
}
