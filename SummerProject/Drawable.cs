using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.factories;

namespace SummerProject
{
    public class Drawable
    {
        public virtual Vector2 Position { get; set; }
        public ISprite Sprite { get;  set; }
        protected float angle = 0;
        private float prevAngle;

        public Drawable(Vector2 position, IDs id = IDs.DEFAULT) // : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            if (id == IDs.DEFAULT)
                id = EntityConstants.TypeToID(GetType());
            Sprite = SpriteHandler.GetSprite((int) id);
            prevAngle = angle;
            if (Sprite is Sprite)
                Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2);
        }

        public virtual void ChangeSprite(ISprite sprite)
        {
            //Sprite = sprite;
            //if (sprite is Sprite)
            //    sprite.Origin = new Vector2(sprite.SpriteRect.Width / 2, sprite.SpriteRect.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Position = Position;
            //if (prevAngle != angle) // for changing sprite rotation independently (asteroid)
            Sprite.Rotation = angle;
            Sprite.Draw(spriteBatch, gameTime);
            prevAngle = angle;
            //Color c = Color.White;
            //Microsoft.Xna.Framework.Graphics.DrawLine(new Pen(c),new Point((int)Position.X,(int)Position.Y), new Point((int)Position.X+(int)sprite.Origin.X, (int)Position.Y+(int)sprite.Origin.Y));
        }
    }
}
