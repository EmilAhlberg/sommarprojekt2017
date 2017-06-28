using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.wave
{
    public class SpawnPointGenerator
    {

        private Random rand;
        private Vector2[] spawnPoints;
        private int mode;
        private int spawnSize;

        public SpawnPointGenerator(int windowWidth, int windowHeight)
        {
            rand = new Random();
            spawnPoints = new Vector2[8];

            spawnPoints[0] = new Vector2(-50, -50);     // Top left
            spawnPoints[1] = new Vector2(windowWidth + 50, -50);  // top right
            spawnPoints[2] = new Vector2(-50, windowHeight + 50); // bottom left
            spawnPoints[3] = new Vector2(windowWidth + 50, windowHeight + 50); // bottom right
            spawnPoints[4] = new Vector2(windowWidth + 50, windowHeight / 2); // right  (bugged)
            spawnPoints[5] = new Vector2(-50, windowHeight / 2); // left      (bugged)
            spawnPoints[6] = new Vector2(windowWidth / 2, windowHeight + 50); // bottom
            spawnPoints[7] = new Vector2(windowWidth / 2, -50); // top
        }

        public void ChangeMode(int modeType)
        {
            switch(modeType)
            {
                //case WaveGenerator.INCREASING_PRESSURE:
                //    spawnSize = 1;
                //    break;
                //case WaveGenerator.WAVESPAWN_MODE:
                //    spawnSize = 5;
                //    break;
            }
            mode = modeType;
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public Vector2[] GetSpawnPoints()
        {
            
            bool[] takenPoint = new bool[8]; //!!!    8 :: the number of predefined spawnPoints
            Vector2[] vs = new Vector2[spawnSize];
            for (int i = 0; i<spawnSize; i++)
            {
                int rNbr = (int)rand.Next(0, 8);//!!
                //gets unique points, avoids 'overlap spawn'
                while (takenPoint[rNbr])
                    rNbr = (rNbr + 3) % 8; //!!!!!!
                takenPoint[rNbr] = true;
                vs[i] = spawnPoints[rNbr];
            }
            return vs;
        }

    }
}
