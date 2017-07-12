using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public class Texture2DPlus
    {
        public Texture2D Texture { get; private set; }
        public int Subimages { get; private set; }

        public Texture2DPlus(Texture2D texture, int subimages = 1)
        {
            Texture = texture;
            Subimages = subimages;
        }

        private List<Texture2D> splitTextures;
        public List<Texture2D> SplitTextures
        {
            get
            {
                return splitTextures ?? GetSplitTexture();
            }
        }
        private List<Texture2D> GetSplitTexture()
        {
            int largestSplit = 3;
            if (largestSplit > Texture.Width)
            {
                largestSplit = Texture.Width / 2;
            }
            int pixelSize = 2;
            if (pixelSize > Texture.Width)
            {
                pixelSize = Texture.Width / 2 - 1;
            }

            int width = Texture.Width / Subimages;
            int height = Texture.Height;
            List<Texture2D> texList = new List<Texture2D>();
            Texture2D tex1 = new Texture2D(Texture.GraphicsDevice, width, height);
            Texture2D tex2 = new Texture2D(Texture.GraphicsDevice, width, height);
            Color[] colors = new Color[Texture.Width * Texture.Height];
            Color[] colorsTex1 = new Color[width * height];
            Color[] colorsTex2 = new Color[width * height];
            int breakPoint = SRandom.Next(width / 2 - largestSplit, width / 2 + largestSplit);
            Texture.GetData(colors);
            for (int y = 0; y < height; y++)
            {
                if (y % pixelSize == 0)
                    breakPoint = SRandom.Next(width / 2 - largestSplit, width / 2 + largestSplit);
                for (int x = 0; x < width; x++)
                {
                    if (x < breakPoint)
                    {
                        colorsTex1[x + y * width] = colors[x + y * Texture.Width];
                        colorsTex2[x + y * width] = Color.Transparent;
                    }
                    else
                    {
                        colorsTex1[x + y * width] = Color.Transparent;
                        colorsTex2[x + y * width] = colors[x + y * Texture.Width];
                    }
                }
            }
            tex1.SetData(colorsTex1);
            tex2.SetData(colorsTex2);
            texList.Add(tex1);
            texList.Add(tex2);
            splitTextures = texList;
            return texList;
        }

        /// <summary>
        /// Only works for cardinal directions!
        /// </summary>

        public Texture2D GetRotatedTexture(float rads)
        {
            Texture2D newTex;
            Color[] colors = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors);
            Color[] newColors = new Color[Texture.Width * Texture.Height];

            if (rads == (float)Math.PI / 2)
            {
                newTex = new Texture2D(Texture.GraphicsDevice, Texture.Height, Texture.Width);
                for (int x = 0; x < Texture.Width; x++)
                {
                    for (int y = 0; y < Texture.Height; y++)
                    {
                        newColors[y + x * Texture.Height] = colors[x + y * Texture.Width];
                    }
                }
            }
            else
            if (rads == (float)Math.PI)
            {
                newTex = new Texture2D(Texture.GraphicsDevice, Texture.Width, Texture.Height);
                for (int x = 0; x < Texture.Width; x++)
                {
                    for (int y = 0; y < Texture.Height; y++)
                    {
                        newColors[(Texture.Width - 1) - x + ((Texture.Height - 1) * Texture.Width) - (y * Texture.Width)] = colors[x + y * Texture.Width];
                    }
                }
            }
            else
            if (rads == 3 * (float)Math.PI / 2)
            {
                newTex = new Texture2D(Texture.GraphicsDevice, Texture.Height, Texture.Width);
                for (int x = 0; x < Texture.Width; x++)
                {
                    for (int y = 0; y < Texture.Height; y++)
                    {
                        newColors[(Texture.Height - 1) - y + (Texture.Height * (Texture.Width - 1)) - (x * Texture.Height)] = colors[x + y * Texture.Width];
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

        private Color? primaryColor;
        public Color PrimaryColor { get { return primaryColor ?? CalcPrimaryColor(); } }
        public Color CalcPrimaryColor()
        {
            Color[] colors1D = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors1D);
            Dictionary<Color, int> colorDic = new Dictionary<Color, int>();
            foreach (Color c in colors1D)
            {
                if (c.A != 0 && !c.Equals(new Color(32, 32, 32)))
                {
                    if (!colorDic.Keys.Contains(c))
                        colorDic.Add(c, 0);
                    colorDic[c]++;
                }
            }
            Color returnColor = colorDic.Keys.First();
            foreach (Color c in colorDic.Keys)
            {
                if (colorDic[c] > colorDic[returnColor])
                    returnColor = c;
            }
            primaryColor = returnColor;
            return returnColor;
        }

        private List<Color> colors;
        public List<Color> Colors
        {
            get
            {
                return colors ?? CalcColors();
            }
        }
        private List<Color> CalcColors()
        {
            Color[] colors1D = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors1D);
            colors = colors1D.Where(c => c.A != 0 && !c.Equals(new Color(32, 32, 32))).ToList();
            return colors;
        }

        private Texture2DPlus evilTex;
        public Texture2DPlus EvilTexture
        {
            get { return evilTex ?? CalcEvilTexture(); }
        }
        private Texture2DPlus CalcEvilTexture()
        {
            Color primColor = PrimaryColor;
            Color[] colors1D = new Color[Texture.Height * Texture.Width];
            Texture.GetData(colors1D);
            for(int i = 0; i < colors1D.Length; i++)
            {
                if (colors1D[i].A != 0 && !colors1D[i].Equals(new Color(32, 32, 32)))
                {
                    Color c = colors1D[i];
                    float q = (c.R + c.G + c.B) / 3;
                    colors1D[i] = new Color((byte)q, (byte)0, (byte)0, c.A);
                }

            }
            Texture2D newTex = new Texture2D(Texture.GraphicsDevice, Texture.Width, Texture.Height);
            newTex.SetData(colors1D);
            Texture2DPlus newTexPlus = new Texture2DPlus(newTex, Subimages);
            evilTex = newTexPlus;
            evilTex.evilTex = this; //From my point of view, the Jedi are evil!
            return newTexPlus;
            
        }

        private List<Vector2> edges;
        public List<Vector2> Edges
        {
            get
            {
                return edges ?? CalculateEdges();
            }
        }
        /// <summary>
        /// Slow as fuck, just so that you know. Use only once per sprite, at most.
        /// </summary>
        /// <returns></returns>
        private List<Vector2> CalculateEdges()
        {
            Color[] colors1D = new Color[Texture.Width * Texture.Height];
            Texture.GetData(colors1D);
            Color[,] colors2D = new Color[Texture.Width, Texture.Height];
            for (int x = 0; x < Texture.Width; x++)
                for (int y = 0; y < Texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * Texture.Width];
                }

            List<Vector2> edgeList = new List<Vector2>();
            for (int x = 0; x < Texture.Width; x++)
                for (int y = 0; y < Texture.Height; y++)
                {
                    if (colors2D[x, y].A != 0)
                    {
                        if (x == 0 || x == Texture.Width - 1 || y == 0 || y == Texture.Height - 1)
                        {
                            edgeList.Add(new Vector2(x, y));
                        }
                        else
                            for (int i = -1; i <= 1; i++)
                                for (int j = -1; j <= 1; j++)
                                    if (!(i == j))
                                    {
                                        if (colors2D[x + i, y + j].A == 0)
                                        {
                                            edgeList.Add(new Vector2(x, y));
                                            break;
                                        }
                                    }
                    }
                }

            edges = edgeList;
            return edgeList;
        }

    }
}
