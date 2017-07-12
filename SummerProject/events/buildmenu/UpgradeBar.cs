﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.factories;
using Microsoft.Xna.Framework.Input;

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
        private int nbrOfItems = 5;
        private int itemOffset = (int)ClickableItem.Width;
        private Texture2D backgroundText;
        private Sprite upgradeBarBkg;
        private Sprite outlineBkg;
        private Sprite screenBkg;
        private bool screenBkgMoved;
        private Sprite followMouseSprite;
        #region Bar item positioning
        private const int rows = 5;
        private const int spacing = 50;
        private const int offsetX = 60;
        private const int offsetY = 150;
        #endregion

        public UpgradeBar(List<IDs> upgradePartsIDs, SpriteFont font, Texture2D backgroundText)
        {
            this.upgradePartsIDs = upgradePartsIDs;
            this.font = font;
            this.backgroundText = backgroundText;
            this.spentResource = 0;
            InitBackgrounds();
        }

        private void InitBackgrounds()
        {
            upgradeBarBkg = SpriteHandler.GetSprite((int)IDs.UPGRADEBAR);
            screenBkg = SpriteHandler.GetSprite((int)IDs.MENUSCREENBKG);
            outlineBkg = SpriteHandler.GetSprite((int)IDs.UPGRADEBAR);
            outlineBkg.MColor = Color.DarkGray;
            outlineBkg.Position = new Vector2(((upgradePartsIDs.Count / nbrOfItems + 2) * itemOffset), 0);
            int yScaleFactorForBkg = WindowSize.Height / 3;                     // original sprite is 1x3
            outlineBkg.Scale = new Vector2(4, yScaleFactorForBkg);
            screenBkg.Scale = new Vector2(WindowSize.Width, WindowSize.Height);
            upgradeBarBkg.Scale = new Vector2(((upgradePartsIDs.Count / nbrOfItems + 2) * itemOffset), yScaleFactorForBkg);
            upgradeBarBkg.LayerDepth = 1; // background should be in background
            screenBkg.LayerDepth = 0;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            screenBkg.Draw(spriteBatch, gameTime);
            if (Active)
            {
                //menubar     
                //  Texture2D outlineBkg = new Texture2D()     
                upgradeBarBkg.Draw(spriteBatch, gameTime);
                outlineBkg.Draw(spriteBatch, gameTime);
                //currency           
                string word = "Currency: " + resource;
                spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word,
                                            DrawHelper.CenteredWordPosition(word, font,
                                            new Vector2(itemOffset + (int)(((float)(upgradePartsIDs.Count / nbrOfItems) - 0.5) * (float)itemOffset), itemOffset / 3)),
                                            Color.Gold);

                for (int i = 0; i < itemBoxes.Count; i++)
                {
                    itemBoxes[i].Draw(spriteBatch, gameTime);
                }
                if (Action)
                followMouseSprite.Draw(spriteBatch, gameTime);
            }
        }

        internal void Update(GameTime gameTime)
        {
       //     Action = false; // lol just removed this and now has dragable build item
            CheckAction();
            resource = Traits.CURRENCY.Counter; //not here      
            if (Action)
                followMouseSprite.Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
        }

        private void CheckAction()
        {
            if (itemBoxes != null)
            {
                foreach (UpgradeBarItem barItem in itemBoxes)
                {
                    if (barItem.BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
                    {
                        barItem.Active = true;
                        Action = true;
                        followMouseSprite = new Sprite(barItem.Sprite);
                        followMouseSprite.Scale *= ClickableItem.SCALEFACTOR;
                        SelectedPart = barItem.ReturnPart();
                        foreach (UpgradeBarItem otherItem in itemBoxes)
                            if (otherItem != barItem)
                                otherItem.Active = false;
                        break;
                    }
                }
            }
        }

        public void ClearStuckOnMouseItem ()
        {
            Action = false;
        }

        internal void CreateItemBoxes()
        {
            itemBoxes = new List<UpgradeBarItem>();
            int cols = (int) Math.Ceiling((double)(itemBoxes.Count / rows));
            int currentCol = 0;
            int scaleFactor = 32 * ClickableItem.SCALEFACTOR;
            for (int i = 0; i < upgradePartsIDs.Count; i++)
            {
                if (i % (rows) == 0 && i != 0)
                    currentCol++;
                UpgradeBarItem u = new  UpgradeBarItem( new Vector2(currentCol * scaleFactor + spacing*(currentCol+1) + offsetX, ((i-currentCol*(rows))*scaleFactor + spacing * (i - currentCol * (rows) ) + offsetY)), font, upgradePartsIDs[i]);
                itemBoxes.Insert(i, u);
            }
        }
    }
}
