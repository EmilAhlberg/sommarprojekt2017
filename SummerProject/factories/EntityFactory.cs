﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables;

namespace SummerProject.factories
{
    class EntityFactory
    {
        private const int standard = -5000;
        public static Entity CreateEntity(Sprite sprite, Player player)
        {
            return new Enemy(FarAway(), new Sprite(sprite), player);
            
        }
               
        public static Entity CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                case 1: return new Bullet(FarAway(), new Sprite(sprite));                   
                case 2: return new HomingBullet(FarAway(), new Sprite(sprite));           
                default:
                    throw new NotImplementedException();
            }        
        }

        private static Vector2 FarAway()
        {
            return new Vector2(standard, standard);
        }
    }
}
