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

        /// <summary>
        /// Only works for cardinal directions!
        /// </summary>

        public static Texture2D GetRotatedTexture(this Texture2D tex, float rads)
        {
            Texture2D newTex;
            Color[] colors = new Color[tex.Width * tex.Height];
            tex.GetData(colors);
            Color[] newColors = new Color[tex.Width * tex.Height];

            if (rads == (float)Math.PI / 2)
            {
                newTex = new Texture2D(tex.GraphicsDevice, tex.Height, tex.Width);
                for (int x = 0; x < tex.Width; x++)
                {
                    for (int y = 0; y < tex.Height; y++)
                    {
                        newColors[y + x * tex.Height] = colors[x + y * tex.Width];
                    }
                }
            }
            else
            if (rads == (float)Math.PI)
            {
                newTex = new Texture2D(tex.GraphicsDevice, tex.Width, tex.Height);
                for (int x = 0; x < tex.Width; x++)
                {
                    for (int y = 0; y < tex.Height; y++)
                    {
                        newColors[(tex.Width-1)-x + ((tex.Height - 1) * tex.Width) - (y * tex.Width)] = colors[x + y * tex.Width];
                    }
                }
            }
            else
            if (rads == 3*(float)Math.PI / 2)
            {
                newTex = new Texture2D(tex.GraphicsDevice, tex.Height, tex.Width);
                for (int x = 0; x < tex.Width; x++)
                {
                    for (int y = 0; y < tex.Height; y++)
                    {
                        newColors[(tex.Height-1)-y + (tex.Height*(tex.Width-1))-(x * tex.Height)] = colors[x + y * tex.Width];
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
            newTex.SetData(newColors);
            return newTex;
        }
    }
}
