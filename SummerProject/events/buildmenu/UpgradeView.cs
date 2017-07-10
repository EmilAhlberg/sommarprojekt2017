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
using SummerProject.factories;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private Dictionary<int, ShipItem> shipItems;
        private int activeSelection;
        private Player player;
        private List<IDs> upgradePartsIDs;
        private UpgradeBar upgradeBar;
        private int emptyPartIndex = 100;

        //maybe not
        internal void Reset()
        {
            ShipItem motherBoard = shipItems[0];
            ((RectangularHull)motherBoard.Part).ResetLinks();
            shipItems = new Dictionary<int, ShipItem>();
            shipItems.Add(0, motherBoard);            
            AddEmptyParts((RectangularHull)motherBoard.Part, shipItems[0], false);
        }

     

        public UpgradeView(Texture2D text, SpriteFont font, Player player, List<IDs> upgradePartsIDs) //remove text param
        {
            activeSelection = -1;
            this.upgradePartsIDs = upgradePartsIDs;
            this.player = player;
            upgradeBar = new UpgradeBar(upgradePartsIDs, font, text);
        }

        internal void Initialize()
        {
            if (shipItems == null)
            {             
                shipItems = new Dictionary<int, ShipItem>();
                List<Part> parts = player.Parts;
                int activeBoxIndex = 0;
                IDs id = IDs.RECTHULLPART;      
                shipItems.Add(0, new ShipItem(new Vector2(WindowSize.Width / 2, WindowSize.Height / 2), 0, null, player.Parts[0],  id)); //not 100% centered   + hull = null  + linkpos = 0
                RectangularHull currentHull = (RectangularHull)parts[0];

                for (int i = 1; i < parts.Count; i++)
                {
                    Part currentPart = parts[i];
                    RectangularHull carrier = (RectangularHull)currentPart.Carrier;                
                    if (currentHull != carrier)
                    {
                        activeBoxIndex = parts.IndexOf(carrier);
                        currentHull = carrier;
                        AddEmptyParts(currentHull, shipItems[activeBoxIndex], false);
                    }
                    Vector2 itemPos = shipItems[activeBoxIndex].Position;
                    Vector2 v = LinkPosition(currentPart.LinkPosition, itemPos);

                    ShipItem shipItem = CreateShipItem(currentPart, currentPart.LinkPosition,  v, currentHull);
                    shipItems.Add(i, shipItem);
                }
            }
        }

        private void AddEmptyParts(RectangularHull hull, ShipItem current, bool inMenu)
        {
            bool[] taken = hull.TakenPositions;
            for (int j = 0; j < taken.Length; j++)
            {
                if (!taken[j])
                {
                    if (inMenu)
                    {
                        Vector2 v = LinkPosition(j, current.Position);
                        if (PositionIsFree(v))
                            shipItems.Add(emptyPartIndex++, new ShipItem(new Vector2(v.X, v.Y), j, hull, null, IDs.EMPTYPART)); //secondary constructor for empty parts /!!! null
                    }
                    else
                    {
                        Vector2 v = LinkPosition(j, current.Position);
                        shipItems.Add(emptyPartIndex++, new ShipItem(new Vector2(v.X, v.Y), j, hull, null, IDs.EMPTYPART)); //secondary constructor for empty parts /!!! null
                    }
                }                
            }
        }

        private bool PositionIsFree(Vector2 probe)
        {
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.BoundBox.Contains(probe))
                {
                    return false;
                }
            }
            return true;
        }

        private void AddPart(Part newPart)
        {
            if (activeSelection != 0)
            {
                IPartCarrier hull = null;
                ShipItem pressedItem = shipItems[activeSelection];

                hull = pressedItem.Hull;
                hull.AddPart(newPart, pressedItem.LinkPosition);
                player.Parts.Add(newPart);

                if (pressedItem.id == IDs.RECTHULLPART)
                {                
                    RemoveShipItems(pressedItem);
                }

                if (newPart is RectangularHull)
                {
                    AddEmptyParts((RectangularHull)newPart, pressedItem, true);
                }
                newPart.Carrier = hull;

                Vector2 v = pressedItem.Position;
                ShipItem s = CreateShipItem(newPart, pressedItem.LinkPosition, v, (RectangularHull)hull);
                shipItems[activeSelection] = s;
                //parts.remove(stuff) ?
            }
        }
       

        private void RemoveShipItems(ShipItem pressedItem)
        {
            List<int> temp = new List<int>();
            GetRemovedItems(pressedItem, temp);
            foreach (int i in temp)
                shipItems.Remove(i);
        }

        private void GetRemovedItems(ShipItem current, List<int> temp)
        {           
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
                if (item.Value.Hull == current.Part)
                {
                    temp.Add(item.Key);
                    if (item.Value.Part is RectangularHull)
                    {
                        GetRemovedItems(item.Value, temp);
                    }
                }                                         
        }

        private ShipItem CreateShipItem(Part part, int linkPosition, Vector2 v, RectangularHull hull)
        {
            IDs newID = 0;
            if (part is RectangularHull)
            {
                newID = IDs.RECTHULLPART;
            }
            else if (part is SprayGunPart)
            {
                newID = IDs.SPRAYGUNPART;
            }
            else if (part is MineGunPart)
            {
                newID = IDs.MINEGUNPART;
            }
            else if (part is ChargingGunPart)
            {
                newID = IDs.CHARGINGGUNPART;
            }
            else if (part is GunPart)
            {
                newID = IDs.GUNPART;
            }
            else if (part is EnginePart)
            {
                newID = IDs.ENGINEPART;
            }

            return new ShipItem(new Vector2(v.X, v.Y), part.LinkPosition, hull, part, newID);
        }

        private Vector2 LinkPosition(int pos, Vector2 itemPos)
        {
            switch (pos)
            {
                case 0: // mirrored
                    return itemPos + new Vector2(-ShipItem.Width, 0);
                case 1:
                    return itemPos + new Vector2(0, -ShipItem.Height);
                case 2:
                    return itemPos + new Vector2(ShipItem.Width, 0);
                case 3:
                    return itemPos + new Vector2(0, ShipItem.Height);
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
                if (item.Value.BoundBox.Contains(InputHandler.mPosition))
                {
                    if (InputHandler.isJustPressed(MouseButton.LEFT)) {
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
                    else if (InputHandler.isJustPressed(MouseButton.RIGHT) && item.Value.id != IDs.EMPTYPART)
                    {
                        if (RotatePart(item.Value))
                        {

                        }
                    }
                }
            }
            if (upgradeBar.Active && upgradeBar.Action)
                AddPart(upgradeBar.SelectedPart);
        }

        private bool RotatePart(ShipItem current)
        {
            for (int i = 1; i < 4; i++)
            {
                int newPos = (current.LinkPosition +i) %4;
                //if (newPos % 2 == 0)
                //    newPos = (newPos + 2) % 4;
                Vector2 v = LinkPosition(newPos, new Vector2(current.BoundBox.Left, current.BoundBox.Top));// + new Vector2(ClickableItem.Width/2, ClickableItem.Height / 2); //"center" of box   VECTOR2 IS RIGHT/WRONG?
                ShipItem s = HullPresent(v);
                if (s != null)//&& current.id !=IDs.EMPTYPART) ///current?
                {
                    ((RectangularHull)s.Part).AddPart(current.Part, (newPos + 2)%4 );
                    current.Hull = (RectangularHull)s.Part;
                    current.LinkPosition = newPos;
                    current.UpdateRotation();                      
                    return true;
                }
            }
            return false;
        }
        private ShipItem HullPresent(Vector2 v)
        {

            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.BoundBox.Contains(v) && item.Value.id == IDs.RECTHULLPART)
                {
                    return item.Value;
                }
            }
            return null;
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

