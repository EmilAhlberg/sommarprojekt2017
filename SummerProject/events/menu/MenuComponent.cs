namespace SummerProject.menu
{
    using SummerProject;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using util;

    public abstract class MenuComponent
    {
        private string[] MenuItems;
        public SpriteFont Font { get; private set; }
        private static readonly Color normal = Color.Gold;
        private static readonly Color hilite = Color.OrangeRed;
        private static readonly Color pressed = Color.DarkRed;
        private float width;
        private float height;
        protected int pressedIndex;
        public Vector2 Position { get; private set; }

        public MenuComponent(Vector2 position, SpriteFont spriteFont, string[] menuItems)
        {
            pressedIndex = int.MaxValue;
            this.MenuItems = menuItems;
            this.Font = spriteFont;
            MeasureMenu();
            this.Position = position;
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in MenuItems)
            {
                Vector2 size = Font.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += Font.LineSpacing;
            }
        }

        public abstract int HandleSelection(int currentMenu, int selectedIndex, EventOperator handler);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, int selectedIndex)
        {
            Vector2 location = Position;
            Color tint;
            float scale;
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (i == pressedIndex)
                    tint = pressed;
                else if (i == selectedIndex)
                    tint = hilite;
                    scale = 1.1f;
                }
                else
                {
                    tint = normal;
                    scale = 1;
                }
                DrawHelper.DrawOutlinedString(spriteBatch, 3, new Color(32, 32, 32), Font, MenuItems[i], location, tint, 0, Font.MeasureString(MenuItems[i]) /2, scale);
                location.Y += Font.LineSpacing + 5;
            }
        }
    }
}