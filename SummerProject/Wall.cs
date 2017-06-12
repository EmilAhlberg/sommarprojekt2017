using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Wall 
    {
        private Vector2 position;
        private float angle = 0;
        private Sprite sprite;

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.position = position;
            sprite.rotation = angle;
            sprite.Draw(spriteBatch);
        }
    }
}
