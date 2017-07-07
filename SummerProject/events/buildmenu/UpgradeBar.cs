using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using SummerProject.achievements;

namespace SummerProject.events.buildmenu
{
    class UpgradeBar
    {
        private SpriteFont font;
        private List<IDs> upgradePartsIDs;
        private float spentResource;
        private float resource;
        private List<UpgradeBarItem> itemBoxes;

        public bool Active { get; internal set; }
        public bool Action { get; internal set; }
        public Part SelectedPart { get; internal set; }
        private Rectangle background;
        private int nbrOfItems = 5;
        private int itemOffset = (int)ClickableItem.Width;
        private Texture2D backgroundText;

        public UpgradeBar(List<IDs> upgradePartsIDs, SpriteFont font, Texture2D backgroundText)
        {
            this.upgradePartsIDs = upgradePartsIDs;
            this.font = font;
            this.backgroundText = backgroundText;
            this.spentResource = 0;
            background = new Rectangle(0, 0, (upgradePartsIDs.Count / nbrOfItems +2) * itemOffset,  WindowSize.Height);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
            {
                //menubar                            
                spriteBatch.Draw(backgroundText, background, Color.SaddleBrown);

                //currency           
                string word = "Currency: " + resource;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word,
                                            DrawHelper.CenteredWordPosition(word, font, 
                                            new Vector2(itemOffset + (int)(((float)(upgradePartsIDs.Count/nbrOfItems) - 0.5) * (float)itemOffset), itemOffset/3)), 
                                            Color.AntiqueWhite); 

            

                for (int i = 0; i < itemBoxes.Count; i++)
                {
                    itemBoxes[i].Draw(spriteBatch, gameTime);
                }
            }
          
        }

        internal void Update(GameTime gameTime)
        {
            Action = false;
            CheckAction();
            float resource = Traits.SCORE.Counter - spentResource; //not here         
        }

        private void CheckAction()
        {
            if (itemBoxes != null)
            {
                foreach (UpgradeBarItem barItem in itemBoxes)
                    if (barItem.BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
                    {
                        barItem.Active = true;
                        Action = true;
                        SelectedPart = barItem.ReturnPart();
                    }
            }
        }
        internal void CreateItemBoxes()
        {
            itemBoxes = new List<UpgradeBarItem>();          
            int boxHeight = (WindowSize.Height - itemOffset) / nbrOfItems ; 
            Vector2 tempVect = new Vector2(itemOffset, itemOffset);
            for (int i = 0; i < upgradePartsIDs.Count; i++)
            {
                UpgradeBarItem si = new UpgradeBarItem (tempVect + new Vector2((i/nbrOfItems) * itemOffset, i * boxHeight - i/nbrOfItems * (WindowSize.Height- itemOffset)), upgradePartsIDs[i]);
                itemBoxes.Insert(i, si);
            }
        }
    }
}
