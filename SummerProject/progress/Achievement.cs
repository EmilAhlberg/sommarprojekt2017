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
    public class Achievement
    {
        public bool Unlocked { get; private set; }
        private String name;
        private Dictionary<int, Trait> traits;
        private Timer unlockTimer = new Timer(3);//!

        public Achievement(String name, Dictionary<int, Trait> traits)
        {
            this.name = name;
            this.traits = traits;
        }

        public void Update(GameTime gameTime)
        {
            CheckUnlockCondition();
            if (Unlocked)
            {
                unlockTimer.CountDown(gameTime);
            }          
        }

        public void AlreadyUnlocked()
        {
            Unlocked = true;
            unlockTimer = new Timer(0);
            unlockTimer.CountDown(new GameTime());
        }

        private void CheckUnlockCondition()
        {
            var itemsToRemove = traits.Keys.ToArray();
            bool unlocked = false; ;
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
            if (!Unlocked)
                Unlocked = unlocked;     
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font) 
        {
            if (Unlocked && !unlockTimer.IsFinished)
            {
                string s = name;
                spriteBatch.DrawString(font, s, DrawHelper.CenteredWordPosition(s, font) + new Vector2(0,-200), Color.PapayaWhip); //! vector + font
            }            
        }      
    }
}
