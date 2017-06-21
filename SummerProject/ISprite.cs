using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SummerProject
{
    public interface ISprite
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Rectangle SpriteRect { get; set; }
        Vector2 Origin { get; set; }
        float Scale { get; set; }
        Color MColor { get; set; }
        void Draw(SpriteBatch sb, GameTime gameTime);
        void Animate(GameTime gameTime);
        List<Vector2> CalculateEdges();
    }
}
