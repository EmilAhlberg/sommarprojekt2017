using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.factories;

namespace SummerProject.collidables.parts
{
    class DetectorPart : Part
    {
        //   private Entity target;
        private Enemy target;
        private ITargeting parent;
        private Type type;
        private bool detected;
        public DetectorPart(int detectionWidth, int detectionHeight, Type type, ITargeting parent, IDs id = IDs.DEFAULT) : base(id)
        {
            RotRectangle temp = BoundBox;
            BoundBox = new RotRectangle(new Rectangle((int) Position.X, (int) Position.Y, detectionWidth, detectionHeight), angle);
            BoundBox.Origin = temp.Origin;
            this.parent = parent;
            this.type = type;
        }

        public override void TakeAction()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            if (detected && target.IsActive) ;
                //parent.UpdateTarget(target);
        }

        public override void Death()
        {
            detected = false;
            target = null;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        protected override void HandleCollision(ICollidable c2)
        {
            //       if (c2.GetType() == type)
            if (c2 is Enemy)
            {
                //   target = (Entity)Convert.ChangeType(c2, type);
                target = ((Enemy)c2);
                detected = true;
            }
        }
    }
}
