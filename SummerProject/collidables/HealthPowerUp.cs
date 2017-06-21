using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables
{
    class HealthPowerUp : PowerUp
    {
        private const int givesHealth = 1;
        public HealthPowerUp(Vector2 position, ISprite sprite) : base(position, sprite)
        {
        }

        public void Spawn(Vector2 pos)
        {
            Activate(pos, pos);
        }

        public override void Collision(Collidable c2)
        {
            if (c2 is Player)
            {
                ((Player)c2).Health += givesHealth; 
                Death();
            }
        }
    }
}
