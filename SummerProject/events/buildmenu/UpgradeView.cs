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
using SummerProject.collidables.parts.guns;

namespace SummerProject.framework
{
    public class UpgradeView
    {
        private Dictionary<int, ShipItem> shipItems;
        private int activeSelection;
        private Player player;
        private List<IDs> upgradePartsIDs;
        public UpgradeBar UpgradeBar;
        private int emptyPartIndex = 100;
        private bool rotatePartSelected;

        internal void Reset()
        {
            ShipItem motherBoard = shipItems[0];
            foreach (ShipItem si in shipItems.Values)
            {
                Traits.CURRENCY.Counter += EntityConstants.PRICE[(int)si.id];
            }
            Traits.CURRENCY.Counter -= EntityConstants.PRICE[(int)motherBoard.id];
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
            UpgradeBar = new UpgradeBar(upgradePartsIDs, font, text);
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
                RenewEmptyBoxes();
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

        //refactor buying out of this method
        private void AddPart(Part newPart)
        {
            bool notEnoughMoney = false;  // only used to see if 
            if (activeSelection != 0)
            {
                ShipItem pressedItem = shipItems[activeSelection];
                RectangularHull hull = null;
                hull = pressedItem.Hull;
                if (pressedItem.id == IDs.RECTHULLPART)
                {
                    RemoveHull(pressedItem);
                    FixLinkPosition(pressedItem); //!!!!!
                }

                if (newPart == null)
                {
                    PlaceEmptyBox(pressedItem, hull);
                    UpgradeBar.SpentResource -= EntityConstants.PRICE[(int)pressedItem.id];
                }
                else
                {
                    float newPartPrice = EntityConstants.GetStatsFromID(EntityConstants.PRICE, EntityConstants.TypeToID(newPart.GetType()));
                    if (UpgradeBar.Resource >= newPartPrice)
                    {
                        PlacePart(pressedItem, hull, newPart);
                        UpgradeBar.SpentResource += newPartPrice;
                        UpgradeBar.SpentResource -= EntityConstants.PRICE[(int)pressedItem.id];
                    }
                    else
                        notEnoughMoney = true;


                }
                if (!notEnoughMoney)
                {
                    Vector2 v = pressedItem.Position;
                    ShipItem s = CreateShipItem(newPart, pressedItem.LinkPosition, v, (RectangularHull)hull);
                    shipItems[activeSelection] = s;
                    RenewEmptyBoxes();
                }
            }
        }

        private void PlacePart(ShipItem pressedItem, RectangularHull hull, Part newPart)
        {              
            hull.AddPart(newPart, pressedItem.LinkPosition);
            player.Parts.Add(newPart);
            newPart.Carrier = hull; //REDUNDANCY        
        }

        private void PlaceEmptyBox(ShipItem pressedItem, RectangularHull hull)
        {            
            pressedItem.Hull.RemovePart(pressedItem.Part); /////OR REMOVE AT <--bugged on link positions
            pressedItem.Part = null;
        }

        private void RenewEmptyBoxes()
        {
            List<ShipItem> rectangles = new List<ShipItem>();
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.Part is RectangularHull)
                {
                    rectangles.Add(item.Value);                   
                }
            }
            foreach (ShipItem item in rectangles)
            {
                AddEmptyParts((RectangularHull)item.Part, item, true);
            }
        }

        private void RemoveHull(ShipItem pressedItem)
        {
            List<ShipItem> removables = new List<ShipItem>();
            RemoveSetup(pressedItem, removables);
            List<ShipItem> leftOverHulls = null; 
            foreach (ShipItem removable in removables)
            {
                if (!LinkToOther(removable, pressedItem))
                {
                  leftOverHulls = LinkToSelf(removable, pressedItem);
                }
            }
            RemoveShipItems(pressedItem);
            if (leftOverHulls != null)
            {
                foreach (ShipItem leftOver in leftOverHulls)
                {
                    RemoveShipItems(leftOver);
                }
            }       
        }

        private void GetDependencies(ShipItem current, List<ShipItem> dependables)
        {
            dependables.Add(current);
            for (int i = 0; i < 4; i++)
            {
                Vector2 v = LinkPosition(i, new Vector2(current.BoundBox.Left, current.BoundBox.Top)); 
                ShipItem newHull = HullPresent(v);
                if (newHull != null && !dependables.Contains(newHull) && newHull.Hull == current.Part)  
                {
                    GetDependencies(newHull, dependables);
                }
            }
        }

