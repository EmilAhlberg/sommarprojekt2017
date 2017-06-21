using Microsoft.Xna.Framework;
using SummerProject.collidables;
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
                    if (c1 is AIEntity && c2 is AIEntity)
                    {
                        AIEntity e1 = c1 as AIEntity;
                        AIEntity e2 = c2 as AIEntity;
                        if (e1.IsActive && e2.IsActive)
                        {
                            if (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                                HandleCollision(c1, c2);
                            for (int k = 1; k < c1.BoundBoxes.Count; k++) // assumes index 0 is always what collides with walls
                            {
                                if (c1.BoundBoxes[k].Intersects(c2.BoundBoxes[0]))
                                    HandleDetectionCollision(c1, c2);
                            }
                        }
                    }
                    else
                    {
                        if (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                            HandleCollision(c1, c2);
                        for (int k = 1; k < c1.BoundBoxes.Count; k++) // assumes index 0 is always what collides with walls
                        {
                            if (c1.BoundBoxes[k].Intersects(c2.BoundBoxes[0]))
                                HandleDetectionCollision(c1, c2);
                        }
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
            if (c1.IsStatic)
                MoveObject(c2, c1);
            else if (c2.IsStatic)
                MoveObject(c1, c2);
            c1.Collision(c2);
            c2.Collision(c1);

        }

        private void MoveObject(Collidable c1, Collidable c2) // c1 should be moved 
        {
            Vector2 collidedPos = c1.Position;
            Vector2 backVect;
            backVect = c1.PrevPos - c2.Position;
            backVect.Normalize();
            backVect *= 0.2f;
            float dotProd = Vector2.Dot(backVect, c1.Velocity);
            Vector2 result = dotProd / backVect.LengthSquared() * backVect;
            while (c1.BoundBoxes[0].Intersects(c2.BoundBoxes[0]))
                c1.Position += backVect;
            c1.Velocity -= result;

            //if (c1.BoundBox.Bottom == c2.BoundBox.Top || c1.BoundBox.Top == c2.BoundBox.Bottom)
            //    c1.Position = new Vector2(collidedPos.X, c1.Position.Y);
            //else
            //    c1.Position = new Vector2(c1.Position.X, collidedPos.Y);
        }

        //private void rotatingIntersection(Collidable c1, Collidable c2)
        //{
        //    float axis1x = c1.Position - c2.BoundBox.Width
        //}
    }
}
