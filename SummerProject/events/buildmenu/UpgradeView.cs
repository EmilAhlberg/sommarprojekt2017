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
        private int activeSelection;
        private Player player;
        private List<Texture2D> upgradeParts;
        private UpgradeBar upgradeBar;

        public UpgradeView(Texture2D text, SpriteFont font, Player player, List<Texture2D> upgradeParts) //remove text param
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
                List<Part> parts = player.Parts;
                int activeBoxIndex = 0;
                Sprite s = new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]);
                shipItems.Add(new ShipItem(new Vector2(WindowSize.Width / 2, WindowSize.Height / 2), s, 0, PartTypes.RECTANGULARHULL)); //not 100% centered
                RectangularHull currentHull = (RectangularHull)parts[0];

                for (int i = 1; i < parts.Count; i++)
                {
                    Part currentPart = parts[i];
                    RectangularHull carrier = (RectangularHull)currentPart.Carrier;
                    int type = 0;
                    if (currentHull != carrier)
                    {
                        activeBoxIndex = parts.IndexOf(carrier);
                        currentHull = carrier;
                        AddEmptyParts(currentHull, activeBoxIndex);
                    }
                    Vector2 itemPos = shipItems[activeBoxIndex].Position;
                    Vector2 v = LinkPosition(currentPart.LinkPosition, itemPos, shipItems[activeBoxIndex]);

                    ShipItem shipItem = CreateShipItem(currentPart, currentPart.LinkPosition,  v);
                  
                    shipItems.Insert(i, shipItem);
                }
            }
        }


        private void AddEmptyParts(RectangularHull hull, int activeBoxIndex)
        {
            bool[] taken = hull.TakenPositions;
            for (int j = 0; j < taken.Length; j++)
            {
                if (!taken[j])
                {
                    Vector2 v = LinkPosition(j, shipItems[activeBoxIndex].Position, shipItems[activeBoxIndex]);
                    shipItems.Add(new ShipItem(new Vector2(v.X, v.Y), new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]), j, PartTypes.EMPTYPART, hull)); //secondary constructor for empty parts
                }

            }
        }

        private void AddPart(Part newPart)
        {
            IPartCarrier hull = null;
            if (shipItems[activeSelection].PartType == PartTypes.EMPTYPART)
            {
                hull = shipItems[activeSelection].Hull;
                hull.AddPart(newPart, shipItems[activeSelection].LinkPosition);
                player.Parts.Insert(activeSelection, newPart);
                
            }
            else if(!(shipItems[activeSelection].PartType == PartTypes.RECTANGULARHULL))
            {
                hull = player.Parts[activeSelection].Carrier;
                hull.AddPart(newPart, player.Parts[activeSelection].LinkPosition);
                player.Parts[activeSelection] = newPart;
            }


            int hullIndex = player.Parts.IndexOf((Part)hull);

            Vector2 v = LinkPosition(shipItems[activeSelection].LinkPosition, shipItems[hullIndex].Position, shipItems[hullIndex]);
            ShipItem s = CreateShipItem(newPart, shipItems[activeSelection].LinkPosition, v);
            shipItems[activeSelection] = s;
        }

        private ShipItem CreateShipItem(Part part, int linkPosition, Vector2 v)
        {
            int type = 0;
            Sprite s = null;
            if (part is RectangularHull)
            {
                s = new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]);
                type = PartTypes.RECTANGULARHULL;
            }
            else if (part is GunPart)
            {
                s = new Sprite(upgradeParts[PartTypes.GUNPART]);
                type = PartTypes.GUNPART;
            }
            else if (part is EnginePart)
            {
                s = new Sprite(upgradeParts[PartTypes.ENGINEPART]);
                type = PartTypes.ENGINEPART;
            }

            return new ShipItem(new Vector2(v.X, v.Y), s, part.LinkPosition, type);
        }

        private Vector2 LinkPosition(int pos, Vector2 itemPos, ShipItem activeBox)
        {
            switch (pos)
            {
                case 0:
                    return itemPos + new Vector2(-activeBox.Width, 0);
                case 1:
                    return itemPos + new Vector2(0, -activeBox.Height);
                case 2:
                    return itemPos + new Vector2(activeBox.Width, 0);
                case 3:
                    return itemPos + new Vector2(0, activeBox.Height);
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
                    {
                        upgradeBar.CreateItemBoxes();
                    }
                    else if (oldActive == activeSelection)
                        activeSelection = -1;
                    if (activeSelection >= 0)
                        upgradeBar.Active = true;
                    else
                        upgradeBar.Active = false;
                    //Buy(100); //!
                }
            }
            if (upgradeBar.Active && upgradeBar.Action)
                AddPart(upgradeBar.SelectedPart);
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
