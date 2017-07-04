﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.collidables.parts
{
    class DetectorPart : Part
    {
        //   private Entity target;
        private Enemy target;
        private ITargeting parent;
        private Type type;
        private bool detected;
        public DetectorPart(int detectionWidth, int detectionHeight, Type type, ITargeting parent) : base(new Sprite())
        {
            BoundBox = new RotRectangle(new Rectangle((int) Position.X, (int) Position.Y, detectionWidth, detectionHeight), angle);
            this.parent = parent;
            this.type = type;
        }

        public override void TakeAction(Type type)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            if (detected && target.IsActive)
                parent.UpdateTarget(target);
        }

        public override void Collision(Collidable c2)
        {
        //    if (c2.GetType() == type)
        if (c2 is Enemy)
            {
                //   target = (Entity)Convert.ChangeType(c2, type);
                target = ((Enemy)c2);
                detected = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}
