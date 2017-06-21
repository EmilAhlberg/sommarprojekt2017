using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.collidables.parts
{
    public interface IPartCarrier
    {
        bool AddPart(Part part, int pos);
        void Collision(Collidable c2);
    }
}
