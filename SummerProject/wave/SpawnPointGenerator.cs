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
        private GameMode gameMode;
        private int spawnSize;       
        private int windowWidth;
        private int windowHeight;
        private int mapOffset = -10; //!       
        private int diagonalWaveSize = 600; //!
        private int oldMode;
        private Vector2[] tempPoints;
        private int burstIndex;

        public SpawnPointGenerator(GameMode gameMode)
        {
            this.windowWidth = WindowSize.Width;
            this.windowHeight = WindowSize.Height;
            this.gameMode = gameMode;
            oldMode = gameMode.SpawnMode;

        }

        public void UpdateMode()
        {
            if (oldMode != gameMode.SpawnMode)
            {

                switch (gameMode.SpawnMode)
                {
                    case GameMode.RANDOM_SINGLE:
                        spawnSize = 1; //! 
                        break;
                    case GameMode.RANDOM_WAVE:
                        spawnSize = 2; //!                    
                        break;
                }
                oldMode = gameMode.SpawnMode;
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateMode();
            ChangeLevel();                
        }

        private void ChangeLevel()
        {
            if (gameMode.ChangeLevel)
            {

                switch (gameMode.SpawnMode)
                {
                    case GameMode.RANDOM_WAVE:
                        spawnSize = gameMode.Level + 1;
                        break;
                    //case GameMode.BURST_TIME: //!
                    //    spawnSize = 15;
                    //    break;
                }
            }
        }

        public Vector2[] GetSpawnPoints()
        {
            Vector2[] vs = new Vector2[spawnSize];

            //vs = RandomWaveMode();
            vs = BurstWaveMode();

            

            return vs;
        }

        private Vector2 SidePoint(int side, float spacing, float x, float y)
        {
            Vector2 v = Vector2.Zero;          

            switch (side)
            {
                case 1: //bottom
                    v = new Vector2(x + spacing, windowHeight - mapOffset);
                    break;
                case 2: // top
                    v = new Vector2(x + spacing, mapOffset) ;
                    break;
                case 3: //left
                    v = new Vector2(mapOffset, y + spacing) ;
                    break;
                case 4: //right
                    v = new Vector2(windowWidth - mapOffset, y + spacing);
                    break;
            }
            return v;
        }

        private Vector2 DiagonalPoint(int corner, int offset)
        {
            Vector2 v = Vector2.Zero;
            switch (corner)
            {
                case 1: //bottom left
                    v = new Vector2(offset, windowHeight + offset);
                    break;
                case 2: // bottom right
                    v = new Vector2(windowWidth + offset, windowHeight-offset);
                    break;
                case 3: //top left
                    v = new Vector2(offset, -offset);
                    break;
                case 4: //top right
                    v = new Vector2(windowWidth + offset, offset);
                    break;
            }
            return v;
        }

        /*
         * MODES:
         * 
         */
        private Vector2[] RandomWaveMode()
        {
            Vector2[] vs = new Vector2[spawnSize];

            int waveType = SRandom.Next(0, 9);
            if (waveType >= 8)
                vs = RandomDiagonalWave();
            else if (waveType >= 5)
                vs = RandomSideWave();
            else
            {
                vs = RandomOffMapLocation();
            }
            return vs;
        }

        private Vector2[] BurstWaveMode()
        {
            spawnSize = 13;
            Vector2[] vs = new Vector2[1];
            if (tempPoints == null || burstIndex >= tempPoints.Length)
            {
                tempPoints = RandomSideWave(); //should be changed
                burstIndex = 0;
            }

            vs[0] = tempPoints[burstIndex];
            burstIndex++;

            return vs;
        }






        /*
         * WaveTypes:
         *      RandomSideWave: Spaces enemies evenly on a randomly selected side.
         *      RandomDiagonalWave: Spaces enemies evenly across the DiagonalWaveSize, corner is randomly selected.
         *      RandomOffMapLocation: Spawns an enemy on a random location, just outside the map.
         *      
         */

        private Vector2[] RandomSideWave()
        {
            Vector2[] vs = new Vector2[spawnSize];

            int side = SRandom.Next(1, 5);
            float sideLength = 0;
            if (side < 3)
                sideLength = windowWidth;
            else
                sideLength = windowHeight;

            float gapLength = (sideLength / (float)spawnSize);
            float sum = gapLength/2;

            for (int i = 0; i < spawnSize; i++)
            {
                vs[i] = SidePoint(side, sum, 0, 0);
                sum += gapLength;            
            }
            return vs;                       
        }
              
      
        private Vector2[] RandomDiagonalWave()
        {
            Vector2[] vs = new Vector2[spawnSize];
            int corner = SRandom.Next(1, 5);
            int offset = -spawnSize/2;
            for (int i = 0; i< spawnSize; i++)
            {
                vs[i] = DiagonalPoint(corner, offset* diagonalWaveSize/spawnSize);
                offset++;
            }
            return vs;                            
        }

        private Vector2[] RandomOffMapLocation()
        {
            Vector2[] vs = new Vector2[spawnSize];

            for (int i = 0; i < spawnSize; i++)
            {
                int side = SRandom.Next(1, 5);
                Vector2 v = Vector2.Zero;
                float x = 0;
                float y = 0;

                if (side < 3)
                    x = windowWidth * SRandom.NextFloat();
                else
                    y = windowHeight * SRandom.NextFloat();

                v = SidePoint(side, 0, x, y);
                vs[i] = v;
            }   
            return vs;
        }      
    }




}