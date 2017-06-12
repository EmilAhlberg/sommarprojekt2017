using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class CollisionHandler
    {
        public void CheckCollisions(params Collidable[] list)
        {
            foreach (Collidable c1 in list)
            {
                foreach (Collidable c2 in list)
                {
                    if (c1 != c2)
                        if (c1.BoundBox.Intersects(c2.BoundBox))
                            HandleCollision(c1, c2);
                }
            }
            foreach (Collidable c in list)
            {
                c.PrevPos = c.Position;
            }
        }

        private void HandleCollision(Collidable c1, Collidable c2)
        {
            if (c1.IsStatic)
                MoveObject(c2, c1);
            else
                MoveObject(c1, c2);
        }
        private void MoveObject(Collidable c1, Collidable c2) // c1 should be moved 
        {
            Vector2 backVect = c1.PrevPos - c1.Position;
            backVect.Normalize();
            backVect *= 0.2f;
            while (c1.BoundBox.Intersects(c2.BoundBox))
            {
                c1.Position += backVect;
                c1.BoundBox = new Rectangle((int)Math.Round(c1.Position.X), (int)Math.Round(c1.Position.Y), c1.BoundBox.Width, c1.BoundBox.Height);
            }
            
        }
    }
}
