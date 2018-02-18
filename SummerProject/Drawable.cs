using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.factories;

namespace SummerProject
{
    public class Drawable
    {
        public virtual Vector2 Position { get; set; }
        public Sprite Sprite { get;  set; }
        protected float angle = 0;
        protected IDs id;
        private string team;
        protected string Team { get { return team; } set { team = value; Sprite.Team = team; } }

        public Drawable(Vector2 position, IDs id = IDs.DEFAULT) // : base(sprite.spriteRect.Width, sprite.spriteRect.Height)
        {
            if (id == IDs.DEFAULT)
                id = EntityConstants.TypeToID(GetType());
            this.id = id;
            SetStats(id);
        }

        public virtual void SetStats(IDs id)
        {
            Sprite = SpriteHandler.GetSprite((int)id);
            Team = EntityConstants.GetStatsFromID(EntityConstants.TEAM, id);
            if (Sprite is Sprite)
                Sprite.Origin = new Vector2(Sprite.SpriteRect.Width / 2, Sprite.SpriteRect.Height / 2);
        }

        public virtual void ChangeSprite(Sprite sprite)
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
            //Color c = Color.White;
            //Microsoft.Xna.Framework.Graphics.DrawLine(new Pen(c),new Point((int)Position.X,(int)Position.Y), new Point((int)Position.X+(int)sprite.Origin.X, (int)Position.Y+(int)sprite.Origin.Y));
        }
    }
}
