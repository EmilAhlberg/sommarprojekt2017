using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    abstract class Ammunition : Collidable
    {
        private float despawnTimer = 0f;
        private const float despawnTime = 7f;
        public bool isActive { get; set; }
        public int Damage {get; set; }

        public Ammunition(Sprite sprite) : base (Vector2.Zero, sprite)
        {

        }

        public abstract void Update(GameTime gametime);


        public override void collision(Collidable c2)
        {
            if (c2 is Enemy || c2 is Wall)
            {
                despawnTimer = despawnTime;
                isActive = false;
            }
        }

        protected override void Move()
        {
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y + (float)Math.Sin(angle));
        }
    }
}
