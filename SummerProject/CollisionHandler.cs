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
        public void CheckCollisions(Collidable[] list1, params Collidable[] list2)
        {
            Collidable[] list = list1.Concat(list2).ToArray();
            foreach (Collidable c1 in list)
            {
                foreach (Collidable c2 in list)
                {
                    if (c1 != c2)
                        if (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
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
            else if (c1.IsStatic)
                MoveObject(c1, c2);
            c1.Collision(c2);
            c2.Collision(c1);
        }

        private void MoveObject(Collidable c1, Collidable c2) // c1 should be moved 
        {
            Vector2 collidedPos = c1.Position;
            Vector2 backVect = c1.PrevPos - collidedPos;
            backVect.Normalize();
            backVect *= 0.2f;
            while (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                c1.Position += backVect;
            if (c1.BoundBoxes[0].Bottom == c2.BoundBoxes[0].Top || c1.BoundBoxes[0].Top == c2.BoundBoxes[0].Bottom)
                c1.Position = new Vector2(collidedPos.X, c1.Position.Y);
            else
                c1.Position = new Vector2(c1.Position.X, collidedPos.Y);
        }

        //private void rotatingIntersection(Collidable c1, Collidable c2)
        //{
        //    float axis1x = c1.Position - c2.BoundBox.Width
        //}
    }
}
