using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.util;
using SummerProject.achievements;

namespace SummerProject.events.buildmenu
{
    class UpgradeBar
    {
        private SpriteFont font;
        private List<Texture2D> upgradeParts;
        private float spentResource;
        private float resource;

        public UpgradeBar(List<Texture2D> upgradeParts, SpriteFont font)
        {
            this.upgradeParts = upgradeParts;
            this.font = font;
            this.spentResource = 0;
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //currency           
            string word = "Currency: " + resource;
            spriteBatch.DrawOutlinedString(3, new Color(32, 32, 32), font, word,
                                        DrawHelper.CenteredWordPosition(word, font) + new Vector2(0, -300), Color.AntiqueWhite); //! vector
        }

        internal void Update(GameTime gameTime)
        {
            float resource = Traits.SCORE.Counter - spentResource; //not here
        }

        internal void CreateItemBoxes()
        {
            throw new NotImplementedException();
        }
    }
}
