using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerProject.collidables.parts
{
    interface ITargeting
    {
        DetectorPart Detector { get; }
        //void UpdateTarget(Entity target);
    }
}
