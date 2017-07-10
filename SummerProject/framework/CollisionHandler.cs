using Microsoft.Xna.Framework;
using SummerProject.collidables;
using SummerProject.collidables.parts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerProject
{
    class CollisionHandler
    {
        public void CheckCollisions(List<Collidable> list1, List<Collidable> list2)
        {
            foreach (Collidable c1 in list1)
            {
                foreach (Collidable c2 in list2)     // can optimize
                {
                    if (c1.BoundBox.Intersects(c2.BoundBox))

                        if (c1 is Part && c2 is Part)
                            HandleCollision((Collidable)((Part)c1).GetController(), (Collidable)((Part)c2).GetController());
                        else
                        if (c1 is Part)
                            HandleCollision((Collidable)((Part)c1).GetController(), c2);
                        else
                        if (c2 is Part)
                            HandleCollision((Collidable)((Part)c2).GetController(), c1);
                        else
                            HandleCollision(c1, c2);

                }
            }

            foreach (Collidable c in list1)
            {
                c.PrevPos = c.Position;
            }
            foreach (Collidable c in list2)
            {
                c.PrevPos = c.Position;
            }
        }

        private void HandleDetectionCollision(Collidable c1, Collidable c2)
        {
            if (!(c1 is Player || c2 is Player || c1 is Wall || c2 is Wall))
                c1.Collision(c2);
        }
        private void HandleCollision(Collidable c1, Collidable c2)
        {
            c1.Collision(c2);
            c2.Collision(c1);
        }

        //private void rotatingIntersection(Collidable c1, Collidable c2)
        //{
        //    float axis1x = c1.Position - c2.BoundBox.Width
        //}
    }
}
