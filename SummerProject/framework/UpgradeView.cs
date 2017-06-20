using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private Texture2D text;

        private List<Color> colors;
        private List<Rectangle> rects;
    
        private Color color;

        public UpgradeView(Texture2D text)
        {
            this.text = text;
            ShitInit();
        }

        private void ShitInit()
        {
            rects = new List<Rectangle>();
            Rectangle r1 = new Rectangle(150, 150, 200, 200);
            Rectangle r2 = new Rectangle(150, 1000, 200, 200);
            Rectangle r3 = new Rectangle(900, 150, 200, 200);
            Rectangle r4 = new Rectangle(900, 1000, 200, 200);
            Rectangle r5 = new Rectangle(1400, 150, 200, 200);
            Rectangle r6 = new Rectangle(1400, 1000, 200, 200);
            rects.Add(r1);
            rects.Add(r2);
            rects.Add(r3);
            rects.Add(r4);
            rects.Add(r5);
            rects.Add(r6);

            colors = new List<Color>();
            for (int i = 0; i<6; i++)
            {
                colors.Add(Color.White);
            }
        }

        internal void Update(GameTime gameTime)
        {
            for (int i = 0; i<6; i++)
            {
                if (rects[i].Contains(InputHandler.mPosition))
                    colors[i] = Color.Black;
                else
                    colors[i] = Color.White;
            }            
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < 6; i++)
            {
                spriteBatch.Draw(text, rects[i], colors[i]);
            }           
        }
    }
}
