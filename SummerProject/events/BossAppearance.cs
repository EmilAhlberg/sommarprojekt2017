using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.events
{
    public class BossAppearance : Drawable
    {
        private double oldTime = -1000; //! 
        private Vector2 startingPosition = new Vector2(WindowSize.Width, 100); //!

        public BossAppearance() : base(Vector2.Zero, IDs.DEFAULT)
        {
   
        }



        public void Update (GameTime gameTime, float dX)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds > oldTime + AnimatedEventHandler.BOSSAPPEARANCE_TIME)
            {
                NewAppearance();
                oldTime = gameTime.ElapsedGameTime.TotalSeconds;
            }          
            Position = Position - new Vector2(dX, 0);
        }

        private void NewAppearance()
        {
            Position = startingPosition;
            id = IDs.HOMINGBULLET; //!
        }
    }
}
