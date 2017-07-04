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
        private int size;
        private Texture2D text;
        private List<Color> colors;
        private List<Rectangle> slotBoxes;
        private List<Rectangle> itemBoxes;
        private int totalResource;
        private int spentResource;
        private SpriteFont font;
        private Player player;
        private Texture2D[] upgradeParts;

        public UpgradeView(Texture2D text, SpriteFont font, Player player, Texture2D[] upgradeParts)
        {
            this.font = font;
            this.text = text;
            this.upgradeParts = upgradeParts;
            size = 200;
            spentResource = 0;
            totalResource = 0;
            ShitInit();
        }

        private void ShitInit()
        {           
            int counter = 6; //! # of slots
            int widthGap = (WindowSize.Width - size) / counter;
           
            slotBoxes = new List<Rectangle>();
            for (int i = 0; i< counter; i++)
            {
               slotBoxes.Add(new Rectangle(size/2 + widthGap*i, size, size, size));
            }
            colors = new List<Color>();
            for (int i = 0; i<6; i++)
            {
                colors.Add(Color.White);
            }

            //upgradeParts (items)
            itemBoxes = new List<Rectangle>();
            int heightGap = (WindowSize.Height/ 2) / upgradeParts.Length;
            for (int i = 0; i< upgradeParts.Length; i++)
            {
                itemBoxes.Add(new Rectangle(WindowSize.Width-size, WindowSize.Height/2 + i*heightGap, size/2, size/2));
            }


        }

        public void Update(GameTime gameTime)
        {
            totalResource = (int)Traits.SCORE.Counter; //?
            CheckActions();
         

        }

        private void CheckActions()
        {
            for (int i = 0; i < 6; i++)
            {
                if (slotBoxes[i].Contains(InputHandler.mPosition))
                {
                    colors[i] = Color.Black;
                    if (InputHandler.isJustPressed(MouseButton.LEFT))
                        Buy(100); //!
                }


                else
                    colors[i] = Color.White;
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //currency
            float resource = Traits.SCORE.Counter - spentResource;
            string word = "Currency: " + resource;
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word, DrawHelper.CenteredWordPosition(word, font) + new Vector2(0,200), Color.AntiqueWhite); //! vector
            //slots
            for (int i = 0; i < 6; i++)
            {
                spriteBatch.Draw(text, slotBoxes[i], colors[i]);
            }
            //items
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
    }
}
