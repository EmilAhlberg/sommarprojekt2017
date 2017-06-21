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
            //throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public Vector2 GetSpawnPoint()
        {
            return spawnPoints[(int)(rand.NextDouble() * 8)];
        }

    }
}
