using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;

namespace SummerProject.achievements
{
    public class AchievementController
    {
        public List<Achievement> Achievements { get; set; }

        public AchievementController()
        {            
            Achievements = new List<Achievement>(); //!      
            InitAchievements();
        }

        /*
         * Achievements for dummies:
         *      An achievement is created with a name and a dictionary param. The dictionary has to 
         *      be created in the init method below. The dictionary's keys are specific ints which constitutes thresholds for
         *      the corresponding dictionary value. When all thresholds for an achievement have been reached, the achievement is unlocked.
         *      
         *      Example:
         *          dic.Add(10, Traits.SomeTrait);    <--- when SomeTrait reaches above 10, this specific condition for some achievement is fulfilled.
         *          
         *      
         */
        private void InitAchievements()
        {            
            Dictionary<int, Trait> normalDifficulty = new Dictionary<int, Trait>();
            normalDifficulty.Add(Traits.KILLTHRESHOLD[Traits.NORMAL_DIFFICULTY], Traits.KILLS);
            normalDifficulty.Add(Traits.SCORETHRESHOLD[Traits.NORMAL_DIFFICULTY], Traits.SCORE);                       

            Dictionary<int, Trait> hardDifficulty = new Dictionary<int, Trait>();
            hardDifficulty.Add(Traits.KILLTHRESHOLD[Traits.HARD_DIFFICULTY], Traits.KILLS);
            hardDifficulty.Add(Traits.SCORETHRESHOLD[Traits.HARD_DIFFICULTY], Traits.SCORE);

            //Dictionary<int, Trait> eliteRank = new Dictionary<int, Trait>();
            //eliteRank.Add(Traits.SCORETHRESHOLD[Traits.ELITE], Traits.SCORE);
          
            Dictionary<int, Trait> wave11 = new Dictionary<int, Trait>();
            wave11.Add(1, Traits.LEVEL);

            Dictionary<int, Trait> wave21 = new Dictionary<int, Trait>();
            wave21.Add(1, Traits.LEVEL);

            Dictionary<int, Trait> wave31 = new Dictionary<int, Trait>();
            wave31.Add(1, Traits.LEVEL);

            Dictionary<int, Trait> wave41 = new Dictionary<int, Trait>();
            wave41.Add(1, Traits.LEVEL);


            Achievement normalAch = new Achievement("Normal Difficulty Unlocked", normalDifficulty,Traits.NORMAL_DIFFICULTY);
            Achievement hardAch = new Achievement("Hard Difficulty Unlocked", hardDifficulty, Traits.HARD_DIFFICULTY);        
            Achievement wave11Ach = new Achievement("", wave11, Traits.WAVE11);
            Achievement wave21Ach = new Achievement("", wave21, Traits.WAVE21);
            Achievement wave31Ach = new Achievement("", wave31, Traits.WAVE31);
            Achievement wave41Ach = new Achievement("", wave41, Traits.WAVE41);
            //Achievement eliteAch = new Achievement("BIG BOY rank reached!", eliteRank, Traits.ELITE);

           

            Achievements.Insert(Traits.NORMAL_DIFFICULTY,normalAch); //insert instead of add because of order
            Achievements.Insert(Traits.HARD_DIFFICULTY, hardAch);    //Nooooo no work out of order...
            Achievements.Insert(Traits.WAVE11, wave11Ach);
            Achievements.Insert(Traits.WAVE21, wave21Ach);
            Achievements.Insert(Traits.WAVE31, wave31Ach);
            Achievements.Insert(Traits.WAVE41, wave41Ach);

            //Achievements.Insert(Traits.ELITE, eliteAch);          
        }

        public void Update(GameTime gameTime)
        {
            Traits.SCORE.Counter = ScoreHandler.Score;
            
            foreach(Achievement a in Achievements)
            {
                //dont have to update already unlocked achievements
                a.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Achievement a in Achievements)
            {                                
                    a.Draw(spriteBatch, gameTime, DrawHelper.BIGFONT); //! font                               
            }
        }
       
        public void Reset()
        {
            Traits.TIME.Counter = 0;
            Traits.SCORE.Counter = 0;
            Traits.SHOTSFIRED.Counter = 0;
            Traits.SHOTSHIT.Counter = 0;
            Traits.KILLS.Counter = 0;
            Traits.ENEMIESSPAWNED.Counter = 0;
            Traits.LEVEL.Counter = 0;
        }
    }
}
