using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public interface ISprite
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Rectangle SpriteRect { get; set; }
        Vector2 Origin { get; set; }
        float Scale { get; set; }

        void Draw(SpriteBatch sb, GameTime gameTime);
        void Animate(GameTime gameTime);
    }
}
