using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject
{
    class Inventory
    {
        public List<Part> parts { get; private set; }

        public Inventory()
        {
            parts = new List<Part>();
        }

        public void AddPart(Part p)
        {
            parts.Add(p);
        }

        public void Sort()
        {
            parts.Sort((a, b) => a.GetType().ToString().CompareTo(b.GetType().ToString())); //Sorts by each part's type name
        }

        public Part GetPart(int index)
        {
            return parts[index];
        }

        public bool RemovePart(Part p)
        {
            return parts.Remove(p);
        }
    }
}
