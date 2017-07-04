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
            Achievements = new List<Achievement>();      
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

            Dictionary<int, Trait> waveMode = new Dictionary<int, Trait>();
            waveMode.Add(Traits.TIMETHRESHOLD[Traits.WAVE_MODE], Traits.TIME);

            Dictionary<int, Trait> burstMode = new Dictionary<int, Trait>();
            burstMode.Add(Traits.LEVELTHRESHOLD[Traits.BURST_MODE], Traits.LEVEL);

            Achievement normalAch = new Achievement("Normal Difficulty", normalDifficulty);
            Achievement hardAch = new Achievement("Hard Difficulty", hardDifficulty);
            Achievement waveAch = new Achievement("Wave Mode", waveMode);
            Achievement burstAch = new Achievement("Burst Mode", burstMode);

            Achievements.Insert(Traits.NORMAL_DIFFICULTY,normalAch); //insert instead of add because of order 
            Achievements.Insert(Traits.HARD_DIFFICULTY, hardAch);
            Achievements.Insert(Traits.WAVE_MODE, waveAch);
            Achievements.Insert(Traits.BURST_MODE, burstAch);
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
