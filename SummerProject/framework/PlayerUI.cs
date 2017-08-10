using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.achievements;
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
        private AchievementController achController;

        public PlayerUI(UnitBar hpBar, UnitBar energyBar, AchievementController achController)
        {
            this.hpBar = hpBar;
            this.energyBar = energyBar;
            this.achController = achController;

        }


        public void Update(GameTime gameTime, Player player)
        {
            hpBar.Update(player.Health, player.maxHealth);
            energyBar.Update(player.Energy, player.maxEnergy);
            achController.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            achController.Draw(spriteBatch, gameTime);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "FPS: " + (int)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds), new Vector2(50, WindowSize.Height - 50), Color.Gold);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "Score: " + ScoreHandler.Score, new Vector2(WindowSize.Width - 300, 50), Color.Gold);
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), DrawHelper.SCOREFONT, "High Score: " + ScoreHandler.HighScore, new Vector2(WindowSize.Width / 2 - DrawHelper.SCOREFONT.MeasureString("High Score: " + ScoreHandler.HighScore).X / 2, 50), Color.Gold);
        }

        public void Reset()
        {
            hpBar.Reset();
            energyBar.Reset();
            achController.Reset();
        }

        public void DrawBars(SpriteBatch spriteBatch, GameTime gameTime)
        {
            hpBar.Draw(spriteBatch, gameTime);
            energyBar.Draw(spriteBatch, gameTime);
        }
    }
}
