using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.framework
{
    public class PlayerUI
    {
        private UnitBar energyBar;
        private UnitBar hpBar;

        public PlayerUI(UnitBar hpBar, UnitBar energyBar)
        {
            this.hpBar = hpBar;
            this.energyBar = energyBar;

        }


        public void Update(GameTime gameTime, Player player)
        {
            hpBar.Update(player.Health, player.maxHealth);
            energyBar.Update(player.Energy, player.maxEnergy);
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
          

            //if (eventOperator.GameState != EventOperator.SPLASH_SCREEN_STATE)
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "FPS: " + (int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(50, WindowSize.Height - 50), Color.Gold);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "Score: " + ScoreHandler.Score, new Vector2(WindowSize.Width - 300, 50), Color.Gold);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "High Score: " + ScoreHandler.HighScore, new Vector2(WindowSize.Width / 2 - DrawHelper.SCOREFONT.MeasureString("High Score: " + ScoreHandler.HighScore).X / 2, 50), Color.Gold);
        }

        public void Reset()
        {
            hpBar.Reset();
            energyBar.Reset();
        }

        internal void DrawBars(SpriteBatch spriteBatch, GameTime gameTime)
        {
            hpBar.Draw(spriteBatch, gameTime);
            energyBar.Draw(spriteBatch, gameTime);
        }
    }
}
