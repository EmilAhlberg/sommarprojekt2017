using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.events.buildmenu
{
    public class UpgradeBarItem : ClickableItem
    {
        public UpgradeBarItem(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
        }

        public Part ReturnPart()
        {
            switch (id)
            {
                case IDs.GUNPART:
                    return new GunPart();
                case IDs.ENGINEPART:
                    return new EnginePart();
                case IDs.RECTHULLPART:
                    return new RectangularHull();
                case IDs.SPRAYGUNPART:
                    return new SprayGunPart();
                case IDs.MINEGUNPART:
                    return new MineGunPart();
                case IDs.CHARGINGGUNPART:
                    return new ChargingGunPart();
                case IDs.EMPTYPART:
                    return new GunPart(); //!!
                default: return new EnginePart();

            }
        }
    }
}
