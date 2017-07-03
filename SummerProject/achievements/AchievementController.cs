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

        //different achievements and their links to traits are added here
        private void InitAchievements()
        {            
            Dictionary<int, Trait> normalDifficulty = new Dictionary<int, Trait>();
            normalDifficulty.Add(Traits.KILLTHRESHOLD[Traits.NORMAL_DIFFICULTY], Traits.KillTrait);
            normalDifficulty.Add(Traits.SCORETHRESHOLD[Traits.NORMAL_DIFFICULTY], Traits.ScoreTrait);

           

            Dictionary<int, Trait> hardDifficulty = new Dictionary<int, Trait>();
            hardDifficulty.Add(Traits.KILLTHRESHOLD[Traits.HARD_DIFFICULTY], Traits.KillTrait);
            hardDifficulty.Add(Traits.SCORETHRESHOLD[Traits.HARD_DIFFICULTY], Traits.ScoreTrait);

            Dictionary<int, Trait> waveMode = new Dictionary<int, Trait>();
            waveMode.Add(Traits.TIMETHRESHOLD[Traits.WAVE_MODE], Traits.TimeTrait);

            Dictionary<int, Trait> burstMode = new Dictionary<int, Trait>();
            burstMode.Add(Traits.LEVELTHRESHOLD[Traits.BURST_MODE], Traits.LevelTrait);

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
            Traits.ScoreTrait.Counter = ScoreHandler.Score;
            
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
            //Traits.ShotsFiredTrait.Counter = 0;
            //Traits.ShotsHitTrait.Counter = 0;
            //Traits.KillTrait.Counter = 0;
            //Traits.EnemiesSpawnedTrait.Counter = 0;
        }
    }
}
