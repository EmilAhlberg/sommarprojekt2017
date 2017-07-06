using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerProject.collidables;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.collidables.parts;
using SummerProject.events.buildmenu;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private List<ShipItem> shipItems;
        private List<Texture2D> textures;          
        private int activeSelection;
        private Player player;
        private List<Texture2D> upgradeParts;
        private UpgradeBar upgradeBar;

        public UpgradeView(Texture2D text, SpriteFont font, Player player, List<Texture2D> upgradeParts)
        {
            activeSelection = -1;
            this.upgradeParts = upgradeParts;
            this.player = player;
            upgradeBar = new UpgradeBar(upgradeParts, font);
        }

        internal void Initialize()
        {
            if (shipItems == null)
            {
                shipItems = new List<ShipItem>();
                textures = new List<Texture2D>();
                List<Part> parts = player.Parts;                               
                int activeBoxIndex = 0;
                Sprite s = new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]);      
                shipItems.Add(new ShipItem(new Vector2(WindowSize.Width / 2, WindowSize.Height / 2), s, 0)); //not 100% centered
                RectangularHull currentHull = (RectangularHull)parts[0];

                for (int i = 1; i < parts.Count; i++)
                {
                    Part currentPart = parts[i];
                    RectangularHull carrier = (RectangularHull) currentPart.Carrier;
                    if (currentHull != carrier)
                    {
                        activeBoxIndex = parts.IndexOf(carrier);
                        currentHull = carrier;
                    }
                    Vector2 itemPos =shipItems[activeBoxIndex].Position;
                    Vector2 v = LinkPosition(currentPart.LinkPosition, itemPos, shipItems[activeBoxIndex]);
                   
                    if (currentPart is RectangularHull)
                        s = new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]);
                    else if (currentPart is GunPart)
                        s = new Sprite(upgradeParts[PartTypes.GUNPART]);
                    else if (currentPart is EnginePart)
                        s = new Sprite(upgradeParts[PartTypes.ENGINEPART]);
                    shipItems.Add(new ShipItem(new Vector2(v.X, v.Y), s, currentPart.LinkPosition));
                }

            }
        }

        private void AddParts()
        {

        }


        private Vector2 LinkPosition(int pos, Vector2 itemPos, ShipItem activeBox)
        {
            switch (pos)
            {
                case 0:
                    return itemPos + new Vector2(activeBox.Width, 0);
                    break;
                case 1:
                    return itemPos + new Vector2(0, activeBox.Height);
                    break;
                case 2:
                    return itemPos + new Vector2(-activeBox.Width, 0);
                    break;
                case 3:
                    return itemPos + new Vector2(0, -activeBox.Height);
                    break;
                default:
                    return itemPos;
            }
        }

        public void Update(GameTime gameTime)
        {
            upgradeBar.Update(gameTime);
            CheckActions();
        }

        private void CheckActions()
        {
            for (int i = 0; i < shipItems.Count; i++)
            {
                if (shipItems[i].BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
                {
                    int oldActive = activeSelection;
                    activeSelection = i;
                    if (oldActive != i && activeSelection >= 0)
                        upgradeBar.CreateItemBoxes();
                    else if (oldActive == activeSelection)
                        activeSelection = -1;
                    if (activeSelection >= 0)
                        upgradeBar.Active = true;
                    else
                        upgradeBar.Active = false;
                    //Buy(100); //!
                }
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {         
                upgradeBar.Draw(spriteBatch, gameTime);

            //slots         
            for (int i = 0; i < shipItems.Count; i++)
            {

                if (i == activeSelection)
                    shipItems[i].Active = true;
                else
                    shipItems[i].Active = false;

                shipItems[i].Draw(spriteBatch, gameTime);

            }           
        }

        private void Buy(int price)
        {
            //if (totalResource - spentResource >= price)
            //{
            //    spentResource += price;
            //    //player.dosomething typ addpart(location)
            //}
        }
    }
}
