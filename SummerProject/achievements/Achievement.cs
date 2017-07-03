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

        private void CheckUnlockCondition()
        {
            var itemsToRemove = traits.Keys.ToArray();
            foreach (int i in itemsToRemove) {
                Trait trait = traits[i];

                if (trait.CheckCondition(i))
                {
                    Unlocked = true;
                    traits.Remove(i); //remove trait to avoid redundant checks?  i tink så
                }
                else
                {
                    Unlocked = false;
                    break;
                }                    
            }            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {
            if (Unlocked && !unlockTimer.IsFinished)
            {
                spriteBatch.DrawString(font, name + " unlocked!", new Vector2(400, 400), Color.PapayaWhip);
            }            
        }
    }
}
