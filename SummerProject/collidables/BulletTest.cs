using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject
{
    class BulletTest : collidables.Projectile
    {
        private Enemy enemy;
        private bool lockedOn;
        private Rectangle oldBoundBox;
        private Rectangle bigBoundBox;
        public BulletTest(Sprite sprite) : base(Vector2.Zero, sprite)
        {
            this.sprite = sprite;
            Damage = 10; //!   
            oldBoundBox = BoundBox;
            bigBoundBox = new Rectangle(oldBoundBox.X, oldBoundBox.Y, 200, 200);
            BoundBox = bigBoundBox;
        }

        private void CalculateAngle()
        {
            float dX = Position.X - enemy.Position.X;
            float dY = Position.Y - enemy.Position.Y;
            base.CalculateAngle(dX, dY);
        }


        protected override void Move()
        {
            if (lockedOn)
                CalculateAngle();
            Position = new Vector2(Position.X + (float)Math.Cos(angle), Position.Y + (float)Math.Sin(angle));
        }

        public override void Collision(Collidable c2)
        {
            if (!lockedOn && c2 is Enemy)
            {
                enemy = (Enemy)c2;
                lockedOn = true;
                BoundBox = oldBoundBox;
            }
            else if (lockedOn && (c2 is Enemy || c2 is Wall))
            {
                Death();
                lockedOn = false;
                BoundBox = bigBoundBox;
            }
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        protected override void SpecificActivation(Vector2 source, Vector2 target)
        {
            throw new NotImplementedException();
        }
    }
}
