using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.factories
{
    class EntityFactory
    {
        public static Entity CreateEntity(Sprite sprite, Player player)
        {
            return new Enemy(new Vector2(-5000, -5000), sprite, player);
            
        }
               
        public static Entity CreateEntity(Sprite sprite, int type)
        {
            switch (type)
            {
                case 1: return new Bullet(sprite);                   
                //case 2: return new TestBullet(sprite)               
                default:
                    throw new NotImplementedException();
            }        
        }
    }
}
