using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.util
{
    public static class TextureHelper
    {
        public static List<Texture2D> GetSplitTexture(this Texture2D tex, int subimages)
        {
            int largestSplit = 3;
            if(largestSplit > tex.Width)
            {
                largestSplit = tex.Width / 2;
            }
            int pixelSize = 2;
            if (pixelSize > tex.Width)
            {
                pixelSize = tex.Width / 2-1;
            }

            int width = tex.Width / subimages;
            int height = tex.Height;
            List<Texture2D> texList = new List<Texture2D>();
            Texture2D tex1 = new Texture2D(tex.GraphicsDevice, width, height);
            Texture2D tex2 = new Texture2D(tex.GraphicsDevice, width, height);
            Color[] colors = new Color[tex.Width * tex.Height];
            Color[] colorsTex1 = new Color[width * height];
            Color[] colorsTex2 = new Color[width * height];
            int breakPoint = SRandom.Next(width / 2 - largestSplit, width / 2 + largestSplit);
            tex.GetData(colors);
            for (int y = 0; y < height; y++) {
                if(y % pixelSize == 0)
                    breakPoint = SRandom.Next(width / 2 - largestSplit, width / 2 + largestSplit);
                for (int x = 0; x < width; x++)
                {
                    if (x < breakPoint)
                    {
                        colorsTex1[x + y * width] = colors[x + y * tex.Width];
                        colorsTex2[x + y * width] = Color.Transparent;
                    }
                    else
                    {
                        colorsTex1[x + y * width] = Color.Transparent;
                        colorsTex2[x + y * width] = colors[x + y * tex.Width];
                    }
                }
            }
            tex1.SetData(colorsTex1);
            tex2.SetData(colorsTex2);
            texList.Add(tex1);
            texList.Add(tex2);
            return texList;
        }
    }
}
