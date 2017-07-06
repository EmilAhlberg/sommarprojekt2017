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
        private List<Texture2D> upgradeParts;
        private float spentResource;
        private float resource;
        private List<ShipItem> itemBoxes;

        public bool Active { get; internal set; }
        public bool Action { get; internal set; }
        public Part SelectedPart { get; internal set; }

        public UpgradeBar(List<Texture2D> upgradeParts, SpriteFont font)
        {
            this.upgradeParts = upgradeParts;
            this.font = font;
            this.spentResource = 0;
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
                Rectangle background = new Rectangle(0, 0, 200, upgradeParts.Count * 100); //hard coded #1
                spriteBatch.Draw(upgradeParts[0], background, Color.SaddleBrown);

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
                foreach (ShipItem shipItem in itemBoxes)
                    if (shipItem.BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
                    {
                        Action = true;
                        SelectedPart = shipItem.ReturnPart();
                    }
            }
        }
        internal void CreateItemBoxes()
        {

            itemBoxes = new List<ShipItem>();
            Rectangle background = new Rectangle(0, 0, 200, upgradeParts.Count * 100); //hard coded #2
            //float width = background.Width;
            //float height = background.Height;         
            //int boxHeight = (int)height / upgradeParts.Count;
            for (int i = 0; i < upgradeParts.Count; i++)
            {
                Sprite s = new Sprite(upgradeParts[i]);
                float height = s.SpriteRect.Height*ShipItem.SCALEFACTOR;
                int boxHeight = (int)height / upgradeParts.Count;
                itemBoxes.Insert(i, new ShipItem(new Vector2 (background.X, background.Y + boxHeight * i), s, 0, i));
            }
        }
    }
}
