using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public interface IActivatable
    {
        bool IsActive { get; set; }
        bool IsBusy { get; set; }
        void Activate(Vector2 source, Vector2 target);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Death();
    }
}
