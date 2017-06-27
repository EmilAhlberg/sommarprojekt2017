using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class DropSpawnPoints
    {
        private Random rand = new Random();
        public static List<Vector2> points = new List<Vector2>();
        

        public static void DeathAt(Vector2 source)
        {
            points.Add(source);
        }

        //improvement needed
        public Vector2[] SpawnPositions()
        {
            for(int i =points.Count-1; i>=0; i--)
            {
                if(rand.Next(0,6) <5)
                {
                    points.RemoveAt(i);                    
                }
            }

            Vector2[] vs = new Vector2[points.Count];
                for (int i = vs.Length-1; i >=0; i--)
                {                  
                   vs[i] = points[i];
                   points.RemoveAt(i);
                }

           
            return vs;            
        }

    }
}
