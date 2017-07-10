namespace SummerProject.menu
{
    using SummerProject;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using util;
    using System;

    public abstract class MenuComponent
    {
        protected string[] menuItems;
        public SpriteFont Font { get; private set; }
        private static readonly Color normal = Color.Gold;
        private static readonly Color hilite = Color.OrangeRed;
        private static readonly Color pressed = Color.DarkRed;
        private float width;
        private float height;
        protected bool[] isLocked;
        protected int pressedIndex;        
        public Vector2 Position { get; private set; }

        public MenuComponent(Vector2 position, SpriteFont spriteFont, string[] menuItems)
        {
            pressedIndex = int.MaxValue;          
            this.menuItems = menuItems;
            this.Font = spriteFont;
            MeasureMenu();
            if (menuItems[1] == "Reset Creation") //HACK!
                this.Position = position + new Vector2(WindowSize.Width / 3, WindowSize.Height / 3);
            else
                this.Position = position - new Vector2(0, height)/2;
            SetLockedItems();
        }

        protected abstract void SetLockedItems();
        public abstract void UpdateUnlocks(EventOperator handler);
    

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = Font.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += Font.LineSpacing;
            }
            height -= Font.LineSpacing; //! why
        }

        public abstract int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, int selectedIndex)
        {
            Vector2 location = Position;
            Color tint;
            float scale = 0;
            for (int i = 0; i < menuItems.Length; i++)
            {
                #region Handle selected/pressed menuitem
                if (i == pressedIndex)
                {
                    tint = pressed;
                    scale = 1;
                }
                else if (i == selectedIndex && !isLocked[i])
                {
                    tint = hilite;
                    scale = 1.1f;
                }
                else
                {
                    tint = normal;
                    scale = 1;
                }
                #endregion               
                if(isLocked[i])
                    spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), Font, menuItems[i], location, Color.DarkGray, 0, Font.MeasureString(menuItems[i]) / 2, scale);
                else
                    spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), Font, menuItems[i], location, tint, 0, Font.MeasureString(menuItems[i]) / 2, scale);
               
                location.Y += Font.LineSpacing ;
            }
        }      
    }
}

