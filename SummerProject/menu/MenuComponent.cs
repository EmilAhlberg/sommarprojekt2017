namespace SummerProject.menu
{
    using SummerProject;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class MenuComponent
    {
        private string[] MenuItems;
        public SpriteFont Font { get; private set; }
        private static readonly Color normal = Color.Gold;
        private static readonly Color hilite = Color.OrangeRed;
        private float width;
        private float height;
        public Vector2 Position { get; private set; }

        public MenuComponent(Vector2 position, SpriteFont spriteFont, string[] menuItems)
        {
            this.MenuItems = menuItems;
            this.Font = spriteFont;
            MeasureMenu();
            this.Position = position - (new Vector2(width, height)) / 2;
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
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = hilite;
                else
                    tint = normal;
                spriteBatch.DrawString(Font, MenuItems[i], location, tint);
                location.Y += Font.LineSpacing + 5;
            }
        }
    }
}