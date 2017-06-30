using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.achievements
{
    public class Achievement
    {
        private bool unlocked;
        private String name;
        private Dictionary<int, Trait> traits;
        private Timer unlockTimer = new Timer(3);//!

        public bool HasChanged { get; internal set; }

        public Achievement(String name, Dictionary<int, Trait> traits)
        {
            this.name = name;
            this.traits = traits;
        }

        public void Update(GameTime gameTime)
        {
            CheckUnlockCondition();
            if (unlocked)
            {
                unlockTimer.CountDown(gameTime);
            }
                //trigger Achievement unlock                
        }

        private void CheckUnlockCondition()
        {
            var itemsToRemove = traits.Keys.ToArray();
            foreach (int i in itemsToRemove) {
                Trait trait = traits[i];

                if (trait.CheckCondition(i))
                {
                    unlocked = true;
                    traits.Remove(i); //remove trait to avoid redundant checks?  i tink så
                }
                else
                {
                    unlocked = false;
                    break;
                }
                    
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            if (unlocked && !unlockTimer.IsFinished)
            {
                spriteBatch.DrawString(font, "unlock", new Vector2(400, 400), Color.PapayaWhip);
            }            
        }
    }
}
