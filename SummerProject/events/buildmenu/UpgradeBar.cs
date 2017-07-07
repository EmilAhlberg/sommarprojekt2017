using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.factories;

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

        public UpgradeBar(List<IDs> upgradePartsIDs, SpriteFont font)
        {
            this.upgradePartsIDs = upgradePartsIDs;
            this.font = font;
            this.spentResource = 0;
            Rectangle background = new Rectangle(0, 0, WindowSize.Height, upgradePartsIDs.Count/nbrOfItems * itemOffset);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
            {
                //currency           
                string word = "Currency: " + resource;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word,
                                            DrawHelper.CenteredWordPosition(word, font) + new Vector2(0, -300), Color.AntiqueWhite); //! vector

                //menubar     
              //  Texture2D outlineBkg = new Texture2D()     
                Sprite bkgSprite = SpriteHandler.GetSprite((int)IDs.UPGRADEBAR);
                bkgSprite.Scale = new Vector2(ClickableItem.SCALEFACTOR * 32, WindowSize.Height/3); // original sprite is 3x3  //! hardcoded width
                bkgSprite.Draw(spriteBatch,gameTime);


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
            //! 6 items per column
            //!
            int boxHeight = (WindowSize.Height - itemOffset) / nbrOfItems ; 
            Vector2 tempVect = new Vector2(itemOffset, itemOffset);
            for (int i = 0; i < upgradePartsIDs.Count; i++)
            {
                UpgradeBarItem si = new UpgradeBarItem (tempVect + new Vector2((i/nbrOfItems) * itemOffset, i * boxHeight - i/nbrOfItems * (WindowSize.Height- itemOffset)), upgradePartsIDs[i]);
                itemBoxes.Insert(i, si);
                //tempVect.Y += 175;
                //  itemBoxes.Insert(i, new ShipItem(new Vector2 (background.X, background.Y + boxHeight * i), s, 0, i));
            }
        }
    }
}
