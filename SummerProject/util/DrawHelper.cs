﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.util
{
    static class DrawHelper
    {
        public static void DrawOutlinedString(this SpriteBatch sb, float outlineSize, Color outlineColor, SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
        {
            sb.DrawString(font, text, position + new Vector2(outlineSize, outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, -outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(outlineSize, -outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(outlineSize, 0), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, -0), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-0, outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(0, -outlineSize), outlineColor, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 1);
        }

        public static void DrawOutlinedString(this SpriteBatch sb, float outlineSize, Color outlineColor, SpriteFont font, string text, Vector2 position, Color color)
        {
            DrawOutlinedString(sb, outlineSize, outlineColor, font, text, position, color, 0, Vector2.Zero, 1);
        }


        public static void DrawExtrudedString(this SpriteBatch sb, float outlineSize, Color outlineColor, SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
        {
            sb.DrawString(font, text, position + new Vector2(outlineSize, -outlineSize), Color.Black, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, outlineSize), Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(outlineSize, outlineSize), Color.Black, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(outlineSize, 0), Color.Black, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-0, outlineSize), Color.Black, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(0, -outlineSize), Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, -0), Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position + new Vector2(-outlineSize, -outlineSize), Color.White, rotation, origin, scale, SpriteEffects.None, 1);
            sb.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 1);
        }

        public static Vector2 CenteredWordPosition(string s, SpriteFont font)
        {
            Vector2 size = font.MeasureString(s);
            float width = 0;
            if (size.X > width)
                width = size.X;
            return new Vector2((WindowSize.Width - width) / 2, (WindowSize.Height) / 2);
        }
    }
}
