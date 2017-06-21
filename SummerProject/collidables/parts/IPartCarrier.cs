using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    interface IPartCarrier
    {
        bool AddPart(Part part, int pos);
    }
}