        private List<ShipItem> LinkToSelf(ShipItem removable, ShipItem pressedItem)
        {
            List<ShipItem> dependables = new List<ShipItem>();
            GetDependencies(removable, dependables);

            for (int j = 0; j<dependables.Count; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 v = LinkPosition(i, new Vector2(dependables[j].BoundBox.Left, dependables[j].BoundBox.Top));
                    ShipItem newHull = HullPresent(v);
                    if (newHull != null && !dependables.Contains(newHull) && newHull.Part != pressedItem.Part) 
                    {                       
                        dependables[j].LinkPosition = i; // + 2) % 4; //!!!!!!!!!!!!!!!!!!                   
                        return ReverseDependency(newHull, dependables[j], dependables);
                    }
                }
            }
            return null;
        }

        private List<ShipItem> ReverseDependency(ShipItem newHull, ShipItem removable, List<ShipItem> dependables)
        {
            do
            {
                ((RectangularHull)newHull.Part).AddPart(removable.Part, (removable.LinkPosition+2) %4); //! linkposition wrong?
                removable.Hull = (RectangularHull)newHull.Part;                      //set hull to new hull            
             

                //probably all  done, --> reset
                newHull = removable;
                dependables.Remove(removable);
                removable = CheckDepedency(dependables, newHull); // MAKE THIS A LIST FUNCTION? FOR MULTIPLE BRANCHES!!!
            } while (removable != null);

            FixLinkPosition(newHull);
            return dependables;        
         
        }

        private void FixLinkPosition(ShipItem fixItem)
        {
            for (int i = 0; i<4; i++)
            {
                Vector2 v = LinkPosition(i, new Vector2(fixItem.BoundBox.Left, fixItem.BoundBox.Top)); //dependablse [i] not removable
                ShipItem newHull = HullPresent(v);
                if (newHull != null && newHull.Part == fixItem.Hull)
                {
                    fixItem.LinkPosition = (i + 2) %4; //!!!!
                }
            }
        }

        private ShipItem CheckDepedency(List<ShipItem> dependables, ShipItem hull)
        {
            foreach (ShipItem dependable in dependables)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 v = LinkPosition(i, new Vector2(dependable.BoundBox.Left, dependable.BoundBox.Top));
                    ShipItem newHull = HullPresent(v);
                    if (newHull == hull)
                    {
                        dependable.LinkPosition = i; //????????!!!!!!!!!!!!!!!
                        ((RectangularHull)dependable.Part).RemovePart(newHull.Part); ////! RIGHT?
                        return dependable;                      
                    }
                }               
            }
            return null; //all good --> done
        }

        private ShipItem GetItemFromHull(Part hull)
        {            
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {               
                if (hull == item.Value.Part)
                {
                    return item.Value;
                }
            }
            return null;
        }

        private bool LinkToOther(ShipItem removable, ShipItem pressedItem)
        {
            List<RectangularHull> hulls = ((RectangularHull)removable.Part).GetHulls();
            for (int i = 0; i < 4; i++)
            {              
                Vector2 v = LinkPosition(i, new Vector2(removable.BoundBox.Left, removable.BoundBox.Top)); //
                ShipItem s = HullPresent(v);
                if (s != null && !hulls.Contains(s.Part) && s.Part != pressedItem.Part)                                                         
                {
                    AlterDependency(removable, pressedItem,(RectangularHull)s.Part, i);
                    return true;
                }
            }
           return false;
        }

        private void AlterDependency(ShipItem removable, ShipItem pressedItem, RectangularHull hull, int linkPosition)
        {
            ((RectangularHull)pressedItem.Part.Carrier).RemovePart(pressedItem.Part);
            pressedItem.Hull.RemovePart(pressedItem.Part);                 //remove pressedItem from parts structure
            hull.AddPart(removable.Part, (linkPosition + 2) % 4); //!         //add part to new hull in part structure                   
            removable.Hull = hull;                      //set hull to new hull
            ((RectangularHull)removable.Part).Carrier = removable.Hull;    //   - | | -               parts structure
            removable.LinkPosition = (linkPosition + 2); //?
        }

        private void RemoveSetup(ShipItem pressedItem, List<ShipItem> removable)
        {
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.Hull == pressedItem.Part && item.Value.id == IDs.RECTHULLPART)
                {
                    removable.Add(item.Value);
                }
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
            IDs id = IDs.EMPTYPART;       
            if(part != null)
                id = EntityConstants.TypeToID(part.GetType());          
            return new ShipItem(new Vector2(v.X, v.Y), linkPosition, hull, part, id);

            //return new ShipItem(new Vector2(v.X, v.Y), part.LinkPosition, hull, part, newID);
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
            UpgradeBar.Update(gameTime);
            CheckActions();
        }

        private void CheckActions()
        {
       
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
                if (item.Value.BoundBox.Contains(InputHandler.mPosition))
                {
                    if (InputHandler.isJustPressed(MouseButton.LEFT) && UpgradeBar.Action) {
                        if (!UpgradeBar.RotateItemSelected)
                        {
                            activeSelection = item.Key;
                            AddPart(UpgradeBar.SelectedPart);
                        }
                        else
                            RotatePart(item.Value);
                        break;
                    }
                }
            }
        }

        private bool RotatePart(ShipItem current)
        {
            for (int i = 0; i < 4; i++)
            {                
                    int newPos = (current.LinkPosition + i) % 4;
                    Vector2 v = LinkPosition(newPos, new Vector2(current.BoundBox.Left, current.BoundBox.Top));
                    ShipItem s = HullPresent(v);
                    if (s != null && current.Hull != s.Part )//&& current.id !=IDs.EMPTYPART) ///current?
                    {
                        ((RectangularHull)s.Part).AddPart(current.Part, (newPos + 2) % 4);
                        current.Hull.RemovePart(current.Part); //restore hull ????????!!!!!!!!!!!!!!!!!!!!!!!! in parts also, not only shipItem
                        //((RectangularHull)removable.Part).Carrier = removable.Hull; //ALSO WHEN ADDPART?
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
            UpgradeBar.Draw(spriteBatch, gameTime);

            //slots         
            foreach (KeyValuePair<int, ShipItem> item in shipItems)
            {
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