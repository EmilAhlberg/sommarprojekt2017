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
        public static Entity CreateEntity(int n, Player player, Sprite sprite)
        {
            return new Enemy(new Vector2(-5000, -5000), sprite, player);
            
        }
    }
}
