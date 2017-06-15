﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;


    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
        {
            private string[] menuItems;
            private int selectedIndex;
            private Game1 game;
            private Color normal = Color.White;
            private Color hilite = Color.Yellow;          
            private KeyboardState keyboardState;
            private KeyboardState oldKeyboardState;
            private SpriteBatch spriteBatch;
            private SpriteFont spriteFont;
            private Vector2 position;
            private float width = 0f; //!
            private float height = 0f; //!

            public MenuComponent(Game1 game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
               : base(game)
            {
                this.spriteBatch = spriteBatch;
                this.spriteFont = spriteFont;
                this.game = game; 
                this.menuItems = menuItems;
                MeasureMenu();
            }

            public int SelectedIndex
            {
                get { return selectedIndex; }
                set
                {
                    selectedIndex = value;
                    if (selectedIndex < 0)
                        selectedIndex = 0;
                    if (selectedIndex >= menuItems.Length)
                        selectedIndex = menuItems.Length - 1;
                }
            }

            private void MeasureMenu()
            {
                height = 0;
                width = 0;
                foreach (string item in menuItems)
                {
                    Vector2 size = spriteFont.MeasureString(item);
                    if (size.X > width)
                        width = size.X;
                    height += spriteFont.LineSpacing + 5;
                }
                position = new Vector2((Game.Window.ClientBounds.Width - width) / 2, (Game.Window.ClientBounds.Height - height) / 2);
            }

            public override void Initialize()
            {
                base.Initialize();
            }

            private bool CheckKey(Keys theKey)
            {
                return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
            }

            public override void Update(GameTime gameTime)
            {
                keyboardState = Keyboard.GetState();
                if (CheckKey(Keys.Down))
                {
                    selectedIndex++;
                    selectedIndex %= menuItems.Length;                 
                }
                if (CheckKey(Keys.Up))
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                        selectedIndex = menuItems.Length - 1;
                }
                if (CheckKey(Keys.Enter))
                {
                HandleSelection();
            }

            base.Update(gameTime);
                oldKeyboardState = keyboardState;
            }

        private void HandleSelection()
        {
            switch (selectedIndex)
            {
                case 0:
                    game.GameState = 2;
                    break;
                case 1:
                    break;
                case 2:
                    game.Exit();
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
            {
                base.Draw(gameTime);
                Vector2 location = position;
                Color tint;
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                        tint = hilite;
                    else
                        tint = normal;
                    spriteBatch.DrawString(spriteFont, menuItems[i], location, tint);
                    location.Y += spriteFont.LineSpacing + 5;
                }
            }
        }    
}