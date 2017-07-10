using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public interface ICollidable
    {
        bool CollidesWith(ICollidable c2);
        void Collision(ICollidable c2);
    }
}
