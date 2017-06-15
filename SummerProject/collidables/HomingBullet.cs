//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace SummerProject.collidables
//{
//    class HomingBullet : Projectile
//    {
//        private Enemy enemy;
//        private bool lockedOn;
//        private Rectangle oldBoundBox;
//        private Rectangle bigBoundBox;
//        private const int homingDamage = 10;
//        private const int homingHealth = 2;

//        public HomingBullet(Vector2 position, ISprite sprite) : base(position, sprite)
//        {
//            oldBoundBox = BoundBoxes[0];
//            bigBoundBox = new Rectangle(oldBoundBox.X, oldBoundBox.Y, 200, 200);
//            InitDetection();
//            Damage = homingDamage;
//            Health = homingHealth;
//        }
//        private void InitDetection()
//        {
//            Damage = 0; //!   big box must do 0 dmg bcuz detection
//            bigBoundBox.Location = BoundBoxes[0].Location;
//            bigBoundBox.Offset(200, 0);
//            BoundBoxes[0] = bigBoundBox;
//            lockedOn = false;
//        }
//        private void InitLockOn()
//        {
//            lockedOn = true;
//            oldBoundBox.Location = BoundBoxes[0].Location;
//            oldBoundBox.Offset(-200, 0);
//            BoundBoxes[0] = oldBoundBox;
//            Damage = homingDamage;    // set damage here
//        }

//        private void CalculateAngle()
//        {
//            float dX = Position.X - enemy.Position.X;
//            float dY = Position.Y - enemy.Position.Y;
//            base.CalculateAngle(dX, dY);
//        }


//        public override void Collision(Collidable c2)
//        {
//            if (!lockedOn)
//            {
//                if (c2 is Enemy)
//                {
//                    enemy = (Enemy)c2;
//                    InitLockOn();
//                }
//                else if (c2 is Wall)
//                {
//                    oldBoundBox.Location = BoundBoxes[0].Location;
//                    BoundBoxes[0] = oldBoundBox;
//                    if (c2.BoundBoxes[0].Intersects(oldBoundBox))
//                    {
//                        Death();
//                        InitDetection();
//                    }
//                }
//            }
//            else if (lockedOn && (c2 is Enemy || c2 is Wall))
//            {
//                Death();
//                InitDetection();
//            }

//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (lockedOn)
//            {
//                if (enemy.isActive)
//                    CalculateAngle();
//                else
//                    InitDetection();
//            }
//            UpdateTimer(gameTime);
//            Move();

//        }

//        protected override void SpecificActivation(Vector2 source, Vector2 target)
//        {
//            float dX = source.X - target.X;
//            float dY = source.Y - target.Y;
//            base.CalculateAngle(dX, dY);
//            ResetSpawnTime();
//        }
//    }
//}
