using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    public interface CompositePart
    {
        Part[] Parts { get; }
        void addPart(Part p);
        void SubPartDamaged();
    }
}
