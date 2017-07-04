using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.util;
using SummerProject.achievements;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private Color markedColor = Color.Beige;
        private Color defaultColor = Color.Wheat;
        private int slotSize;
        private Texture2D text;     
        private List<Rectangle> slotBoxes;
        private List<Rectangle> itemBoxes;
        private int totalResource;
        private int spentResource;
        private int activeSelection;
        private SpriteFont font;
        private Player player;
        private Texture2D[] upgradeParts;

        public UpgradeView(Texture2D text, SpriteFont font, Player player, Texture2D[] upgradeParts)
        {
            activeSelection = -1;
            this.font = font;
            this.text = text;
            this.upgradeParts = upgradeParts;
            itemBoxes = new List<Rectangle>();
            slotSize = 200;
            spentResource = 0;
            totalResource = 0;
            ShitInit();
        }

        private void ShitInit()
        {
            int counter = 6; //! # of slots
            int widthGap = (WindowSize.Width - slotSize) / counter;

            slotBoxes = new List<Rectangle>();
            for (int i = 0; i < counter; i++)
            {
                slotBoxes.Add(new Rectangle(slotSize / 2 + widthGap * i, slotSize, slotSize, slotSize));
            }       
        }

        public void Update(GameTime gameTime)
        {
            totalResource = (int)Traits.SCORE.Counter; //?
            CheckActions();
        }

        private void CheckActions()
        {
            for (int i = 0; i < slotBoxes.Count; i++)
            {
                if (slotBoxes[i].Contains(InputHandler.mPosition)&& InputHandler.isJustPressed(MouseButton.LEFT))
                {
                    int oldActive = activeSelection;
                    activeSelection = i;
                    if (oldActive != i && activeSelection >= 0)
                        CreateItemBoxes();
                    else if (oldActive == activeSelection)
                        activeSelection = -1;   
                    //Buy(100); //!
                }
            }        

        }

        private void CreateItemBoxes()
        {
            itemBoxes = new List<Rectangle>();
            Rectangle background = CreateSubFrame();
            float width = background.Width;
            float height = background.Height;
            int boxWidth = (int)width / upgradeParts.Length;
            for (int i = 0; i < upgradeParts.Length; i++)
            {
                itemBoxes.Insert(i, new Rectangle(i*boxWidth + background.X, background.Y, boxWidth, boxWidth));
            }



        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //currency
            float resource = Traits.SCORE.Counter - spentResource;
            string word = "Currency: " + resource;
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word,
                                        DrawHelper.CenteredWordPosition(word, font) + new Vector2(0, 200), Color.AntiqueWhite); //! vector
            //slots         
            for (int i = 0; i < 6; i++)
            {
                if (i == activeSelection)                   
                    spriteBatch.Draw(text, slotBoxes[i], markedColor);
                else
                    spriteBatch.Draw(text, slotBoxes[i], defaultColor);
            }
            //submenu
            if (activeSelection >= 0)
                DrawSelection(spriteBatch);
        }

        private void DrawSelection(SpriteBatch spriteBatch)
        {
            Rectangle background = CreateSubFrame();
            spriteBatch.Draw(text, background, Color.White);


            for (int i = 0; i < upgradeParts.Length; i++)
            {
                spriteBatch.Draw(upgradeParts[i], itemBoxes[i], Color.Blue);
            }

        }

        private void Buy(int price)
        {
            if (totalResource - spentResource >= price)
            {
                spentResource += price;
                //player.dosomething typ addpart(location)
            }
        }

        private Rectangle CreateSubFrame()
        {
            int frameAddition = 20;
            Rectangle activeSlot = slotBoxes[activeSelection];
            return new Rectangle(activeSlot.Location.X, activeSlot.Location.Y + slotSize, slotSize, slotSize / 2);
            //return new Rectangle(activeSlot.Location.X, activeSlot.Location.Y + size, size + frameAddition, size / 2 + frameAddition);
        }
    }
}
