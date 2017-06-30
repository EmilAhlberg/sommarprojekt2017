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
        public List<Achievement> achievements;
        private SpriteFont font;    

        public AchievementController(SpriteFont font)
        {
            this.font = font;
            achievements = new List<Achievement>();
            InitAchievements();
        }

        //different achievements and their links to traits are added here
        private void InitAchievements()
        {            
            Dictionary<int, Trait> normalMode = new Dictionary<int, Trait>();
            normalMode.Add(Traits.KILLTHRESHOLD[Traits.NORMAL], Traits.KillTrait);
            normalMode.Add(Traits.SCORETHRESHOLD[Traits.NORMAL], Traits.ScoreTrait);

            Achievement normalAch = new Achievement("NormalMode", normalMode);

            Dictionary<int, Trait> hardMode = new Dictionary<int, Trait> ();
            hardMode.Add(Traits.KILLTHRESHOLD[Traits.HARD], Traits.KillTrait);
            hardMode.Add(Traits.SCORETHRESHOLD[Traits.HARD], Traits.ScoreTrait);
            Achievement hardAch = new Achievement("HardMode", hardMode);

            achievements.Add(normalAch);
            achievements.Add(hardAch);
        }

        public void Update(GameTime gameTime)
        {
            Traits.ScoreTrait.Counter = ScoreHandler.Score;
            
            foreach(Achievement a in achievements)
            {
                a.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Achievement a in achievements)
            {                                
                    a.Draw(spriteBatch, gameTime, font); //! font
                               
            }
        }
    }
}
