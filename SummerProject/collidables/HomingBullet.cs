using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables
{
    class HomingBullet : Projectile
    {
        private Enemy enemy;
        private Wall wall;
        private bool lockedOn;
        private Rectangle oldBoundBox;
        private Rectangle bigBoundBox;
        private bool lockedOnEnemy;
        public HomingBullet(ISprite sprite) : base(sprite)
        {
            oldBoundBox = BoundBox;
            bigBoundBox = new Rectangle(oldBoundBox.X, oldBoundBox.Y, 200, 200);
            InitDetection();
        }
        private void InitDetection()
        {
            Damage = 0; //!   big box must do 0 dmg bcuz detection
            bigBoundBox.Location = BoundBox.Location;
            BoundBox = bigBoundBox;
            lockedOn = false;
        }
        private void InitLockOn()
        {
            Damage = 10;    // set damage here
            lockedOn = true;
            oldBoundBox.Location = BoundBox.Location;
            BoundBox = oldBoundBox;
        }

        private void CalculateAngle()
        {
            float dX;
            float dY;
            if (lockedOnEnemy)
            {
                 dX = Position.X - enemy.Position.X;
                 dY = Position.Y - enemy.Position.Y;
            }
            else
            {
                 dX = Position.X - wall.Position.X;
                 dY = Position.Y - wall.Position.Y;
            }
            base.CalculateAngle(dX, dY);
        }


        public override void Collision(Collidable c2)
        {
            if (!lockedOn)
            {
                if (c2 is Enemy)
                {
                    lockedOnEnemy = true;
                    enemy = (Enemy)c2;
                    InitLockOn();
                }
                else if (c2 is Wall)
                {
                    lockedOnEnemy = false;
                    wall = (Wall)c2;
                    InitLockOn();
                }
            }
            else if (lockedOn && (c2 is Enemy || c2 is Wall))
            {
                Death();
                InitDetection();
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (lockedOn)
            {
                if (lockedOnEnemy)
                {
                    if (enemy.isActive)
                        CalculateAngle();
                }
                else if (wall != null)
                    CalculateAngle();
                else
                    InitDetection();
            }
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
    }
}
