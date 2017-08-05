using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.achievements
{
    public class SaveData
    {      
        public int Score;       
        public bool[] unlocks;

        public SaveData()
        {
        }

        public void SaveProgress(AchievementController ac)
        {
            unlocks = new bool[ac.Achievements.Count];
            for (int i = 0; i < ac.Achievements.Count; i++)
            {
                if (ac.Achievements[i].Unlocked)
                    unlocks[i] = true;
            }

            Score = (int)Traits.SCORE.Counter;
            if (ScoreHandler.HighScore > Score)
            {
                Score = ScoreHandler.HighScore;
            }
        }
    }
}
