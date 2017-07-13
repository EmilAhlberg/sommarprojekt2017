using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.events.buildmenu
{
    class RotateItem : ClickableItem
    {
        public RotateItem(Vector2 position, IDs id = IDs.DEFAULT) : base(position, id)
        {
        }
    }
}
