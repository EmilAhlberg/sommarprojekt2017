using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerProject.collidables.parts;

namespace SummerProject.collidables.enemies.SemiBosses
{
    class EngineSBoss1 : Enemy
    {
        public EngineSBoss1(Vector2 position, PartController player, IDs id = IDs.DEFAULT) : base(position, player, id)
        {
            CompositePart u = new RectangularHull();
            CompositePart l = new RectangularHull();
            CompositePart r = new RectangularHull();
            CompositePart ld = new RectangularHull();
            CompositePart rd = new RectangularHull();
            Hull.AddPart(u,1);
            Hull.AddPart(r, 0);
            Hull.AddPart(l, 2);
            Hull.AddPart(new EnginePart(), 3);
            u.AddPart(new RectangularHull(), 0);
            u.AddPart(new RectangularHull(), 2);
            l.AddPart(ld, 3);
            r.AddPart(rd, 3);
            ld.AddPart(new EnginePart(), 3);
            rd.AddPart(new EnginePart(), 3);
        }

        public override void Move()
        {
            Hull.TakeAction(typeof(EnginePart));
        }
    }
}
