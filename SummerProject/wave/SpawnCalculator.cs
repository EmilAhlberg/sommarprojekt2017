using Microsoft.Xna.Framework;
using SummerProject.wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class SpawnCalculator
    {

        public bool SpawnIsReady { get; internal set; }    

        private SpawnPointGenerator spawnPointGen;
        private SpawnTimer spawnTimer;            

        public SpawnCalculator(int mode, int windowWidth, int windowHeight)
        {
            spawnPointGen = new SpawnPointGenerator(windowWidth, windowHeight);
            spawnTimer = new SpawnTimer(mode);
        }

        public void SetGameMode(int modeType)
        {
            spawnTimer.ChangeMode(modeType);      
            spawnPointGen.ChangeMode(modeType);    
        }

        public Vector2[] GetSpawnPoints()
        {
            return spawnPointGen.GetSpawnPoints();
        }
        
        public void JustSpawned()
        {            
            spawnTimer.JustSpawned();
        }

        public void Update(GameTime gameTime)
        {
            SpawnIsReady = spawnTimer.Update(gameTime);
            spawnPointGen.Update(gameTime);      
        }
    }
}
