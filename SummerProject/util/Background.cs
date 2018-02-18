using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhysicsSimulator;

namespace SummerProject.util
{
    class Background
    {
        private Sprite bkg;
        private AnimatedBackgroundObject bluePlanet;
        private AnimatedBackgroundObject moon;
        private AnimatedBackgroundObject bigRed;
        private AnimatedBackgroundObject smallRed;

        private const int boundOffsetForSize = 750;
        private const float speedMultiplier = 0.3f; 

        private Vector2 blueStartingPos = new Vector2((WindowSize.Width * 1) / 8, (WindowSize.Height * 1) / 8);
        private Vector2 blueDirection = new Vector2(-1, 0.08f);
        private const float blueSpeed = 0.2f * speedMultiplier;
        private Vector2 blueRespawnPos = new Vector2(WindowSize.Width + boundOffsetForSize, 0 );         // remember height will be offset due to boundOffsetForSize
        private const int blueRespawnDelay = (int) (50 /speedMultiplier);                

        private Vector2 bigRedStartingPos = new Vector2(WindowSize.Width + boundOffsetForSize, WindowSize.Height / 4  );
        private Vector2 bigRedDirection = new Vector2(-1, -0.1f);
        private float bigRedSpeed = 0.5f * speedMultiplier;
        private const int bigRedRespawnDelay = (int)(35 / speedMultiplier);

        private Vector2 smallRedStartingPos = new Vector2((WindowSize.Width * 2)/3, (WindowSize.Height * 2f) /6);
        private Vector2 smallRedDirection = new Vector2(-1, 0.04f);
        private float smallRedSpeed = 0.1f * speedMultiplier;
        private Vector2 smallRespawnPos = new Vector2(WindowSize.Width + boundOffsetForSize, (WindowSize.Height * 2f) / 7.3f);
        private const int smallRedRespawnDelay = (int)(10 / speedMultiplier);

        public Background (Sprite bkg, Texture2DPlus bluePlanet, Texture2DPlus moon, Texture2DPlus bigRed, Texture2DPlus smallRed)
        {
            this.bkg = bkg;
            this.bluePlanet = new AnimatedBackgroundObject(new Sprite(bluePlanet), blueStartingPos, blueDirection, blueSpeed, blueRespawnDelay);
          //  this.moon = new AnimatedBackgroundObject(new Sprite(moon), blueStartingPos, blueDirection, blueSpeed);
            this.bigRed = new AnimatedBackgroundObject(new Sprite(bigRed), bigRedStartingPos, bigRedDirection, bigRedSpeed, bigRedRespawnDelay);
            this.smallRed = new AnimatedBackgroundObject(new Sprite(smallRed), smallRedStartingPos, smallRedDirection, smallRedSpeed, smallRedRespawnDelay);
            this.bluePlanet.RespawnPoint = blueRespawnPos;
            this.smallRed.RespawnPoint = smallRespawnPos;

        }

        public void Update(GameTime gameTime)
        {
            bluePlanet.Update(gameTime);
            bigRed.Update(gameTime);
            smallRed.Update(gameTime);
        }

        public void Draw(SpriteBatch sb, GameTime gt)  // order important!
        {
            bkg.Draw(sb, gt);
            smallRed.Draw(sb, gt);
            bluePlanet.Draw(sb, gt);
            bigRed.Draw(sb, gt);

        }

    }
}
