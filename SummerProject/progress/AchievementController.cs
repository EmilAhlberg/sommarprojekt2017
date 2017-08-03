using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.achievements
{
    public class AchievementController
    {
        public List<Achievement> Achievements { get; set; }
      
        private SpriteFont font;

        public AchievementController(SpriteFont font)
        {
            this.font = font;
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

            Dictionary<int, Trait> eliteRank = new Dictionary<int, Trait>();
            eliteRank.Add(Traits.SCORETHRESHOLD[Traits.ELITE], Traits.SCORE);
          

            //Dictionary<int, Trait> boss1 = new Dictionary<int, Trait>();
            //boss1.Add(Traits.LEVELTHRESHOLD[Traits.BOSS_SLAIN1], Traits.LEVEL);

            //Dictionary<int, Trait> boss2 = new Dictionary<int, Trait>();
            //boss2.Add(Traits.LEVELTHRESHOLD[Traits.BOSS_SLAIN2], Traits.LEVEL);

            //Dictionary<int, Trait> boss3 = new Dictionary<int, Trait>();
            ////boss3.Add(Traits.LEVELTHRESHOLD[Traits.BOSS_SLAIN3], Traits.LEVEL);

            Dictionary<int, Trait> wave11 = new Dictionary<int, Trait>();
            wave11.Add(1, Traits.LEVEL);

            Dictionary<int, Trait> wave21 = new Dictionary<int, Trait>();
            wave21.Add(1, Traits.LEVEL);


            Achievement normalAch = new Achievement("Normal Difficulty Unlocked", normalDifficulty,Traits.NORMAL_DIFFICULTY);
            Achievement hardAch = new Achievement("Hard Difficulty Unlocked", hardDifficulty, Traits.HARD_DIFFICULTY);
            //Achievement waveAch = new Achievement("Wave Mode", waveMode);
            //Achievement burstAch = new Achievement("Burst Mode", burstMode);
            Achievement wave11Ach = new Achievement("", wave11, Traits.WAVE11);
            Achievement wave21Ach = new Achievement("", wave21, Traits.WAVE21);
            Achievement eliteAch = new Achievement("BIG BOY rank reached!", eliteRank, Traits.ELITE);


            //Achievement boss1Ach = new Achievement("Cyberlord Jorav is no more!", boss1, Traits.BOSS_SLAIN1);
            //Achievement boss2Ach = new Achievement("Colonel Klint bites the dust!", boss2, Traits.BOSS_SLAIN2);         
            //Achievement boss3Ach = new Achievement("Some text here, Big Boss Usker!", boss3, Traits.BOSS_SLAIN3);

            Achievements.Insert(Traits.NORMAL_DIFFICULTY,normalAch); //insert instead of add because of order
            Achievements.Insert(Traits.HARD_DIFFICULTY, hardAch);    //Nooooo no work out of order...
            Achievements.Insert(Traits.WAVE11, wave11Ach);
            Achievements.Insert(Traits.WAVE21, wave21Ach);
            Achievements.Insert(Traits.ELITE, eliteAch);

            //Achievements.Insert(Traits.BOSS_SLAIN1, boss1Ach);
            //Achievements.Insert(Traits.BOSS_SLAIN2, boss2Ach);
            //Achievements.Insert(Traits.BOSS_SLAIN3, boss3Ach);

            //Achievements.Insert(Traits.WAVE_MODE, waveAch);
            //Achievements.Insert(Traits.BURST_MODE, burstAch);
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
                    a.Draw(spriteBatch, gameTime, font); //! font                               
            }
        }

        //??
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
