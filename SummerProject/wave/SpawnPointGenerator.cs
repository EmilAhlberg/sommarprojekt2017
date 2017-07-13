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
        private int mapOffset = -10; //!       
        private int diagonalSpacing = 40; //!
        private Vector2[] tempPoints;
        private int burstIndex;

        public SpawnPointGenerator(GameMode gameMode)
        {
            this.gameMode = gameMode;
        }    

        public void Update(GameTime gameTime)
        { 
            UpdateMode();                
        }

        private void UpdateMode()
        {
            if (gameMode.IsChanged)
            {
                switch (gameMode.SpawnMode)
                {
                    case GameMode.RANDOM_SINGLE:
                        spawnSize = 1; //move to difficulty ?
                        break;
                    case GameMode.RANDOM_WAVE:
                        spawnSize = GameMode.Level + Difficulty.WAVE_SIZE; //!
                        break;
                    case GameMode.BURST_WAVE:
                        spawnSize = GameMode.Level + Difficulty.BURST_SIZE; //!
                        tempPoints = null;
                        break;
                }
            }
        }

        public Vector2[] GetSpawnPoints()
        {
            Vector2[] vs = new Vector2[spawnSize];
            switch (gameMode.SpawnMode)
            {
                case GameMode.BURST_WAVE:
                    vs = BurstWaveMode();
                    break;
                default:
                    vs = RandomWaveType();
                    break;
            }              
            return vs;
        }

        #region Point methods for wave generation
        private Vector2 SidePoint(int side, float spacing, float x, float y)
        {
            Vector2 v = Vector2.Zero;

            switch (side)
            {
                case 1: //bottom
                    v = new Vector2(x + spacing, WindowSize.Height - mapOffset);
                    break;
                case 2: // top
                    v = new Vector2(x + spacing, mapOffset);
                    break;
                case 3: //left
                    v = new Vector2(mapOffset, y + spacing);
                    break;
                case 4: //right
                    v = new Vector2(WindowSize.Width - mapOffset, y + spacing);
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
                    v = new Vector2(offset, WindowSize.Height + offset);
                    break;
                case 2: // bottom right
                    v = new Vector2(WindowSize.Width + offset, WindowSize.Height - offset);
                    break;
                case 3: //top left
                    v = new Vector2(offset, -offset);
                    break;
                case 4: //top right
                    v = new Vector2(WindowSize.Width + offset, offset);
                    break;
            }
            return v;
        }
        #endregion

        /*
         * MODES:
         * 
         */
        private Vector2[] RandomWaveType()
        {
            Vector2[] vs = new Vector2[spawnSize];

            int waveType = SRandom.Next(0, 9);
            if (waveType >= 8)
                vs = RandomDiagonalWave();
            else if (waveType >= 5)
                vs = RandomSideWave();
            else
            {
                vs = RandomLocation();
            }
            return vs;
        }

        private Vector2[] BurstWaveMode()
        {          
            Vector2[] vs = new Vector2[1];
            if (tempPoints == null || burstIndex >= tempPoints.Length)
            {
                tempPoints = RandomWaveType();
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
         *      RandomLocation: Spawns an enemy on a random location.
         *      
         */

        private Vector2[] RandomSideWave()
        {
            Vector2[] vs = new Vector2[spawnSize];

            int side = SRandom.Next(1, 5);
            float sideLength = 0;
            if (side < 3)
                sideLength = WindowSize.Width;
            else
                sideLength = WindowSize.Height;

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
            int diagonalWaveSize = spawnSize * diagonalSpacing; // alright?
            Vector2[] vs = new Vector2[spawnSize];
            int corner = SRandom.Next(1, 5);
            int offset = -spawnSize/2;
            for (int i = 0; i< spawnSize; i++)
            {
                vs[i] = DiagonalPoint(corner, offset * diagonalWaveSize/spawnSize);
                offset++;
            }
            return vs;                            
        }

        private Vector2[] RandomLocation()
        {
            Vector2[] vs = new Vector2[spawnSize];

            for (int i = 0; i < spawnSize; i++)
            {
                int side = SRandom.Next(1, 5);
                Vector2 v = Vector2.Zero;
                float x = 0;
                float y = 0;

                if (side < 3)
                    x = WindowSize.Width * SRandom.NextFloat();
                else
                    y = WindowSize.Height * SRandom.NextFloat();

                v = SidePoint(side, 0, x, y);
                vs[i] = v;
            }   
            return vs;
        }      
    }
}