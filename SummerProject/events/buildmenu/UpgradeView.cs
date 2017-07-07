using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SummerProject.collidables;
using SummerProject.util;
using SummerProject.achievements;
using SummerProject.collidables.parts;
using SummerProject.events.buildmenu;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private Dictionary<int, ShipItem> shipItems;
        private int activeSelection;
        private Player player;
        private List<Texture2D> upgradeParts;
        private UpgradeBar upgradeBar;
        private int emptyPartIndex = 100;

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
                //shipItems = new List<ShipItem>();
                shipItems = new Dictionary<int, ShipItem>();
                List<Part> parts = player.Parts;
                int activeBoxIndex = 0;
                Sprite s = new Sprite(upgradeParts[PartTypes.RECTANGULARHULL]);
                shipItems.Add(0, new ShipItem(new Vector2(WindowSize.Width / 2, WindowSize.Height / 2), s, 0, PartTypes.RECTANGULARHULL)); //not 100% centered
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

                    ShipItem shipItem = CreateShipItem(currentPart, currentPart.LinkPosition,  v, currentHull);
                    shipItems.Add(i, shipItem);
                    //shipItems.Insert(i, shipItem);
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
                    //!shipitems.Count
                    Vector2 v = LinkPosition(j, shipItems[activeBoxIndex].Position, shipItems[activeBoxIndex]);
                    shipItems.Add(emptyPartIndex++,new ShipItem(new Vector2(v.X, v.Y), new Sprite(upgradeParts[PartTypes.EMPTYPART]), j, PartTypes.EMPTYPART, hull)); //secondary constructor for empty parts
                }

            }
        }

        private void AddPart(Part newPart)
        {
            IPartCarrier hull = null;
            ShipItem current = shipItems[activeSelection];
            if (current.PartType == PartTypes.EMPTYPART)
            {
                hull = current.Hull;
                hull.AddPart(newPart, current.LinkPosition);
                player.Parts.Add(newPart);
                //   player.Parts.Insert(activeSelection, newPart);

            }
            else if(!(current.PartType == PartTypes.RECTANGULARHULL))
            {
                hull = current.Hull;
                //hull = player.Parts[activeSelection].Carrier; //?
                hull.AddPart(newPart, current.LinkPosition);
                player.Parts.Add(newPart);
                //player.Parts. Insert(activeSelection, newPart); // insert or part[activeselection] = newPart ??
            }
            newPart.Carrier = hull;

            Vector2 v = current.Position;
            ShipItem s = CreateShipItem(newPart, shipItems[activeSelection].LinkPosition, v, (RectangularHull) hull);
            shipItems[activeSelection] = s;
        }

        private ShipItem CreateShipItem(Part part, int linkPosition, Vector2 v, RectangularHull hull)
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

            return new ShipItem(new Vector2(v.X, v.Y), s, part.LinkPosition, type, hull);
        }

        private Vector2 LinkPosition(int pos, Vector2 itemPos, ShipItem activeBox)
        {
            switch (pos)
            {
                case 0: // mirrored
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
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
                {
                    int oldActive = activeSelection;
                    activeSelection = item.Key;
                    if (oldActive != activeSelection && activeSelection >= 0)
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
            //for (int i = 0; i < shipItems.Count; i++)
            //{
            //    if (shipItems[i].BoundBox.Contains(InputHandler.mPosition) && InputHandler.isJustPressed(MouseButton.LEFT))
            //    {
            //        int oldActive = activeSelection;
            //        activeSelection = i;
            //        if (oldActive != i && activeSelection >= 0)
            //        {
            //            upgradeBar.CreateItemBoxes();
            //        }
            //        else if (oldActive == activeSelection)
            //            activeSelection = -1;
            //        if (activeSelection >= 0)
            //            upgradeBar.Active = true;
            //        else
            //            upgradeBar.Active = false;
            //        //Buy(100); //!
            //    }
            //}
            //if (upgradeBar.Active && upgradeBar.Action)
            //    AddPart(upgradeBar.SelectedPart);
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            upgradeBar.Draw(spriteBatch, gameTime);

            //slots         
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Key == activeSelection)
                    item.Value.Active = true;
                else
                    item.Value.Active = false;

                item.Value.Draw(spriteBatch, gameTime);
            }
        }
            //for (int i = 0; i < shipItems.Count; i++)
            //{

            //    if (i == activeSelection)
            //        shipItems[i].Active = true;
            //    else
            //        shipItems[i].Active = false;

            //    shipItems[i].Draw(spriteBatch, gameTime);
            //}

        

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
