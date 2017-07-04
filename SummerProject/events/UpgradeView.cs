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
using SummerProject.collidables.parts;

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
        private RectangularHull hull;
        private List<Texture2D> upgradeParts;

        public UpgradeView(Texture2D text, SpriteFont font, Player player, List<Texture2D> upgradeParts)
        {
            activeSelection = -1;
            this.font = font;
            this.text = text;
            this.upgradeParts = upgradeParts;
            this.player = player;
            itemBoxes = new List<Rectangle>();
            slotSize = 200;
            spentResource = 0;
            totalResource = 0;
            ShitInit();
        }

        private void ShitInit()
        {
            int widthGap = (WindowSize.Width - slotSize);
            slotBoxes = new List<Rectangle>();
            slotBoxes.Add(new Rectangle((WindowSize.Width - slotSize) / 2, (WindowSize.Height - slotSize) / 2, slotSize, slotSize)); //always hull at 0

            List<Part> parts = player.Parts;
            Point center = slotBoxes[0].Center;
            for (int i = 1; i < parts.Count; i++)
            {
                if (parts[i] is RectangularHull)
                {
                    Vector2 v = LinkPosition(i % 4, center);
                    slotBoxes.Add(new Rectangle((int)v.X, (int)v.Y, slotSize, slotSize));
                }
                else if (parts[i] is GunPart)
                {
                    Vector2 v = LinkPosition(i % 4, center);
                    slotBoxes.Add(new Rectangle((int)v.X, (int)v.Y, slotSize, slotSize));
                }
            }
          
            if (hull is RectangularHull) { }              
            
        }

        private Vector2 LinkPosition(int pos, Point center)
        {
            switch (pos)
            {
                case 0:
                    return new Vector2(center.X + slotSize / 2, center.Y - slotSize / 2);
                    break;
                case 1:
                    return new Vector2(center.X - slotSize / 2, center.Y + slotSize / 2);
                    break;
                case 2:
                    return new Vector2(center.X - slotSize / 2, center.Y - slotSize / 2);
                    break;
                case 3:
                    return new Vector2(center.X + slotSize / 2, center.Y - slotSize);
                    break;                
            }
            return Vector2.Zero;
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
            int boxWidth = (int)width / upgradeParts.Count;
            for (int i = 0; i < upgradeParts.Count; i++)
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
            for (int i = 0; i < slotBoxes.Count; i++)
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


            for (int i = 0; i < upgradeParts.Count; i++)
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
            Rectangle activeSlot = new Rectangle(0, 100, 200, 200);//slotBoxes[activeSelection];
            return new Rectangle(activeSlot.Location.X, activeSlot.Location.Y + slotSize, slotSize, slotSize / 2);
            //return new Rectangle(activeSlot.Location.X, activeSlot.Location.Y + size, size + frameAddition, size / 2 + frameAddition);
        }
    }
}
