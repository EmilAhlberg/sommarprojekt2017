using Microsoft.Xna.Framework;
using SummerProject.collidables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerProject
{
    class CollisionHandler
    {
        public void CheckCollisions(Collidable[] list1, params Collidable[] list2)
        {
            Collidable[] list = list1.Concat(list2).ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length; j++)     // can optimize
                {
                    Collidable c1 = list[i];
                    Collidable c2 = list[j];
                    if (c1 == c2)
                        continue;
                    if (c1 is ActivatableEntity && c2 is ActivatableEntity)
                    {
                        ActivatableEntity e1 = c1 as ActivatableEntity;
                        ActivatableEntity e2 = c2 as ActivatableEntity;
                        if (e1.IsActive && e2.IsActive) //! PLS REFACTOR ;)
                        {
                            if (e1 is PartController && e2 is PartController)
                            {
                                PartController pc1 = c1 as PartController;
                                PartController pc2 = c2 as PartController;
                                foreach (Part p1 in pc1.Parts)
                                    foreach (Part p2 in pc2.Parts)
                                    {
                                        if (p1.BoundBox.Intersects(p2.BoundBox))
                                        {
                                            HandleCollision(c1, c2);
                                        }
                                    }
                            }
                            else if (e1 is PartController)
                            {
                                PartController pc1 = c1 as PartController;
                                foreach (Part p1 in pc1.Parts)
                                {
                                    if (p1.BoundBox.Intersects(c2.BoundBox))
                                    {
                                        HandleCollision(c1, c2);
                                    }
                                }
                            }
                            else if (e2 is PartController)
                            {
                                PartController pc2 = c2 as PartController;
                                foreach (Part p2 in pc2.Parts)
                                {
                                    if (c1.BoundBox.Intersects(p2.BoundBox))
                                    {
                                        HandleCollision(c1, c2);
                                    }
                                }
                            }
                            else
                                if (c1.BoundBox.Intersects(c2.BoundBox))
                            {
                                HandleCollision(c1, c2);
                            }
                        }

                        //else
                        //{
                        //    if (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                        //        HandleCollision(c1, c2);
                        //    for (int k = 1; k < c1.BoundBoxes.Count; k++) // assumes index 0 is always what collides with walls
                        //    {
                        //        if (c1.BoundBoxes[k].Intersects(c2.BoundBoxes[0]))
                        //            HandleDetectionCollision(c1, c2);
                        //    }
                        //}
                    }
                }
            }
            foreach (Collidable c in list)
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