//crap for
//private void FixDependencies(ShipItem pressedItem)
//{
//    List<ShipItem> dependables = new List<ShipItem>();
//    GetDependencies(pressedItem, dependables);
//    //
//    int size = dependables.Count;
//    while (dependables.Count != 0)
//    {
//        TryFindNewPart(pressedItem, dependables);
//        if (size == dependables.Count)
//            break;
//        size = dependables.Count;
//    }


//}

//private void TryFindNewPart(ShipItem pressedItem, List<ShipItem> dependables)
//{
//    List<ShipItem> savedItems = new List<ShipItem>();
//    foreach (ShipItem s in dependables)
//    {
//        ShipItem adjacent = AdjacentToHull(s, pressedItem); //works, but also empty boxes     DO NOT TRUST
//        if (adjacent != null)
//        {
//            GetDependencies(adjacent, savedItems); 
//            break;
//        }
//    }

//    foreach(ShipItem item in savedItems)
//    {
//        dependables.Remove(item);
//    }
//}

//private ShipItem AdjacentToHull(ShipItem current, ShipItem hull)
//{
//    for (int i = 0; i < 4; i++)
//    {
//        Vector2 v = LinkPosition(i, new Vector2(current.BoundBox.Left, current.BoundBox.Top));// + new Vector2(ClickableItem.Width/2, ClickableItem.Height / 2); //"center" of box   VECTOR2 IS RIGHT/WRONG?
//        ShipItem s = HullPresent(v, hull);
//        if (s != null)//&& current.id !=IDs.EMPTYPART) ///current?
//        {
//            s.Hull.AddPart(current.Part, (i + 2) % 4);
//            current.Hull = (RectangularHull)s.Part;
//            return s;
//        }
//    }
//    return null;          
//}

//private ShipItem HullPresent(Vector2 v, ShipItem hull)
//{
//    foreach(KeyValuePair<int, ShipItem> item in shipItems)
//    {
//        if (item.Value.BoundBox.Contains(v) && item.Value != hull && item.Value.id == IDs.RECTHULLPART)
//        {
//            return item.Value;
//        }
//    }
//    return null;
//}

//private void GetDependencies(ShipItem current, List<ShipItem> dependables)
//{
//    foreach (KeyValuePair<int, ShipItem> item in shipItems)
//    {
//        if (item.Value.Hull == current.Part && !dependables.Contains(current))
//        {
//            dependables.Add(item.Value);
//            GetDependencies(item.Value, dependables);
//        }
//    }
//}