using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class Bullet : collidables.Projectile
    {      

        public Bullet(Sprite sprite) : base(Vector2.Zero, sprite)
        {            
            Damage = 10; //!   
        }

        public override void Update(GameTime gameTime)
        {
            UpdateTimer(gameTime);
            Move();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            float dX = source.X - target.X;
            float dY = source.Y - target.Y;
            base.CalculateAngle(dX, dY);
            ResetSpawnTime(); 
        }



       


        public override void Collision(Collidable c2)
        {
            if(c2 is Enemy || c2 is Wall)
            {
                Death();
            }
        }

        

    }
}
