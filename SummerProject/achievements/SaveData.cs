using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.achievements
{
    public class SaveData
    {
        //public string fileName;
        //public int level;
        //public int kills;
        //public int score;
        //public int shotsFired;
        //public int shotsHit;
        //public int enemiesSpawned;
        //public float timeElapsed;
        public bool[] unlocks;

        public SaveData()
        {
            //fileName = "file"; //!
        }

        public void SaveProgress(AchievementController ac)
        {
            unlocks = new bool[ac.Achievements.Count];
            for (int i = 0; i < ac.Achievements.Count; i++)
            {
                if (ac.Achievements[i].Unlocked)
                    unlocks[i] = true;
            }

            //level = (int)Traits.LevelTrait.Counter;
            //kills = (int)Traits.KillTrait.Counter;
            //score = (int)Traits.ScoreTrait.Counter;
            //shotsFired = (int)Traits.ShotsFiredTrait.Counter;
            //shotsHit = (int)Traits.ShotsHitTrait.Counter;
            //enemiesSpawned = (int)Traits.EnemiesSpawnedTrait.Counter;
            //timeElapsed = Traits.TimeTrait.Counter;


        }
        //load precis fixad
        //public bool[] LoadProgress(AchievementController ac)
        //{


        //    //Traits.LevelTrait.Counter = level;
        //    //Traits.KillTrait.Counter = kills;
        //    //Traits.ScoreTrait.Counter = score;
        //    //Traits.ShotsFiredTrait.Counter = shotsFired;
        //    //Traits.ShotsHitTrait.Counter = shotsHit;
        //    //Traits.EnemiesSpawnedTrait.Counter = enemiesSpawned;
        //    //Traits.TimeTrait.Counter = timeElapsed;


        //}

    }
}
