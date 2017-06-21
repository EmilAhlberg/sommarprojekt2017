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
        private int windowWidth;
        private int windowHeight;
        private int mapOffset = 50; //!

        public SpawnPointGenerator(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            rand = new Random();
            //spawnPoints = new Vector2[8]; //!!
            ////!!
            //spawnPoints[0] = new Vector2(-50, -50);     // Top left
            //spawnPoints[1] = new Vector2(windowWidth + 50, -50);  // top right
            //spawnPoints[2] = new Vector2(-50, windowHeight + 50); // bottom left
            //spawnPoints[3] = new Vector2(windowWidth + 50, windowHeight + 50); // bottom right
            //spawnPoints[4] = new Vector2(windowWidth + 50, windowHeight / 2); // right  (bugged)
            //spawnPoints[5] = new Vector2(-50, windowHeight / 2); // left      (bugged)
            //spawnPoints[6] = new Vector2(windowWidth / 2, windowHeight + 50); // bottom
            //spawnPoints[7] = new Vector2(windowWidth / 2, -50); // top
        }

        public void ChangeMode(int modeType)
        {
            switch(modeType)
            {
                case WaveGenerator.INCREASING_PRESSURE:
                    spawnSize = 1; //! DEFAULT
                    break;
                case WaveGenerator.WAVESPAWN_MODE:
                    spawnSize = 5; //! DEFAULT
                    break;
            }
            mode = modeType;
        }

        public void Update(GameTime gameTime, int spawnSize)
        {
            this.spawnSize = spawnSize;
        }

        public Vector2[] GetSpawnPoints()
        {
            Vector2[] vs = new Vector2[spawnSize];
            //bool[] takenPoint = new bool[8]; //!!!    8 :: the number of predefined spawnPoints
            //for (int i = 0; i<spawnSize; i++)
            //{
            //    int rNbr = rand.Next(0, 8); //!!
            //    //gets unique points, avoids 'overlap spawn'
            //    while (takenPoint[rNbr])
            //        rNbr = (rNbr + 3) % 8; //!!!!!! nbr: x= '3' cant be a divider of y = '8'
            //    takenPoint[rNbr] = true;
            //    vs[i] = spawnPoints[rNbr];
            //}



            for (int i = 0; i<spawnSize; i++)
            {
                vs[i] = RandomOffMapLocation();
            }

            return vs;
        }

        private Vector2 RandomOffMapLocation()
        {
            int side = rand.Next(1, 5); 
            Vector2 v = Vector2.Zero;            
            float x = 0 ;
            float y = 0;

            if(side <3)            
                x = windowWidth * (float)rand.NextDouble();               
             else       
                y = windowHeight * (float)rand.NextDouble();     

            switch (side)
            {
                case 1: //bottom
                    v = new Vector2(x, windowHeight + mapOffset);
                    break;
                case 2: // top
                    v = new Vector2(x, -mapOffset);
                    break;
                case 3: //left
                    v = new Vector2(-mapOffset, y);
                    break;
                case 4: //right
                    v = new Vector2(windowWidth + mapOffset, y);
                    break;
            }


            return v;
        }
    }
}
