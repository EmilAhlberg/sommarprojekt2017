using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public interface IActivatable
    {
        bool IsActive { set; get; }
        void Activate(Vector2 source, Vector2 target);
    }
}
